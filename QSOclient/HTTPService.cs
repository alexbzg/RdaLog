﻿//#define DISABLE_HTTP
#define TEST_SRV
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XmlConfigNS;
using System.Windows.Forms;
using SerializationNS;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Reflection;
using ProtoBuf;

namespace tnxlog
{
    [DataContract]
    public class HttpServiceConfig : ConfigSection
    {
        [DataMember]
        public string callsign;
        [DataMember]
        public string password;
        [XmlIgnore]
        private string _token;
        [XmlIgnore]
        public EventHandler<EventArgs> logInOout;

        [DataMember]
        public string token
        {
            get { return _token; }
            set
            {
                if (_token != value)
                {
                    _token = value;
                    write();
                    logInOout?.Invoke(this, new EventArgs());
                }
            }
        }

        public HttpServiceConfig(XmlConfig _parent) : base(_parent) { }
        public HttpServiceConfig() : base() { }

    }
    public class HttpService
    {
        private static int pingIntervalDef = 60 * 1000;
        private static int pingIntervalNoConnection = 5 * 1000;
        HttpClient client = new HttpClient();
#if DEBUG && TEST_SRV
        private static readonly Uri srvURI = new Uri("http://test.tnxqso.com/aiohttp/");
#else
        private static readonly Uri srvURI = new Uri("http://tnxqso.com/aiohttp/");
#endif
        System.Threading.Timer pingTimer;
        System.Threading.Timer loginRetryTimer;
        ConcurrentQueue<LogRequest> logQueue = new ConcurrentQueue<LogRequest>();
        private string unsentFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "unsent.dat");
        private string stationCallsign = null;
        private volatile bool _connected;
        public bool connected { get { return _connected; }
            set {
                if (value != _connected)
                {
                    _connected = value;
                    connectionStateChanged?.Invoke(this, new EventArgs());
                }
            }
        }
        public EventHandler<EventArgs> connectionStateChanged;
        private HttpServiceConfig config;
        private Tnxlog rdaLog;
        public bool gpsServerLoad;


        public HttpService(HttpServiceConfig _config, Tnxlog _rdaLog)
        {
            config = _config;
            rdaLog = _rdaLog;
            //for backwards compatibility
            List<QSO> unsentQSOs = ProtoBufSerialization.Read<List<QSO>>(unsentFilePath);
            if (unsentQSOs != null && unsentQSOs.Count > 0)
                Task.Run(async () =>
               {
                   foreach (QSO qso in unsentQSOs)
                       await postQso(qso);
               });
            unsentQSOs = ProtoBufSerialization.Read<List<QSO>>(unsentFilePath + ".qso");
            if (unsentQSOs != null && unsentQSOs.Count > 0)
                Task.Run(async () =>
                {
                    foreach (QSO qso in unsentQSOs)
                        await postQso(qso);
                });
            List<QsoDeleteRequest>unsentDels = ProtoBufSerialization.Read<List<QsoDeleteRequest>>(unsentFilePath + ".del");
            if (unsentDels != null && unsentDels.Count > 0)
                Task.Run(async () =>
                {
                    foreach (QsoDeleteRequest del in unsentDels) 
                        if (!await _postDeleteQso(del))
                            addToQueue(del);
                });

        }

        private void schedulePingTimer()
        {
            pingTimer = new System.Threading.Timer(obj => ping(), null, 5000, Timeout.Infinite);
        }

        private async Task<HttpResponseMessage> post(string _URI, object data)
        {
            return await post(_URI, data, true);
        }

        private async Task<HttpResponseMessage> post(string _URI, object data, bool warnings)
        {
            string sContent = JsonConvert.SerializeObject(data);
            System.Diagnostics.Debug.WriteLine(sContent);
            string URI = srvURI + _URI;
#if DEBUG && DISABLE_HTTP
            return true;
#endif
            HttpContent content = new StringContent(sContent);
            HttpResponseMessage response = null;
            bool result = false;
            try
            {
                response = await client.PostAsync(URI, content);
                result = response.IsSuccessStatusCode;
                System.Diagnostics.Debug.WriteLine(response.ToString());
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            if (connected != result)
            {
                connected = result;
                if (connected)
                    await processQueue();
            }
            /*if (response?.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                string srvMsg = await response.Content.ReadAsStringAsync();
                if (srvMsg == "Login expired")
                {
                    if (config.callsign.Length > 3 && config.password.Length > 5 && await login(config.callsign, config.password) == System.Net.HttpStatusCode.OK)
                        return await post(_URI, data, warnings);
                }
                if (warnings)
                    MessageBox.Show(await response.Content.ReadAsStringAsync(), "Bad request to server");
            }*/
            return response;
        }

        private async Task<bool> _postQso(QSO qso)
        {
            if (qso._deleted)
                return true;
            HttpResponseMessage response = await post("log", qsoToken(qso));
            if (response == null || !response.IsSuccessStatusCode)
                return false;
            else if (qso.serverTs != 0)
            {
                NewQsoResponse newQsoResponse = JsonConvert.DeserializeObject<NewQsoResponse>(await response.Content.ReadAsStringAsync());
                qso.serverTs = newQsoResponse.ts;
            }
            return true;
        }

        private async Task<bool> _postDeleteQso(QsoDeleteRequest req)
        {
            HttpResponseMessage response = await post("log", req);
            if (response == null || !response.IsSuccessStatusCode)
                return false;
            return true;
        }

        public async Task postQso(QSO qso)
        {
            if (logQueue.IsEmpty && config.token != null)
            {
                if (!await _postQso(qso))
                    addToQueue(qso);
            }
            else
                addToQueue(qso);
        }

        private void addToQueue(QSO qso)
        {
            if (!qsoInQueue(qso))
            {
                logQueue.Enqueue(new LogRequest() { qso = qso });
                saveUnsent();
            }
        }

        private void addToQueue(QsoDeleteRequest req)
        {
            logQueue.Enqueue(new LogRequest() { delete = req });
            saveUnsent();
        }

        public async Task deleteQso(QSO qso)
        {
            if (qsoInQueue(qso))
                qso._deleted = true;
            else if (qso.serverTs != 0)
            {
                QsoDeleteRequest req = new QsoDeleteRequest(config, qso);
                if (logQueue.IsEmpty && config.token != null)
                {
                    if (!await _postDeleteQso(req))
                        addToQueue(req);
                }
                else
                    addToQueue(req);
            }
        }

        private bool qsoInQueue(QSO qso)
        {
            return logQueue.Where(item => item.qso != null).Select(item => item.qso).Contains(qso);
        }

        private void saveUnsent()
        {
            ProtoBufSerialization.Write<List<QSO>>(unsentFilePath + ".qso", logQueue.Where(item => item.qso != null).Select(item => item.qso).ToList());
            ProtoBufSerialization.Write<List<QsoDeleteRequest>>(unsentFilePath + ".del", logQueue.Where(item => item.delete != null).Select(item => item.delete).ToList());
        }

        private async Task processQueue()
        {
            while (!logQueue.IsEmpty && config.token != null)
            {
                logQueue.TryPeek(out LogRequest req);
                bool r = req.qso != null ? await _postQso(req.qso) : await _postDeleteQso(req.delete);
                if (r)
                {
                    logQueue.TryDequeue(out req);
                    saveUnsent();
                }
                else
                    break;
            }
        }

        private static string stringJSONfield(string val)
        {
            return val == null || val == "" ? "null" : "\"" + val + "\"";
        }

        private static string JSONfield(string val)
        {
            return val == null || val == "" ? "null" : val;
        }

        public async Task getLocation()
        {
            Uri statusUri = new Uri(srvURI.Scheme + "://" + srvURI.Host + "/static/stations/" + stationCallsign + "/status.json");
            try
            {
                HttpResponseMessage response = await client.GetAsync(statusUri);
                System.Diagnostics.Debug.WriteLine(response.ToString());
                if (response.IsSuccessStatusCode)
                {
                    LocationResponse location = JsonConvert.DeserializeObject<LocationResponse>(await response.Content.ReadAsStringAsync());
                    rdaLog.setStatusFieldValue("rafa", location.rafa);
                    rdaLog.setStatusFieldValue("rda", location.rda);
                    rdaLog.setStatusFieldValue("locator", location.loc);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
        }

        public async Task ping()
        {
            if (config.token == null)
                return;
            HttpResponseMessage response = await post("location", new StatusData(config));
            if (stationCallsign == null)
                await getUserData();
            if (stationCallsign != null && response.IsSuccessStatusCode)
            {
                LocationResponse location = JsonConvert.DeserializeObject<LocationResponse>(await response.Content.ReadAsStringAsync());
                rdaLog.setStatusFieldValue("rafa", location.rafa);
                rdaLog.setStatusFieldValue("rda", location.rda);
                rdaLog.setStatusFieldValue("locator", location.loc);
            }
            pingTimer.Change(response != null && response.IsSuccessStatusCode ? pingIntervalDef : pingIntervalNoConnection, Timeout.Infinite);
        }

        public async Task<System.Net.HttpStatusCode?> login(bool retry=false)
        {
            if (string.IsNullOrEmpty(config.callsign) || string.IsNullOrEmpty(config.password))
                return null;
            HttpResponseMessage response = await post("login", new LoginRequest() { login = config.callsign, password = config.password }, false);
            if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    LoginResponse userData = JsonConvert.DeserializeObject<LoginResponse>(await response.Content.ReadAsStringAsync());
                    config.token = userData.token;
                    schedulePingTimer();
                    if (loginRetryTimer != null)
                    {
                        loginRetryTimer.Change(Timeout.Infinite, Timeout.Infinite);
                        loginRetryTimer = null;
                    }
                    Task.Run(() => processQueue());
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    config.token = null;
                    MessageBox.Show(await response.Content.ReadAsStringAsync(), Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                } else if (retry)
                {
                    if (loginRetryTimer == null)
                        loginRetryTimer = new System.Threading.Timer(obj => login(true), null, pingIntervalNoConnection, Timeout.Infinite);
                    else
                        loginRetryTimer.Change(pingIntervalNoConnection, Timeout.Infinite);
                }
            }
            return response?.StatusCode;
        }

        public async Task postFreq(string freq)
        {
            await post("location", new FreqData(config, freq));
        }

        private async Task<System.Net.HttpStatusCode?> getUserData()
        {
            HttpResponseMessage response = await post("userData", new JSONToken(config), false);
            if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    UserDataResponse userData = JsonConvert.DeserializeObject<UserDataResponse>(await response.Content.ReadAsStringAsync());
                    stationCallsign = userData.settings.station.callsign.ToLower().Replace( '/', '-' );
                }
            }
            return response?.StatusCode;
        }


        private QSOtoken qsoToken(QSO qso)
        {
            return new QSOtoken(config, qso);
        }
    }


    public class JSONToken
    {
        [IgnoreDataMember]
        internal HttpServiceConfig config;
        public string token { get { return config.token; } set { } }

        public JSONToken(HttpServiceConfig _config)
        {
            config = _config;
        }
    }

    public class LoginRequest
    {
        public string login;
        public string password;
    }

    public class LocationResponse
    {
        public string rafa;
        public string rda;
        public string loc;
    }

    public class LoginResponse
    {
        public string token;
        public string callsign;
    }

    public class StationSettings
    {
        public string callsign;
    }

    public class UserSettings
    {
        public StationSettings station;
    }


    public class UserDataResponse
    {
        public UserSettings settings;
    }

    public class NewQsoResponse
    {
        public decimal ts;
    }

    public class LogRequest
    {
        public QsoDeleteRequest delete;
        public QSO qso;
    }

    class QSOtoken : JSONToken
    {
        public QSO qso;

        internal QSOtoken(HttpServiceConfig _config, QSO _qso) : base(_config)
        {
            qso = _qso;
        }
    }
    [DataContract, ProtoContract]
    public class QsoDeleteRequest : JSONToken
    {
        [DataMember, ProtoMember(1)]
        public decimal delete;
        internal QsoDeleteRequest(HttpServiceConfig _config, QSO qso) : base(_config)
        {
            delete = qso.serverTs;
        }
    }

    class FreqData : JSONToken
    {
        internal FreqData(HttpServiceConfig _config, string _freq) : base(_config) {
            freq = _freq;
        }
        public string freq;
    }

    class StatusData : JSONToken
    {
        public string loc { get { return ((TnxlogConfig)config.parent).getStatusFieldValue("locator"); } set { } }
        public bool ShouldSerializeloc()
        {
            return !((TnxlogConfig)config.parent).getStatusFieldAuto("locator");
        }
        public string rafa { get { return ((TnxlogConfig)config.parent).getStatusFieldValue("rafa"); } set { } }
        public bool ShouldSerializerafa()
        {
            return !((TnxlogConfig)config.parent).getStatusFieldAuto("rafa");
        }

        public string rda { get { return ((TnxlogConfig)config.parent).getStatusFieldValue("rda"); } set { } }
        public bool ShouldSerializerda()
        {
            return !((TnxlogConfig)config.parent).getStatusFieldAuto("rda");
        }

        public string userField { get { return ((TnxlogConfig)config.parent).userField; } set { } }

        internal StatusData(HttpServiceConfig _config) : base(_config) { }

    }

}
