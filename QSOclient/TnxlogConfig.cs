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
            Tuple.Create("LOC", "{LOCATOR} {COMMENT}")
        };

        public static readonly List<string> MainFormPanels = new List<string> { "qsoComments", "qth1_2", "qth3Loc", "statFilter", "callsignId", "cwMacros" };
        public static readonly int QthFieldCount = 3;
        public static readonly string qthFieldDefTitle = "QTH field";
        public static string QthFieldTitle(int field, string value)
        {
            return value == qthFieldDefTitle ? $"{qthFieldDefTitle} {field + 1}" : value;
        }

        public FormMainConfig formMain;
        public HttpServiceConfig httpService;
        public FormLogConfig formLog;
        public TransceiverControllerConfig transceiverController;

        public string[] qthFields = new string[QthFieldCount];
        public void setQthFieldValue(int field, string value)
        {
            if (qthFields[field] != value)
            {
                qthFields[field] = value;
                write();
            }
        }

        public string[] qthFieldTitles = new string[] { qthFieldDefTitle, qthFieldDefTitle, qthFieldDefTitle };

        public void setQthFieldTitle(int field, string value)
        {
            if (qthFieldTitles[field] != value)
            {
                qthFieldTitles[field] = value;
                write();
            }
        }

        public bool[] qthFieldsAuto = new bool[QthFieldCount];
        public void setQthFieldAuto(int field, bool value)
        {
            if (qthFieldsAuto[field] != value)
            {
                qthFieldsAuto[field] = value;
                write();
            }
        }
        public string getQthFieldValue(int field)
        {
            return qthFieldsAuto[field] ? null : qthFields[field];
        }
        public bool getQthFieldAuto(int field)
        {
            return qthFieldsAuto[field];
        }

        private string _loc;
        public string loc
        {
            get { return _loc; }
            set { _loc = value; write(); }
        }

        private bool _locAuto;
        public bool locAuto
        {
            get { return _locAuto; }
            set { _locAuto = value; write(); }
        }

        public string[] qthAdifFields = new string[] { "QTH_FIELD_1", "QTH_FIELD_2", "QTH_FIELD_3" };
        public string commentAdifField = "COMMENT";

        public string esmMacro = "{CALL} TU DE {MY_CALL}";

        private bool _esm = false;
        public bool esm
        {
            get { return _esm; }
            set { _esm = value; write(); }
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

        private int _morseSpeed = 12;
        public int morseSpeed {
            get { return _morseSpeed; }
            set {
                _morseSpeed = value;
                write();
            }
        }

        public bool watchAdifLog = false;
        public string watchAdifLogPath = "";

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
