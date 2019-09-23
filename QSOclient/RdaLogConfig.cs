using GPSReaderNS;
using SerializationNS;
using StorableForm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using XmlConfigNS;

namespace RdaLog
{

    [DataContract]
    public class RdaLogConfig : XmlConfig
    {
        public static readonly List<Tuple<string, string>> HotKeysDefaults = new List<Tuple<string, string>>
        {
            Tuple.Create("CQ", "CQ {MY_CALL} {MY_CALL}"),
            Tuple.Create("5NN", "5NN"),
            Tuple.Create("TU", "{CALL} TU"),
            Tuple.Create("MY", "{MY_CALL} {MY_CALL}"),
            Tuple.Create("HIS", "{CALL} {CALL}"),
            Tuple.Create("", ""),
            Tuple.Create("RDA", "RDA {RDA}"),
            Tuple.Create("RAFA", "RAFA {RAFA}"),
            Tuple.Create("", "")
        };
        [XmlIgnore]
        Dictionary<string, string> rafaData = new Dictionary<string, string>();


        [DataMember]
        public FormMainConfig formMain;
        [DataMember]
        public HttpServiceConfig httpService;

        [DataMember]
        public bool showFields = true;
        [DataMember]
        public bool showCallsignId = true;
        [DataMember]
        public bool showStatFilter = true;
        [DataMember]
        public bool showMacros = true;
        [DataMember]
        public bool enableMacros = true;
        [DataMember]
        public bool autoLogin = true;
        [DataMember]
        public List<string[]> hotKeys;


        public RdaLogConfig() : base()
        {
            try
            {
                using (StreamReader sr = new StreamReader(Application.StartupPath + "\\rafa.csv"))
                {
                    do
                    {
                        string line = sr.ReadLine();
                        string[] lineData = line.Split(';');
                        if (lineData[0] == "")
                        {
                            string[] keys = lineData[3].Split(',');
                            foreach (string key in keys)
                            {
                                string entry = lineData[1];
                                if (rafaData.ContainsKey(key))
                                    rafaData[key] += ", " + entry;
                                else
                                    rafaData[key] = entry;
                            }
                        }
                    } while (sr.Peek() >= 0);
                    System.Diagnostics.Debug.WriteLine(rafaData["KN97TF"]);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                MessageBox.Show("Rafa data could not be loaded: " + e.ToString(), "DXpedition", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public override void initialize()
        {
            if (formMain == null)
                formMain = new FormMainConfig(this);
            else
                formMain.parent = this;

            if (httpService == null)
                httpService = new HttpServiceConfig(this);
            else
                httpService.parent = this;

            if (hotKeys == null)
            {
                hotKeys = new List<string[]>();
            }
            for (int co = hotKeys.Count; co < HotKeysDefaults.Count; co++)
                hotKeys.Add(new string[] { HotKeysDefaults[co].Item1, HotKeysDefaults[co].Item2 });
        }

        public string toJSON()
        {
            return JSONSerializer.Serialize<RdaLogConfig>(this);
        }



        public static string qth(Coords c)
        {
            double lat = c.lat;
            double lng = c.lng;
            string qth = "";
            lat += 90;
            lng += 180;
            lat = lat / 10 + 0.0000001;
            lng = lng / 20 + 0.0000001;
            qth += (char)(65 + lng);
            qth += (char)(65 + lat);
            lat = 10 * (lat - Math.Truncate(lat));
            lng = 10 * (lng - Math.Truncate(lng));
            qth += (char)(48 + lng);
            qth += (char)(48 + lat);
            lat = 24 * (lat - Math.Truncate(lat));
            lng = 24 * (lng - Math.Truncate(lng));
            qth += (char)(65 + lng);
            qth += (char)(65 + lat);
            lat = 10 * (lat - Math.Truncate(lat));
            lng = 10 * (lng - Math.Truncate(lng));
            /*            qth += (char)(48 + lng) + (char)(48 + lat);
                        lat = 24 * (lat - Math.Truncate(lat));
                        lng = 24 * (lng - Math.Truncate(lng));
                        qth += (char)(65 + lng) + (char)(65 + lat);*/
            System.Diagnostics.Debug.WriteLine(qth);
            return qth;
        } // returnQth()


    }
}
