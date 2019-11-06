//#define DISABLE_HTTP
//#define TEST_SRV
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

namespace RdaLog
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
        ConcurrentQueue<QSO> qsoQueue = new ConcurrentQueue<QSO>();
        private string unsentFilePath = Application.StartupPath + "\\unsent.dat";
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
        private RdaLog rdaLog;
        public bool gpsServerLoad;


        public HttpService(HttpServiceConfig _config, RdaLog _rdaLog)
        {
            config = _config;
            rdaLog = _rdaLog;
            //schedulePingTimer();
            List<QSO> unsentQSOs = ProtoBufSerialization.Read<List<QSO>>(unsentFilePath);
            if (unsentQSOs != null && unsentQSOs.Count > 0)
                Task.Run(() =>
               {
                   foreach (QSO qso in unsentQSOs)
                       postQso(qso);
                   saveUnsent();
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


        public async Task postQso(QSO qso)
        {
            if (qsoQueue.IsEmpty && config.token != null)
            {
                HttpResponseMessage response = await post("log", qsoToken(qso));
                if (response == null || !response.IsSuccessStatusCode)
                    addToQueue(qso);
            }
            else
                addToQueue(qso);
        }

        private void addToQueue(QSO qso)
        {
            qsoQueue.Enqueue(qso);
            saveUnsent();
        }

        private void saveUnsent()
        {
            ProtoBufSerialization.Write<List<QSO>>(unsentFilePath, qsoQueue.ToList());
        }

        private async Task processQueue()
        {
            while (!qsoQueue.IsEmpty && config.token != null)
            {
                qsoQueue.TryPeek(out QSO qso);
                HttpResponseMessage r = await post("log", qsoToken(qso));
                if (r != null && r.IsSuccessStatusCode)
                {
                    qsoQueue.TryDequeue(out qso);
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

        public async Task<System.Net.HttpStatusCode?> login()
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
                    Task.Run(() => processQueue());
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    config.token = null;
                    MessageBox.Show(await response.Content.ReadAsStringAsync(), "RDA Log", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return response?.StatusCode;
        }

        public async Task postFreq(decimal freq)
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

    class QSOtoken : JSONToken
    {
        public QSO qso;

        internal QSOtoken(HttpServiceConfig _config, QSO _qso) : base(_config)
        {
            qso = _qso;
        }
    }

    class FreqData : JSONToken
    {
        internal FreqData(HttpServiceConfig _config, decimal _freq) : base(_config) {
            freq = _freq;
        }
        public decimal freq;
    }

    class StatusData : JSONToken
    {
        public string loc { get { return ((RdaLogConfig)config.parent).getStatusFieldValue("locator"); } set { } }
        public bool ShouldSerializeloc()
        {
            return !((RdaLogConfig)config.parent).getStatusFieldAuto("locator");
        }
        public string rafa { get { return ((RdaLogConfig)config.parent).getStatusFieldValue("rafa"); } set { } }
        public bool ShouldSerializerafa()
        {
            return !((RdaLogConfig)config.parent).getStatusFieldAuto("rafa");
        }

        public string rda { get { return ((RdaLogConfig)config.parent).getStatusFieldValue("rda"); } set { } }
        public bool ShouldSerializerda()
        {
            return !((RdaLogConfig)config.parent).getStatusFieldAuto("rda");
        }

        public string userField { get { return ((RdaLogConfig)config.parent).userField; } set { } }

        internal StatusData(HttpServiceConfig _config) : base(_config) { }

    }

}
