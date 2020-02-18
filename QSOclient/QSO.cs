using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Xml;
using ProtoBuf;
using SerializationNS;
using System.Globalization;
using HamRadio;

namespace tnxlog
{
    [DataContract, ProtoContract]
    public class QSO
    {
        internal string _ts;
        internal string _myCS;
        internal string _band;
        internal string _freq;
        internal string _mode;
        internal string _cs;
        internal string _snt;
        internal string _rcv;
        internal string _rda;
        internal string _rafa;
        internal string _wff;
        internal string _loc;
        internal string _freqRx;
        internal string _comments;
        internal int _no;
        internal string[] _userFields;
        internal decimal _serverTs = 0;
        internal bool _deleted = false;

        [DataMember, ProtoMember(1)]
        public string ts { get { return _ts; } set { _ts = value; } }

        [DataMember, ProtoMember(2)]
        public string myCS { get { return _myCS; } set { _myCS = value; } }

        [DataMember, ProtoMember(3)]
        public string band { get { return _band; } set { _band = value; } }

        [DataMember, ProtoMember(4)]
        public string freq { get { return _freq; } set { _freq = value; } }

        [DataMember, ProtoMember(5)]
        public string mode { get { return _mode; } set { _mode = value; } }

        [DataMember, ProtoMember(6)]
        public string cs { get { return _cs; } set { _cs = value; } }

        [DataMember, ProtoMember(7)]
        public string snt { get { return _snt; } set { _snt = value; } }

        [DataMember, ProtoMember(8)]
        public string rcv { get { return _rcv; } set { _rcv = value; } }

        [DataMember, ProtoMember(9)]
        public string rda { get { return _rda; } set { _rda = value; } }

        [DataMember, ProtoMember(10)]
        public string wff { get { return _wff; } set { _wff = value; } }

        [DataMember, ProtoMember(11)]
        public int no { get { return _no; } set { _no = value; } }

        [DataMember, ProtoMember(12)]
        public string rafa { get { return _rafa; } set { _rafa = value; } }

        [DataMember, ProtoMember(13)]
        public string loc { get { return _loc; } set { _loc = value; } }

        [DataMember, ProtoMember(14)]
        public string[] userFields { get { return _userFields; } set { _userFields = value; } }

        [DataMember, ProtoMember(15)]
        public string comments { get { return _comments; } set { _comments = value; } }
        [DataMember, ProtoMember(16)]
        public decimal serverTs { get { return _serverTs; } set { _serverTs = value; } }


        [IgnoreDataMember]
        public string userField0
        {
            get
            {
                if (userFields != null && userFields.Length > 0)
                    return userFields[0];
                else
                    return null;
            }
            set {
                if (userFields != null && userFields.Length > 0)
                    userFields[0] = value;
            }
        }

        public static string formatFreq(decimal freq)
        {
            return freq.ToString("0.0", System.Globalization.NumberFormatInfo.InvariantInfo);
        }

        public static string adifField( string name, string value )
        {
            return "<" + name + ":" +  
                ( value == null ? "0>" : value.Length.ToString() + ">" + value ) + 
                " ";
        }

        public static string adifFormatFreq( string freq )
        {
            return ( Convert.ToDouble(freq, System.Globalization.NumberFormatInfo.InvariantInfo) / 1000 
                ).ToString( "0.000000", System.Globalization.NumberFormatInfo.InvariantInfo);
        }

        public string adif(Dictionary<string, string> adifParams)
        {
            string[] dt = ts.Split(' ');
            return
                adifField("CALL", cs) +
                adifField("QSO_DATE", dt[0].Replace("-", "")) +
                adifField("TIME_ON", dt[1].Replace(":", "")) +
                adifField("BAND", Band.waveLength(band)) +
                adifField("STATION_CALLSIGN", myCS) +
                adifField("FREQ", adifFormatFreq(freq)) +
                adifField("MODE", mode) +
                adifField("RST_RCVD", rcv) +
                adifField("RST_SENT", snt) +
                adifField("MY_GRIDSQUARE", loc) +
                adifField("MY_CITY", adifParams.ContainsKey("RDA") ? adifParams["RDA"] : rda) +
                adifField("RAFA", adifParams.ContainsKey("RAFA") ? adifParams["RAFA"] : rafa) +
                " <EOR>";
        }
    }

    public class QSOFactory
    {
        private Tnxlog rdaLog;
        public int no = 1;

        public static string getAdifField(string line, string fieldName)
        {
            int head = line.IndexOf($"<{fieldName}:");
            if (head < 0)
                return null;
            int idx = line.IndexOf('>', head) + 1;
            string result = "";
            while (idx < line.Length && line[idx] != ' ' && line[idx] != '<')
                result += line[idx++];
            return result;
        }


        public QSOFactory(Tnxlog _qsoClient)
        {
            rdaLog = _qsoClient;
        }


        public QSO fromADIF(string adif)
        {
            return new QSO
            {
                _ts = getAdifField(adif, "QSO_DATE") + " " + getAdifField(adif, "TIME_ON"),
                _myCS = getAdifField(adif, "STATION_CALLSIGN"),
                _band = getAdifField(adif, "BAND"),
                _freq = getAdifField(adif, "FREQ"),
                _mode = getAdifField(adif, "MODE"),
                _cs = getAdifField(adif, "CALL"),
                _snt = getAdifField(adif, "RST_SENT"),
                _rcv = getAdifField(adif, "RST_RCVD"),
                _freqRx = getAdifField(adif, "FREQ"),
                _no = no++,
                _rda = rdaLog.getStatusFieldValue("rda"),
                _rafa = rdaLog.getStatusFieldValue("rafa"),
                _loc = rdaLog.getStatusFieldValue("locator"),
                _userFields = new string[] { rdaLog.userField }
            };

        }
        public QSO create(string callsign, string myCallsign, decimal freq, string mode, string rstRcvd, string rstSnt, string comments, DateTime? timestamp = null)
        {           
            return new QSO {
                _ts = (timestamp == null ? DateTime.UtcNow : (DateTime)timestamp).ToString("yyyy-MM-dd HH:mm:ss"),
                _myCS = myCallsign,
                _band = Band.fromFreq(freq),
                _freq = QSO.formatFreq(freq),
                _mode = mode,
                _cs = callsign,
                _snt = rstSnt,
                _rcv = rstRcvd,
                _freqRx = freq.ToString(),
                _no = no++,
                _rda = rdaLog.getStatusFieldValue("rda"),
                _rafa = rdaLog.getStatusFieldValue("rafa"),
                _loc = rdaLog.getStatusFieldValue("locator"),
                _userFields = new string[] { rdaLog.userField },
                _comments = comments
            };
        }
    }
}
