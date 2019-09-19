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

namespace RdaLog
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
        internal string _oper;
        internal int _no;
        internal string _userField;

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
        public string userField { get { return _userField; } set { _userField = value; } }
        public string freqRx { get { return _freqRx == null ? _freq : _freqRx; } set { _freqRx = value; } }
        public string oper { get { return _oper == null ? _myCS : _oper; } set { _oper = value; } }


        public string toJSON()
        {
            return JSONSerializer.Serialize<QSO>(this);
        }

        public static string formatFreq(string freq)
        {
            return (Convert.ToDouble(Convert.ToInt32(freq)) / 100).ToString("0.00", System.Globalization.NumberFormatInfo.InvariantInfo);
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
                adifField("BAND", band) +
                adifField("STATION_CALLSIGN", myCS) +
                adifField("FREQ", adifFormatFreq(freq)) +
                adifField("FREQ_RX", adifFormatFreq(freqRx)) +
                adifField("MODE", mode) +
                adifField("RST_RCVD", rcv) +
                adifField("RST_SENT", snt) +
                adifField("OPERATOR", oper) +
                adifField("GRIDSQUARE", loc) +
                adifField("RDA", rda) +
                adifField("RAFA", adifParams.ContainsKey("RAFA") ? adifParams["RAFA"] : rafa) +
                adifField("WFF", wff) +
                " <EOR>";
        }
    }

    public class QSOFactory
    {
        private RdaLog qsoClient;
        public int no = 1;



        public QSOFactory(RdaLog _qsoClient)
        {
            qsoClient = _qsoClient;
        }



        public QSO create( string xml )
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlElement root = doc.DocumentElement;

            if (root.Name != "contactinfo")
                return null;

            string mode = root.SelectSingleNode("mode").InnerText;
            if (mode.Equals("RTTY")) mode = "FT8";

            string snt = root.SelectSingleNode("snt").InnerText;
            string rcv = root.SelectSingleNode("rcv").InnerText;

            if (mode.Equals("FT8"))
            {
                snt = "-10";
                rcv = "-10";
            }

            return new QSO {
                _ts = root.SelectSingleNode("timestamp").InnerText,
                _myCS = root.SelectSingleNode("mycall").InnerText,
                _band = root.SelectSingleNode("band").InnerText,
                _freq = QSO.formatFreq(root.SelectSingleNode("txfreq").InnerText),
                _mode = mode,
                _cs = root.SelectSingleNode("call").InnerText,
                _snt = snt,
                _rcv = snt,
                _freqRx = QSO.formatFreq(root.SelectSingleNode("rxfreq").InnerText),
                _oper = root.SelectSingleNode("operator").InnerText,
                _no = no++,
                _rda = qsoClient.rda,
                _rafa = qsoClient.rafa,
                _wff = qsoClient.wff,
                _loc = qsoClient.loc,
                _userField = qsoClient.userField
            };
        }
    }
}
