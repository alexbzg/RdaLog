﻿//#define DISABLE_HTTP
#define TEST_SRV
//#define DISABLE_HTTP_LOGGING
//#define DISABLE_HTTP_LOGGING_CONTENT
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
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Net;

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
        public int updateIterval = 60 * 1000;
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
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private static int pingIntervalNoConnection = 5 * 1000;
#if DEBUG && TEST_SRV
        private static readonly Uri srvURI = new Uri("https://test.tnxqso.com/aiohttp/");
#else
        private static readonly Uri srvURI = new Uri("https://tnxqso.com/aiohttp/");
#endif
        System.Threading.Timer pingTimer;
        System.Threading.Timer loginRetryTimer;
        ConcurrentQueue<LogRequest> logQueue = new ConcurrentQueue<LogRequest>();
        private string unsentFilePath;
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
        private Tnxlog tnxlog;
        private TnxlogConfig tnxlogConfig { get { return (TnxlogConfig)config.parent; } }

        public HttpService(HttpServiceConfig _config, Tnxlog _rdaLog)
        {
            config = _config;
            tnxlog = _rdaLog;
            unsentFilePath = Path.Combine(tnxlog.dataPath, "unsent");
            List<QSO> unsentQSOs = QSOFactory.ReadList<List<QSO>>(unsentFilePath + ".qso");
            if (unsentQSOs != null && unsentQSOs.Count > 0)
                Task.Run(async () =>
                {
                    await postQso(unsentQSOs.ToArray());
                });
            List<QsoDeleteData>unsentDels = ProtoBufSerialization.Read<List<QsoDeleteData>>(unsentFilePath + ".del");
            if (unsentDels != null && unsentDels.Count > 0)
                Task.Run(async () =>
                {
                    foreach (QsoDeleteData del in unsentDels) 
                        if (!await _postDeleteQso(del))
                            addToQueue(del);
                });

        }

        private void schedulePingTimer()
        {
            pingTimer = new System.Threading.Timer(async obj => await ping(), null, 5000, Timeout.Infinite);
        }

        private async Task<HttpResponseMessage> post(string _URI, object data, bool warnings = true, int timeoutSeconds = 100)
        {
            string sContent = JsonConvert.SerializeObject(data);
#if !DISABLE_HTTP_LOGGING_CONTENT
            System.Diagnostics.Debug.WriteLine(sContent);
#endif
            string URI = srvURI + _URI;
#if DEBUG && DISABLE_HTTP
            return true;
#endif
            HttpClient client = new HttpClient();
            HttpContent content = new StringContent(sContent);
            HttpResponseMessage response = null;
            client.Timeout = TimeSpan.FromSeconds(timeoutSeconds < 100 ? 100 : timeoutSeconds);
            bool result = false;
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                response = await client.PostAsync(URI, content);
                result = response.IsSuccessStatusCode;
#if !DISABLE_HTTP_LOGGING
                System.Diagnostics.Debug.WriteLine(response.ToString());
#endif
            }
            catch (Exception e)
            {
                logger.Error(e, "HTTP post error");
                logger.Error($"Server URI {URI}");
                logger.Error(e.ToString());
            }
            if (connected != result)
            {
                connected = result;
                if (connected)
                    await processQueue();
            }
            return response;
        }

        private async Task<bool> _postQso(QSO[] qso)
        {
            QSO[] _qso = qso.Where(item => !item._deleted).ToArray();
            if (_qso.Length == 0)
                return true;
            HttpResponseMessage response = await post("log", qsoToken(_qso), true, _qso.Length / 10);
            if (response == null || !response.IsSuccessStatusCode)
                return false;
            else 
            {
                try
                {
                    string strRsp = await response.Content.ReadAsStringAsync();
                    NewQsoResponse[] qsoResponse = JsonConvert.DeserializeObject<NewQsoResponse[]>(strRsp);
                    for (int qsoCnt = 0; qsoCnt < qsoResponse.Length; qsoCnt++)
                        _qso[qsoCnt].serverTs = qsoResponse[qsoCnt].ts;
                }
                catch (Exception e) {
                    logger.Error(e, "Invalid log response");
                }
            }
            return true;
        }

        private async Task<bool> _postDeleteQso(QsoDeleteData qdd)
        {
            HttpResponseMessage response = await post("log", new QsoDeleteRequest(config, qdd));
            if (response == null || !response.IsSuccessStatusCode)
                return false;
            return true;
        }

        public async Task postQso(QSO qso)
        {
            await postQso(new QSO[] { qso });
        }

        public async Task postQso(QSO[] qso)
        {
            if (logQueue.IsEmpty && config.token != null)
            {
                if (!await _postQso(qso))
                    addToQueue(qso);
            }
            else
                addToQueue(qso);
        }

        private void addToQueue(QSO[] qso)
        {
            logQueue.Enqueue(new LogRequest() { qso = qso });
            saveUnsent();
        }

        private void addToQueue(QsoDeleteData qdd)
        {
            logQueue.Enqueue(new LogRequest() { delete = qdd });
            saveUnsent();
        }

        public async Task deleteQso(QSO qso)
        {
            if (qso.serverTs != 0)
            {
                QsoDeleteData qdd = new QsoDeleteData { delete = qso.serverTs };
                if (logQueue.IsEmpty && config.token != null)
                {
                    if (!await _postDeleteQso(qdd))
                        addToQueue(qdd);
                }
                else
                    addToQueue(qdd);
            }
        }


        private void saveUnsent()
        {
            List<QSO> qsoList = new List<QSO>();
            foreach (QSO[] qsoBatch in logQueue.Where(item => item.qso != null).Select(item => item.qso).ToList())
                qsoList.AddRange(qsoBatch);
            ProtoBufSerialization.Write<List<QSO>>(unsentFilePath + ".qso", qsoList);
            ProtoBufSerialization.Write<List<QsoDeleteData>>(unsentFilePath + ".del", logQueue.Where(item => item.delete != null).Select(item => item.delete).ToList());
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


        public async Task ping()
        {
            if (config.token == null)
                return;
            HttpResponseMessage response = await post("location", new StatusData(config));
            if (stationCallsign == null)
                await getUserData();
            if (stationCallsign != null && response != null && response.IsSuccessStatusCode)
            {
                try
                {
                    LocationResponse location = JsonConvert.DeserializeObject<LocationResponse>(await response.Content.ReadAsStringAsync());
                    for (int field = 0; field < TnxlogConfig.QthFieldCount; field++)
                    {
                        if (tnxlogConfig.qthFieldsAuto[field])
                            tnxlog.setQthField(field, location.qth.fields.values[field]);
                        tnxlog.setQthFieldTitle(field, location.qth.fields.titles[field]);
                    }
                    if (tnxlogConfig.locAuto)
                        tnxlog.loc = location.qth.loc;
                }
                catch (Exception e)
                {
                    logger.Error(e, "Invalid location response");
                }
            }
            pingTimer.Change(response != null && response.IsSuccessStatusCode ? config.updateIterval : pingIntervalNoConnection, Timeout.Infinite);
        }

        public async Task<System.Net.HttpStatusCode?> login(bool retry=false)
        {
            if (string.IsNullOrEmpty(config.callsign) || string.IsNullOrEmpty(config.password))
                return null;
            HttpResponseMessage response = await post("login", new LoginRequest() { login = config.callsign, password = config.password }, false);
            bool retryFlag = true;
            if (response != null)
            {
                retryFlag = false;
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        LoginResponse userData = JsonConvert.DeserializeObject<LoginResponse>(await response.Content.ReadAsStringAsync());
                        config.token = userData.token;
                        schedulePingTimer();
                        if (loginRetryTimer != null)
                        {
                            loginRetryTimer.Change(Timeout.Infinite, Timeout.Infinite);
                            loginRetryTimer = null;
                        }
                        await Task.Run(async () => await processQueue());
                    }
                    catch (Exception e)
                    {
                        logger.Error(e, "Invalid login response");
                        retryFlag = true;
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    config.token = null;
                    MessageBox.Show(await response.Content.ReadAsStringAsync(), Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    retryFlag = true;
            }
            if (retry && retryFlag)
                scheduleLoginRetryTimer();
            return response?.StatusCode;
        }

        private void scheduleLoginRetryTimer()
        {
            if (loginRetryTimer == null)
                loginRetryTimer = new System.Threading.Timer(async obj => await login(true), null, pingIntervalNoConnection, Timeout.Infinite);
            else
                loginRetryTimer.Change(pingIntervalNoConnection, Timeout.Infinite);
        }

        public async Task postFreq(string freq)
        {
            await post("location", new FreqData(config, freq));
        }

        private async Task<System.Net.HttpStatusCode?> getUserData()
        {
            HttpResponseMessage response = await post("userData", new JSONToken(config), false);
            try
            {
                if (response != null)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        UserDataResponse userData = JsonConvert.DeserializeObject<UserDataResponse>(await response.Content.ReadAsStringAsync());
                        stationCallsign = userData.settings.station.callsign.ToLower().Replace('/', '-');
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error(e, "Invalid userData response");
            }
            return response?.StatusCode;
        }


        private QSOtoken qsoToken(QSO[] qso)
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

    public class LocationQthFields
    {
        public string[] values;
        public string[] titles;
    }

    public class LocationQth
    {
        public LocationQthFields fields;
        public string loc;
    }
    public class LocationResponse
    {
        public LocationQth qth;
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
        public QsoDeleteData delete;
        public QSO[] qso;
    }

    class QSOtoken : JSONToken
    {
        public QSO[] qso;

        internal QSOtoken(HttpServiceConfig _config, QSO[] _qso) : base(_config)
        {
            qso = _qso;
        }
    }

    [ProtoContract]
    public class QsoDeleteData
    {
        [ProtoMember(1)]
        public decimal delete;
    }

    public class QsoDeleteRequest : JSONToken
    {
        [ProtoMember(1)]
        public decimal delete;
        internal QsoDeleteRequest(HttpServiceConfig _config, QSO qso) : base(_config)
        {
            delete = qso.serverTs;
        }

        internal QsoDeleteRequest(HttpServiceConfig _config, QsoDeleteData qdd) : base(_config)
        {
            delete = qdd.delete;
        }

    }



    class FreqData : JSONToken
    {
        internal FreqData(HttpServiceConfig _config, string _freq) : base(_config) {
            freq = _freq;
        }
        public string freq;
    }

    public class StatusDataQth
    {
        [IgnoreDataMember]
        TnxlogConfig config;

        public Dictionary<string, string> fields
        {
            get
            {
                Dictionary<string, string> r = new Dictionary<string, string>();
                for (int field = 0; field < TnxlogConfig.QthFieldCount; field++)
                    if (!config.qthFieldsAuto[field])
                        r[field.ToString()] = config.qthFields[field];
                return r;
            }
            set { }
        }

        public string loc
        {
            get { return config.loc; } set { }
        }

        public bool ShouldSerializeloc()
        {
            return !config.locAuto;
        }

        internal StatusDataQth(TnxlogConfig _config)
        {
            config = _config;
        }

    }

    public class StatusData : JSONToken
    {
        public StatusDataQth qth;
        public StatusData(HttpServiceConfig _config) : base(_config)
        {
            qth = new StatusDataQth((TnxlogConfig)_config.parent);
        }
    }


}
