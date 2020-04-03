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
        internal string _loc;
        internal string _loc_rcv;
        internal string _freqRx;
        internal string _comments;
        internal int _no;
        internal string[] _qth;
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
        public int no { get { return _no; } set { _no = value; } }

        [DataMember, ProtoMember(10)]
        public string loc { get { return _loc; } set { _loc = value; } }


        [DataMember, ProtoMember(11)]
        public string comments { get { return _comments; } set { _comments = value; } }
        [DataMember, ProtoMember(12)]
        public decimal serverTs { get { return _serverTs; } set { _serverTs = value; } }

        [DataMember, ProtoMember(13)]
        public string[] qth
        {
            get
            {
                if (_qth == null)
                    return null;
                string[] r = new string[TnxlogConfig.QthFieldCount];
                for (int field = 0; field < TnxlogConfig.QthFieldCount; field++)
                    r[field] = _qth.Length <= field || _qth[field] == null ? "" : _qth[field];
                return r;
            }
            set
            {
                _qth = value;
            }
        }

        [DataMember, ProtoMember(14)]
        public string loc_rcv { get { return _loc_rcv; } set { _loc_rcv = value; } }


        [IgnoreDataMember]
        public string qthField0
        {
            get
            {
                if (qth != null && qth.Length > 0)
                    return qth[0];
                else
                    return null;
            }
            set {
                if (qth == null || qth.Length == 0)
                    qth = new string[] {value, null, null};
                else 
                    qth[0] = value;
            }
        }

        [IgnoreDataMember]
        public string qthField1
        {
            get
            {
                if (qth != null && qth.Length > 1)
                    return qth[1];
                else
                    return null;
            }
            set
            {
                if (qth == null || qth.Length == 1)
                    qth = new string[] {null, value, null};
                else
                    qth[1] = value;
            }
        }
        [IgnoreDataMember]
        public string qthField2
        {
            get
            {
                if (qth != null && qth.Length > 2)
                    return qth[2];
                else
                    return null;
            }
            set
            {
                if (qth == null || qth.Length < 3)
                    qth = new string[] {null, null, value};
                else
                    qth[2] = value;
            }
        }


        public static string formatFreq(decimal freq)
        {
            return freq.ToString("0.0", System.Globalization.NumberFormatInfo.InvariantInfo);
        }


    }

    public class QSOFactory
    {
        private Tnxlog tnxlog;
        public int no = 1;

        public static T ReadList<T>(string filePath) where T : IList<QSO>
        {
            T r = ProtoBufSerialization.Read<T>(filePath);
            if (r != null)
                foreach (QSO qso in r)
                {
                    if (qso.qth == null || qso.qth.Length < TnxlogConfig.QthFieldCount)
                    {
                        string[] src = qso.qth == null ? new string[] { } : qso.qth;
                        qso.qth = new string[TnxlogConfig.QthFieldCount];
                        for (int field = 0; field < src.Length; field++)
                            qso.qth[field] = src[field];
                    }
                }
            return r;
        }

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
        public static string adifField(string name, string value)
        {
            return "<" + name + ":" +
                (value == null ? "0>" : value.Length.ToString() + ">" + value) +
                " ";
        }

        public static string adifFormatFreq(string freq)
        {
            return (Convert.ToDouble(freq, System.Globalization.NumberFormatInfo.InvariantInfo) / 1000
                ).ToString("0.000000", System.Globalization.NumberFormatInfo.InvariantInfo);
        }

        public string adif(QSO qso, Dictionary<string, string> adifParams)
        {
            string[] dt = qso.ts.Split(' ');
            string r = adifField("CALL", qso.cs) +
                adifField("QSO_DATE", dt[0].Replace("-", "")) +
                adifField("TIME_ON", dt[1].Replace(":", "")) +
                adifField("BAND", Band.waveLength(qso.band)) +
                adifField("STATION_CALLSIGN", qso.myCS) +
                adifField("FREQ", adifFormatFreq(qso.freq)) +
                adifField("MODE", qso.mode) +
                adifField("RST_RCVD", qso.rcv) +
                adifField("RST_SENT", qso.snt) +
                adifField("MY_GRIDSQUARE", qso.loc) +
                adifField("GRIDSQUARE", qso.loc_rcv);
            for (int field = 0; field < TnxlogConfig.QthFieldCount; field++) {
                string fieldName = tnxlog.config.qthAdifFields[field];
                if (!string.IsNullOrEmpty(fieldName))
                    r += adifField(fieldName, adifParams.ContainsKey(fieldName) ? adifParams[fieldName] : qso.qth[field]);
            }
            r += " <EOR>";
            return r;
        }



        public QSOFactory(Tnxlog _qsoClient)
        {
            tnxlog = _qsoClient;
        }


        public QSO fromADIF(string adif)
        {
            string date = getAdifField(adif, "QSO_DATE");
            string time = getAdifField(adif, "TIME_ON");
            string myCs = getAdifField(adif, "STATION_CALLSIGN");
            if (string.IsNullOrEmpty(myCs))
                myCs = getAdifField(adif, "OPERATOR");
            decimal freq = Convert.ToDecimal(getAdifField(adif, "FREQ"), System.Globalization.NumberFormatInfo.InvariantInfo) * 1000;
            string band = Band.fromFreq(freq);
            string mode = getAdifField(adif, "MODE");
            string submode = getAdifField(adif, "SUBMODE");
            if (!string.IsNullOrEmpty(submode) && Mode.Names.Contains(submode))
                mode = submode;
            if (Mode.DefFreq.ContainsKey(mode))
            {
                decimal defFreq = Mode.DefFreq[mode].FirstOrDefault(item => Band.fromFreq(item) == band);
                if (defFreq != 0)
                    freq = defFreq;
            }
            QSO qso = new QSO
            {
                _ts = $"{date.Substring(0, 4)}-{date.Substring(4, 2)}-{date.Substring(6, 2)} {time.Substring(0, 2)}:{time.Substring(2, 2)}:00",
                _myCS = myCs,
                _band = band,
                _freq = QSO.formatFreq(freq),
                _mode = mode,
                _cs = getAdifField(adif, "CALL"),
                _snt = getAdifField(adif, "RST_SENT"),
                _rcv = getAdifField(adif, "RST_RCVD"),
                _freqRx = getAdifField(adif, "FREQ"),
                _no = no++,
                _loc = tnxlog.loc,
                _loc_rcv = getAdifField(adif, "GRIDSQUARE"),
                _qth = new string[TnxlogConfig.QthFieldCount]
            };
            for (int field = 0; field < TnxlogConfig.QthFieldCount; field++)
                qso._qth[field] = tnxlog.getQthFieldValue(field);
            return qso;
        }
        public QSO create(string callsign, string myCallsign, decimal freq, string mode, string rstRcvd, string rstSnt, string comments, DateTime? timestamp = null)
        {           
            QSO qso = new QSO {
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
                _loc = tnxlog.loc,
                _comments = comments,
                _qth = new string[TnxlogConfig.QthFieldCount]
            };
            for (int field = 0; field < TnxlogConfig.QthFieldCount; field++)
                qso._qth[field] = tnxlog.getQthFieldValue(field);
            return qso;
        }
    }
}
