//#define DISABLE_HTTP
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
using GPSReaderNS;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;
using System.Xml.Serialization;

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
        public bool connected { get { return _connected; } }
        public EventHandler<EventArgs> connectionStateChanged;
        private GPSReader gpsReader;
        //private DXpConfig config;
        public EventHandler<LocationChangedEventArgs> locationChanged;
        private HttpServiceConfig config;
        private RdaLog rdaLog;
        public bool gpsServerLoad;


        public HttpService(HttpServiceConfig _config, RdaLog _rdaLog)
        {
            config = _config;
            _rdaLog = rdaLog;
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

        private async Task<HttpResponseMessage> post(string _URI, JSONSerializable data)
        {
            return await post(_URI, data, true);
        }

        private async Task<HttpResponseMessage> post(string _URI, JSONSerializable data, bool warnings)
        {
            string sContent = data.serialize();
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
            if (_connected != result)
            {
                _connected = result;
                if (_connected)
                    await processQueue();
                connectionStateChanged?.Invoke(this, new EventArgs());
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
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(LocationResponse));
                    double[] location = ((LocationResponse)serializer.ReadObject(await response.Content.ReadAsStreamAsync())).location;
                    Coords coords = rdaLog.coords;
                    if (location != null && ( location[0] != coords.lat || location[1] != coords.lng))
                    {
                        coords.setLat(location[0]);
                        coords.setLng(location[1]);
                        System.Diagnostics.Debug.WriteLine("New location: " + coords.ToString());
                        locationChanged?.Invoke(this, new LocationChangedEventArgs() { coords = coords });
                    }
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
            if (gpsServerLoad)
            {
                if (stationCallsign != null)
                    await getLocation();
                await getUserData();
            }
            pingTimer.Change(response != null && response.IsSuccessStatusCode ? pingIntervalDef : pingIntervalNoConnection, Timeout.Infinite);
        }

        public async Task<System.Net.HttpStatusCode?> login()
        {
            if (config.callsign.Equals(string.Empty) || config.password.Equals(string.Empty))
                return null;
            HttpResponseMessage response = await post("login", new LoginRequest() { login = config.callsign, password = config.password }, false);
            if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(LoginResponse));
                    LoginResponse userData = (LoginResponse)serializer.ReadObject(await response.Content.ReadAsStreamAsync());
                    config.token = userData.token;
                    schedulePingTimer();
                    Task.Run(() => processQueue());
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    MessageBox.Show(await response.Content.ReadAsStringAsync(), "Bad request to server");
                }
            }
            return response?.StatusCode;
        }

        private async Task<System.Net.HttpStatusCode?> getUserData()
        {
            HttpResponseMessage response = await post("userData", new JSONToken(config), false);
            if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(UserDataResponse));
                    UserDataResponse userData = (UserDataResponse)serializer.ReadObject(await response.Content.ReadAsStreamAsync());
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

    [DataContract]
    public class JSONSerializable
    {
        public string serialize()
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(this.GetType());
                string output = string.Empty;

                using (MemoryStream ms = new MemoryStream())
                {
                    ser.WriteObject(ms, this);
                    output = Encoding.UTF8.GetString(ms.ToArray());
                }
                return output;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            return string.Empty;
        }

    }

    [DataContract]
    public class JSONToken : JSONSerializable
    {
        [IgnoreDataMember]
        internal HttpServiceConfig config;
        [DataMember]
        internal string token { get { return config.token; } set { } }

        public JSONToken(HttpServiceConfig _config)
        {
            config = _config;
        }
    }

    [DataContract]
    public class LoginRequest : JSONSerializable
    {
        [DataMember]
        public string login;
        [DataMember]
        public string password;
    }

    [DataContract]
    public class LocationResponse
    {
        [DataMember]
        public double[] location;
    }

    [DataContract]
    public class LoginResponse
    {
        [DataMember]
        public string token;
        [DataMember]
        public string callsign;
    }

    [DataContract]
    public class StationSettings
    {
        [DataMember]
        public string callsign;
    }

    [DataContract]
    public class UserSettings
    {
        [DataMember]
        public StationSettings station;
    }


    [DataContract]
    public class UserDataResponse
    {
        [DataMember]
        public UserSettings settings;
    }

    [DataContract]
    class QSOtoken : JSONToken
    {
        [DataMember]
        internal QSO qso;

        internal QSOtoken(HttpServiceConfig _config, QSO _qso) : base(_config)
        {
            qso = _qso;
        }
    }

    [DataContract]
    class StatusData : JSONToken
    {
        [DataMember]
        public string loc { get { return ((RdaLogConfig)config.parent).getStatusFieldValue("locator"); } set { } }
        [DataMember]
        public string rafa { get { return ((RdaLogConfig)config.parent).getStatusFieldValue("rafa"); } set { } }
        [DataMember]
        public string rda { get { return ((RdaLogConfig)config.parent).getStatusFieldValue("rda"); } set { } }
        [DataMember]
        public string userField { get { return ((RdaLogConfig)config.parent).userField; } set { } }

        internal StatusData(HttpServiceConfig _config) : base(_config) { }

    }

}
