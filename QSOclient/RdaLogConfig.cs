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
    public class StatusField
    {
        public bool auto = true;
        public string value;
    }

    public class SerStatusField : StatusField
    {
        public string field;
    }

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
        public static readonly List<string> StatusFields = new List<string> { "rda", "rafa", "locator" };

        public FormMainConfig formMain;
        public HttpServiceConfig httpService;

        private Dictionary<string, StatusField> _statusFields;
        public List<SerStatusField> serStatusFields;
        public void setStatusFieldValue(string field, string value)
        {
            if (_statusFields[field].value != value)
            {
                _statusFields[field].value = value;
                write();
            }
        }
        public void setStatusFieldAuto(string field, bool value)
        {
            if (_statusFields[field].auto != value)
            {
                _statusFields[field].auto = value;
                write();
            }
        }
        public string getStatusFieldValue(string field)
        {
            return _statusFields[field].auto ? null : _statusFields[field].value;
        }
        public bool getStatusFieldAuto(string field)
        {
            return _statusFields[field].auto;
        }

        private string _userField;

        public string userField
        {
            get { return _userField; }
            set { if (_userField != value )
                {
                    _userField = value;
                    write();
                } }
        }

        public bool showFields = true;
        public bool showCallsignId = true;
        public bool showStatFilter = true;
        public bool showMacros = true;
        public bool enableMacros = true;
        public bool autoLogin = true;
        public List<string[]> hotKeys;

        public RdaLogConfig() : base() {}

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

            _statusFields = serStatusFields.ToDictionary(item => item.field, item => new StatusField() { auto = item.auto, value = item.value });
            foreach (string field in StatusFields)
                if (!_statusFields.ContainsKey(field))
                    _statusFields[field] = new StatusField() { auto = true, value = null };

            base.initialize();
        }

        public override void write()
        {
            if (initialized)
            {
                serStatusFields = _statusFields.Select(item => new SerStatusField()
                {
                    field = item.Key,
                    auto = item.Value.auto,
                    value = item.Value.value
                }).ToList();
                base.write();
            }
        }

    }
}
