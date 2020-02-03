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

namespace tnxlog
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

    public class SerPropEnable
    {
        public string name;
        public bool enabled;
    }

    public class TnxlogConfig : XmlConfig
    {
        public static readonly List<Tuple<string, string>> CwMacrosDefaults = new List<Tuple<string, string>>
        {
            Tuple.Create("CQ", "CQ {MY_CALL} {MY_CALL}"),
            Tuple.Create("5NN", "5NN"),
            Tuple.Create("TU", "{CALL} TU"),
            Tuple.Create("MY", "{MY_CALL} {MY_CALL}"),
            Tuple.Create("HIS", "{CALL}"),
            Tuple.Create("USR", "{USER_FIELD}"),
            Tuple.Create("RDA", "RDA {RDA}"),
            Tuple.Create("RAF", "RAFA {RAFA}"),
            Tuple.Create("LOC", "{LOCATOR}")
        };
        public static readonly List<string> StatusFields = new List<string> { "rda", "rafa", "locator",  };

        public static readonly List<string> MainFormPanels = new List<string> { "statusFields", "statFilter", "callsignId", "cwMacros" };

        public FormMainConfig formMain;
        public HttpServiceConfig httpService;
        public FormLogConfig formLog;
        public TransceiverControllerConfig transceiverController;

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

        private Dictionary<string, bool> _mainFormPanels;
        public List<SerPropEnable> serMainFormPanels;
        [XmlIgnore]
        public EventHandler mainFormPanelVisibleChange;
        public void setMainFormPanelVisible(string panel, bool value)
        {
            if (_mainFormPanels[panel] != value)
            {
                _mainFormPanels[panel] = value;
                write();
                mainFormPanelVisibleChange?.Invoke(this, new EventArgs());
            }
        }
        public bool getMainFormPanelVisible(string panel)
        {
            return _mainFormPanels[panel];
        }

        public bool enableCwMacros = true;
        private int _morseSpeed = 12;
        public int morseSpeed {
            get { return _morseSpeed; }
            set {
                _morseSpeed = value;
                write();
            }
        }

        public int autoCqRxPause = 3;
        public bool autoLogin = true;
        public List<string[]> cwMacros;

        public TnxlogConfig() : base() {}

        public override void initialize()
        {
            if (formMain == null)
                formMain = new FormMainConfig(this);
            else
                formMain.parent = this;

            if (formLog == null)
                formLog = new FormLogConfig(this);
            else
                formLog.parent = this;

            if (httpService == null)
                httpService = new HttpServiceConfig(this);
            else
                httpService.parent = this;

            if (transceiverController == null)
                transceiverController = new TransceiverControllerConfig(this);
            else
                transceiverController.parent = this;

            if (cwMacros == null)
            {
                cwMacros = new List<string[]>();
            }
            for (int co = cwMacros.Count; co < CwMacrosDefaults.Count; co++)
                cwMacros.Add(new string[] { CwMacrosDefaults[co].Item1, CwMacrosDefaults[co].Item2 });

            if (serStatusFields != null)
                _statusFields = serStatusFields.ToDictionary(item => item.field, item => new StatusField() { auto = item.auto, value = item.value });
            else
                _statusFields = new Dictionary<string, StatusField>();
            foreach (string field in StatusFields)
                if (!_statusFields.ContainsKey(field))
                    _statusFields[field] = new StatusField() { auto = true, value = null };

            if (serMainFormPanels != null)
                _mainFormPanels = serMainFormPanels.ToDictionary(item => item.name, item => item.enabled);
            else
                _mainFormPanels = new Dictionary<string, bool>();
            foreach (string panel in MainFormPanels)
                if (!_mainFormPanels.ContainsKey(panel))
                    _mainFormPanels[panel] = true;

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

                serMainFormPanels = _mainFormPanels.Select(item => new SerPropEnable()
                {
                    name = item.Key,
                    enabled = item.Value
                }).ToList();

                base.write();
            }
        }

    }
}
