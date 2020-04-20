using Newtonsoft.Json;
using StorableForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using XmlConfigNS;
using StringIndexNS;
using HamRadio;
using AutoUpdaterDotNET;
using System.Reflection;
using System.Diagnostics;
using System.Threading;
using System.Collections.Concurrent;

namespace tnxlog
{
    public partial class FormMain : StorableForm.StorableForm<FormMainConfig>, IMessageFilter
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        class QthFieldControls
        {
            internal CheckBox auto;
            internal TextBox value;
            internal Label label;
        }

        class QsoValues
        {
            internal string callsign;
            internal decimal freq;
            internal string mode;
            internal string rstRcvd;
            internal string rstSnt;

            internal bool isEqual(QsoValues a)
            {
                return a != null && a.callsign == callsign && a.freq == freq && a.mode == mode && a.rstRcvd == rstRcvd && a.rstSnt == rstSnt;
            }
        }

        class RegexReplace
        {
            internal Regex re;
            internal string rplcmnt;

            internal string replace(string value)
            {
                return re.Replace(value, rplcmnt);
            }
        }

        class QthRegex
        {
            internal Regex match;
            internal RegexReplace edit;
        }

        static Dictionary<string, QthRegex> QthRegexes = new Dictionary<string, QthRegex>() {
            {"RDA", new QthRegex() {
                match = new Regex(@"[A-Z][A-Z][\- ]?\d\d", RegexOptions.Compiled),
                edit =  new RegexReplace() {
                    re = new Regex(@"([A-Z][A-Z])[\- ]?(\d\d)", RegexOptions.Compiled),
                    rplcmnt = "$1-$2"
                }
            }
            },
            {"Locator", new QthRegex()
            {
                match = new Regex(@"[A-Z][A-Z]\d\d[A-Z][A-Z]", RegexOptions.Compiled)
            } },
            {"RAFA", new QthRegex()
            {
                match = new Regex(@"[A-Z\d]{4}", RegexOptions.Compiled)
            } }

        };

        static readonly Keys[] CwMacrosKeys = new Keys[] { Keys.F1, Keys.F2, Keys.F3, Keys.F4, Keys.F5, Keys.F6, Keys.F7, Keys.F8, Keys.F9 };
#if DEBUG
        static readonly string AutoUpdaterURI = "https://tnxqso.com/static/files/tnxlog_debug.xml";
#else
        static readonly string AutoUpdaterURI = "https://tnxqso.com/static/files/tnxlog.xml";
#endif
        private Tnxlog tnxlog;
        private QthFieldControls[] qthFieldsControls;
        private Dictionary<string, Panel> panels;
        private Dictionary<string, HashSet<string>> qthValues = new Dictionary<string, HashSet<string>>();
        private StringIndex callsignsDb = new StringIndex();
        private StringIndex callsignsQso = new StringIndex();
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private QsoValues qsoValues;
        private InputLanguage englishInputLanguage;
        private CancellationTokenSource tokenSource;
        private List<Label> cwMacrosTitles;
        private System.Threading.Timer autoCqTimer;
        private bool autoCq;
        private ConcurrentQueue<string> cwQueue = new ConcurrentQueue<string>();
        private string clearedCS = "";

        private QsoValues currentQsoValues()
        {
            return new QsoValues() {
                callsign = textBoxCallsign.Text,
                freq = numericUpDownFreq.Value,
                mode = comboBoxMode.Text,
                rstRcvd = textBoxRstRcvd.Text,
                rstSnt = textBoxRstSent.Text
            };
        }
        private TnxlogConfig tnxlogConfig { get { return (TnxlogConfig)config.parent; } }

        private const int WM_LBUTTONDOWN = 0x201;
        private const int WM_RBUTTONDOWN = 0x204;
        public bool PreFilterMessage(ref Message m)
        {
            if ((m.Msg == WM_LBUTTONDOWN || m.Msg == WM_RBUTTONDOWN) && autoCq)
                stopAutoCq();
            return false;
        }
        public FormMain(FormMainConfig _config, Tnxlog _tnxlog) : base(_config)
        {
            tnxlog = _tnxlog;

            InitializeComponent();

            tnxlogConfig.httpService.logInOout += onLogInOut;
            tnxlog.httpService.connectionStateChanged += onLogInOut;

            foreach (InputLanguage iLang in InputLanguage.InstalledInputLanguages)
                if (iLang.Culture.EnglishName.StartsWith("English"))
                {
                    englishInputLanguage = iLang;
                    break;
                }

            try
            {
                string qthValuesStr = File.ReadAllText(Application.StartupPath + @"\qthValues.json");
                Dictionary<string, string[]> qthValuesDict = JsonConvert.DeserializeObject<Dictionary<string,string[]>>(qthValuesStr);
                foreach (string qthField in qthValuesDict.Keys)
                    qthValues[qthField] = new HashSet<string>(qthValuesDict[qthField]);
            }
            catch (Exception e)
            {
                Logger.Error(e, "Error loading QTH fields data");
                MessageBox.Show("QTH fields data could not be loaded: " + e.ToString(), Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                string callsignsStr = File.ReadAllText(Application.StartupPath + @"\callsigns.json");
                foreach (string callsign in JsonConvert.DeserializeObject<string[]>(callsignsStr))
                    callsignsDb.add(callsign);
            }
            catch (Exception e)
            {
                Logger.Error(e, "Error loading callsigns list");
                MessageBox.Show("Callsigns list could not be loaded: " + e.ToString(), Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cwMacrosTitles = new List<Label>() { labelCwMacroF1Title, labelCwMacroF2Title, labelCwMacroF3Title, labelCwMacroF4Title, labelCwMacroF5Title, labelCwMacroF6Title,
                labelCwMacroF7Title, labelCwMacroF8Title, labelCwMacroF9Title };
            updateCwMacrosTitles();
            numericUpDownMorseSpeed.Value = tnxlogConfig.morseSpeed;

            qthFieldsControls = new QthFieldControls[]
                {
                    new QthFieldControls(){
                        auto = checkBoxAutoQth1,
                        value = textBoxQth1,
                        label = labelQth1
                    },
                    new QthFieldControls(){
                        auto = checkBoxAutoQth2,
                        value = textBoxQth2,
                        label = labelQth2
                    },
                    new QthFieldControls(){
                        auto = checkBoxAutoQth3,
                        value = textBoxQth3,
                        label = labelQth3
                    },
                };

            panels = new Dictionary<string, Panel>()
            {
                {"qsoComments", panelQsoComments },
                {"qth1_2", panelQth1_2 },
                {"statFilter", panelStatFilter },
                {"callsignId", panelCallsignId },
                {"cwMacros", panelCwMacro },
                {"qth3Loc", panelQth3Loc }
            };
            arrangePanels();
            tnxlogConfig.mainFormPanelVisibleChange += delegate (object sender, EventArgs e)
            {
                arrangePanels();
            };

            comboBoxStatFilterBand.Items.AddRange(Band.Names);
            comboBoxStatFilterMode.Items.AddRange(HamRadio.Mode.Names);
            buildQsoIndices();

            comboBoxMode.Items.AddRange(HamRadio.Mode.Names);

            comboBoxStatFilterMode.SelectedItem = string.IsNullOrEmpty(config.statFilterMode) || !comboBoxStatFilterMode.Items.Contains(config.statFilterMode) ? 
                comboBoxStatFilterMode.Items[0] : config.statFilterMode;
            comboBoxStatFilterBand.SelectedItem = string.IsNullOrEmpty(config.statFilterBand) || !comboBoxStatFilterBand.Items.Contains(config.statFilterBand) ? 
                comboBoxStatFilterBand.Items[0] : config.statFilterBand;
            comboBoxStatFilterQth.SelectedItem = string.IsNullOrEmpty(config.statFilterQth) || !comboBoxStatFilterQth.Items.Contains(config.statFilterQth) ? 
                comboBoxStatFilterQth.Items[0] : config.statFilterQth;

            checkBoxTop.Checked = config.topmost;
            comboBoxMode.SelectedItem = string.IsNullOrEmpty(config.mode) || !comboBoxMode.Items.Contains(config.mode) ? comboBoxMode.Items[0] : config.mode;
            if (config.freq != 0)
                numericUpDownFreq.Value = config.freq;

            checkBoxAutoStatFilter.Checked = config.statFilterAuto;

            for (int co = 0; co < TnxlogConfig.QthFieldCount; co++)
            {
                int field = co;
                bool auto = tnxlogConfig.getQthFieldAuto(field);
                CheckBox checkBoxAuto = qthFieldsControls[co].auto;
                TextBox textBoxValue = qthFieldsControls[co].value;
                Label label = qthFieldsControls[co].label;
                checkBoxAuto.Checked = auto;
                textBoxValue.Enabled = !auto;
                textBoxValue.Text = tnxlogConfig.getQthFieldValue(field);
                label.Text = tnxlogConfig.qthFieldTitles[field];

                checkBoxAuto.CheckedChanged += delegate (object sender, EventArgs e)
                {
                    tnxlogConfig.setQthFieldAuto(field, checkBoxAuto.Checked);
                    textBoxValue.Enabled = !checkBoxAuto.Checked;
                };

                textBoxValue.Validating += delegate (object sender, CancelEventArgs e)
                {
                    if (this.ActiveControl.Equals(sender))
                        return;
                    if (QthRegexes.ContainsKey(label.Text))
                    {
                        string txt = textBoxValue.Text;
                        try
                        {
                            List<string> qthFieldValues = parseValues(label.Text, ref txt);
                            textBoxValue.Text = String.Join(" ", qthFieldValues);
                        }
                        catch (ArgumentException ex)
                        {
                            txt = ex.Message;
                        }
                        if (txt.Length > 0)
                        {
                            MessageBox.Show($"Invalid {label.Text}: {txt}", Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Cancel = true;
                        }
                    }
                };

                textBoxValue.Validated += delegate (object sender, EventArgs e)
                {
                    if (this.ActiveControl.Equals(sender))
                        return;
                    tnxlog.setQthField(field, textBoxValue.Text);
                };

                textBoxValue.TextChanged += delegate (object sender, EventArgs e)
                {
                    int selStart = textBoxValue.SelectionStart;
                    textBoxValue.Text = textBoxValue.Text.ToUpper();
                    textBoxValue.SelectionStart = selStart;
                };

                tnxlog.qthFieldChange += delegate (object sender, QthFieldChangeEventArgs e)
                {
                    if (e.field == field)
                        DoInvoke(() =>
                        {
                            textBoxValue.Text = e.value;
                        });
                };

                tnxlog.qthFieldTitleChange += delegate (object sender, QthFieldChangeEventArgs e)
                {
                    if (e.field == field)
                        DoInvoke(() =>
                        {
                            label.Text = e.value;
                        });
                };
            }

            tnxlog.locChange += locChange;
            textBoxLocator.Text = tnxlogConfig.loc;
            checkBoxAutoLocator.Checked = tnxlogConfig.locAuto;

            textBoxCallsign.Text = config.callsign;

            tnxlog.qsoList.ListChanged += QsoList_ListChanged;

            timer.Tick += Timer_Tick;
            timer.Interval = 500;
            timer.Enabled = true;

            Text = Assembly.GetExecutingAssembly().GetName().Name + " " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            qsoValues = currentQsoValues();

            connectionStatusLabel.Alignment = ToolStripItemAlignment.Right;

            autoCqTimer = new System.Threading.Timer(async obj => await processCwMacro(tnxlogConfig.cwMacros[0][1]), null, Timeout.Infinite, Timeout.Infinite);
            Application.AddMessageFilter(this);
            updateLabelEsm();

            adifQthMenu();
        }

        private void locChange(object sender, EventArgs e)
        {
            DoInvoke(() =>
            {
                textBoxLocator.Text = tnxlog.loc;
            });
        }

        internal void updateCwMacrosTitles()
        {
            for (int co = 0; co < tnxlogConfig.cwMacros.Count; co++)
                cwMacrosTitles[co].Text = tnxlogConfig.cwMacros[co][0].Length > 3 ? tnxlogConfig.cwMacros[co][0].Substring(0,3) : tnxlogConfig.cwMacros[co][0];
        }

        private void onLogInOut(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(((TnxlogConfig)config.parent).httpService.token))
                loginLabel.Text = "Not logged in.";
            else 
                loginLabel.Text = "Logged in as " + ((TnxlogConfig)config.parent).httpService.callsign.ToUpper() + ".";
            Color backColor = tnxlog.httpService.connected ? Color.Green : Color.Red;
            if (connectionStatusLabel.BackColor != backColor)
                DoInvoke(() => { connectionStatusLabel.BackColor = backColor; });
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            labelDateTime.Text = string.Format("{0:dd MMMM yyyy HH:mm'z'}", DateTime.UtcNow);
        }

        private void QsoList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                QSO qso = tnxlog.qsoList[e.NewIndex];
                indexQso(qso, true);
                DoInvoke(() => {
                    if (QSO.formatFreq(numericUpDownFreq.Value) != qso.freq)
                        numericUpDownFreq.Text = qso.freq;
                    if (qso.mode != comboBoxMode.SelectedItem.ToString())
                        comboBoxMode.SelectedItem = qso.mode;
                });
            }
            else if (e.ListChangedType == ListChangedType.Reset)
                buildQsoIndices();
        }

        private void indexQso(QSO qso, bool updateStatsFlag = false)
        {
            callsignsQso.add(qso.cs);
            if (!string.IsNullOrEmpty(qso.qth[0]))
            {
                bool flag = false;
                string[] values = qso.qth[0].Split(' ');
                foreach (string value in values)
                    if (!comboBoxStatFilterQth.Items.Contains(value))
                        DoInvoke(() =>
                        {
                            comboBoxStatFilterQth.Items.Add(value);
                            if (!flag && checkBoxAutoStatFilter.Checked)
                            {
                                flag = true;
                                comboBoxStatFilterQth.SelectedItem = value;
                            }
                        });
                DoInvoke(() =>
                {
                    if (updateStatsFlag && checkBoxAutoStatFilter.Checked)
                    {
                        if (comboBoxStatFilterBand.SelectedItem.ToString() != qso.band)
                        {
                            comboBoxStatFilterBand.SelectedItem = qso.band;
                            flag = true;
                        }
                        if (comboBoxStatFilterMode.SelectedItem.ToString() != qso.mode)
                        {
                            comboBoxStatFilterMode.SelectedItem = qso.mode;
                            flag = true;
                        }
                    }
                });
                if (!flag && updateStatsFlag)
                    updateStats();
            }
        }

        private void buildQsoIndices()
        {
            DoInvoke(() =>
            {
                if (comboBoxStatFilterQth.Items.Count > 1)
                    for (int co = 1; co < comboBoxStatFilterQth.Items.Count; co++)
                        comboBoxStatFilterQth.Items.RemoveAt(co);
                callsignsQso.clear();
                foreach (QSO qso in tnxlog.qsoList)
                    indexQso(qso);
                setStatFilter();
            });
        }

        private void arrangePanels()
        {
            foreach (Panel panel in panels.Values)
            {
                if (panel.Parent == flowLayoutPanel)
                    flowLayoutPanel.Controls.Remove(panel);
            }
            foreach (string panel in TnxlogConfig.MainFormPanels)
            {
                if (panels.ContainsKey(panel) && tnxlogConfig.getMainFormPanelVisible(panel))
                    flowLayoutPanel.Controls.Add(panels[panel]);
            }
            statusStrip.SendToBack();
        }

        private void ToolStripLabelSettings_Click(object sender, EventArgs e)
        {
            tnxlog.showSettings();
        }

        private void TextBoxCallsign_Validated(object sender, EventArgs e)
        {
            config.callsign = textBoxCallsign.Text;
            config.write();
        }

        private void showBalloon(string text, int msTimeout)
        {
            DoInvoke(() =>
            {
                NotifyIcon notifyIcon = new NotifyIcon();
                notifyIcon.Visible = true;
                notifyIcon.Icon = SystemIcons.Information;
                notifyIcon.BalloonTipTitle = Assembly.GetExecutingAssembly().GetName().Name;
                notifyIcon.BalloonTipText = text;
                notifyIcon.ShowBalloonTip(msTimeout);
                notifyIcon.BalloonTipClosed += delegate (object sender, EventArgs e)
                {
                    notifyIcon.Dispose();
                };
            });
        }

        private void setDefRst()
        {
            string mode = comboBoxMode.SelectedItem.ToString();
            if (HamRadio.Mode.DefRst.ContainsKey(mode))
            {
                textBoxRstSent.Text = HamRadio.Mode.DefRst[mode];
                textBoxRstRcvd.Text = HamRadio.Mode.DefRst[mode];
            }
        }

        private List<string> parseValues(string qthField, ref string values)
        {
            values = values.ToUpper().Trim();
            List<string> result = new List<string>();
            Regex reMatch = null;
            RegexReplace reReplace = null;
            if (QthRegexes.ContainsKey(qthField))
            {
                reMatch = QthRegexes[qthField].match;
                reReplace = QthRegexes[qthField].edit;
            }
            HashSet<string> valuesSet = null;
            if (qthValues.ContainsKey(qthField))
                valuesSet = qthValues[qthField];
            Match match = reMatch.Match(values);
            while (match.Success)
            {
                string matchStr = match.Value;
                if (reReplace != null)
                    matchStr = reReplace.replace(matchStr);
                if (valuesSet == null || valuesSet.Contains(matchStr))
                {
                    result.Add(matchStr);
                    values = values.Replace(match.Value, "");
                }
                else
                    throw new ArgumentException(match.Value);
                match = reMatch.Match(values);
            }
            if (values.Length > 0)
            {
                int trimBegin = 0;
                for (int co = 0; co < values.Length; co++)
                    if (char.IsLetterOrDigit(values[co]))
                        break;
                    else
                        trimBegin++;
                int trimEnd = values.Length - 1;
                for (int co = trimEnd; co > trimBegin; co--)
                    if (char.IsLetterOrDigit(values[co]))
                        break;
                    else
                        trimEnd--;
                if (trimEnd >= trimBegin)
                    values = values.Substring(trimBegin, trimEnd - trimBegin + 1);
                else
                    values = "";
            }
            return result;
        }

        private void TextBoxLocator_Validating(object sender, CancelEventArgs e)
        {
            if (this.ActiveControl.Equals(sender))
                return;
            string txt = textBoxLocator.Text;
            bool ok = false;
            try
            {
                List<string> locators = parseValues("Locator", ref txt);
                if (locators.Count > 0)
                {
                    textBoxLocator.Text = locators[0];
                    ok = true;
                }
            }
            catch (ArgumentException ex)
            {
                txt = ex.Message;
            }
            if (!ok && txt.Length > 0)
            {
                MessageBox.Show("Invalid locator: " + txt, Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }

        }

        private void ToolStripLabelLog_Click(object sender, EventArgs e)
        {
            tnxlog.showFormLog();
        }


        private Dictionary<string, List<QSO>> qsoByLambda (Func<QSO,string> lambda)
        {
            Dictionary<string, List<QSO>> r = new Dictionary<string, List<QSO>>();
            foreach (QSO qso in tnxlog.qsoList)
            {
                string fieldValFull = lambda(qso);
                if (!string.IsNullOrEmpty(fieldValFull))
                {
                    if (!string.IsNullOrEmpty(fieldValFull))
                    {
                        string[] fieldValItems = fieldValFull.Split(new string[] { " " }, StringSplitOptions.None);
                        for (int co = 0; co < fieldValItems.Length; co++)
                        {
                            string valItem = fieldValItems[co];
                            if (!r.ContainsKey(valItem))
                                r[valItem] = new List<QSO>();
                            QSO qsoAdd;
                            if (co == 0)
                                qsoAdd = qso;
                            else {
                                qsoAdd = new QSO
                                {
                                    _ts = DateTime.ParseExact(qso.ts, "yyyy-MM-dd HH:mm:ss",
                                        System.Globalization.CultureInfo.InvariantCulture).AddMinutes(co).ToString("yyyy-MM-dd HH:mm:ss"),
                                    _myCS = qso.myCS,
                                    _band = qso.band,
                                    _freq = qso.freq,
                                    _mode = qso.mode,
                                    _cs = qso.cs,
                                    _snt = qso.snt,
                                    _rcv = qso.rcv,
                                    _freqRx = qso.freq,
                                    _no = qso.no,
                                    _loc = qso.loc,
                                    _qth = new string[TnxlogConfig.QthFieldCount]
                                };
                                for (int qthField = 0; qthField < TnxlogConfig.QthFieldCount; qthField++)
                                    qsoAdd.qth[qthField] = qso.qth[qthField];
                            }
                            r[valItem].Add(qsoAdd);
                        }
                    }
                }
            }
            return r;
        }

        private void writeADIF(string folder, string fileName, List<QSO> _data, Dictionary<string, string> adifParams, bool byCallsigns=false)
        {
            var data = byCallsigns ? _data.GroupBy(item => item.myCS, item => item, (cs, items) => new { callsign = cs, qso = items.ToList() }) :
                _data.GroupBy(item => "", item => item, (cs, items) => new { callsign = cs, qso = items.ToList() });
            DateTime ts = DateTime.UtcNow;
            foreach (var entry in data)
            {
                string entryFileName = string.IsNullOrEmpty(entry.callsign) ? fileName : entry.callsign.Replace('/', '_') + " " + fileName;
                string entryPath = Path.Combine(folder, entryFileName);
                entry.qso.Reverse();
                try
                {
                    using (StreamWriter sw = new StreamWriter(entryPath))
                    {
                        sw.WriteLine("ADIF Export from RDA Log");
                        sw.WriteLine("Logs generated @ {0:yyyy-MM-dd HH:mm:ssZ}", ts);
                        sw.WriteLine("<EOH>");
                        foreach (QSO qso in entry.qso)
                            sw.WriteLine(tnxlog.qsoFactory.adif(qso, adifParams));
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e, "Error while ADIF export to file {file}", entryPath);
                    MessageBox.Show("Can not export to text file: " + e.ToString(), Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        internal void adifQthMenu()
        {
            menuItemAdifExportQth.DropDownItems.Clear();
            for (int field = 0; field < TnxlogConfig.QthFieldCount; field++)
            {
                string adifField = tnxlogConfig.qthAdifFields[field];
                if (!string.IsNullOrEmpty(adifField))
                {
                    int fieldNo = field;
                    ToolStripMenuItem mi = new ToolStripMenuItem();
                    mi.Text = $"By {TnxlogConfig.QthFieldTitle(fieldNo, tnxlogConfig.qthFieldTitles[fieldNo])}";
                    mi.Click += delegate (object sender, EventArgs e)
                    {
                        if (!string.IsNullOrEmpty(config.exportPathQth[fieldNo]))
                            folderBrowserDialog.SelectedPath = config.exportPathQth[fieldNo];
                        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                        {
                            config.exportPathQth[fieldNo] = folderBrowserDialog.SelectedPath;
                            config.write();
                            Dictionary<string, List<QSO>> data = qsoByLambda(qso => qso.qth[fieldNo]);
                            data.Keys.ToList().ForEach(val =>
                            {
                                writeADIF(folderBrowserDialog.SelectedPath, val + ".adi", data[val], new Dictionary<string, string>() { { adifField, val } }, true);
                            });
                        }

                    };
                    menuItemAdifExportQth.DropDownItems.Add(mi);
                }
            }
                
        }
        
        private void CheckBoxAutoStatFilter_CheckedChanged(object sender, EventArgs e)
        {
            setStatFilter();
            if (checkBoxAutoStatFilter.Checked != config.statFilterAuto)
            {
                config.statFilterAuto = checkBoxAutoStatFilter.Checked;
                config.write();
            }
        }


        private void updateListBoxCallsigns(ListBox box, StringIndex callsignsIndex)
        {
            box.ClearSelected();
            box.Items.Clear();
            if (callsignsIndex != null && textBoxCorrespondent.Text.Length > 2)
            {
                string value = textBoxCorrespondent.Text;
                List<string> found = callsignsIndex.search(value);
                if (found != null)
                {
                    found.Sort((a, b) =>
                    {
                        if (a.StartsWith(value) && !b.StartsWith(value))
                            return -1;
                        else if (!a.StartsWith(value) && b.StartsWith(value))
                            return 1;
                        else
                            return a.CompareTo(b);
                    });
                    int maxHints = 5;
                    if (found.Count > maxHints)
                        found.RemoveRange(maxHints, found.Count - maxHints);
                    box.Items.AddRange(found.ToArray());
                }
            }
        }

        private void TextBoxCorrespondent_TextChanged(object sender, EventArgs e)
        {
            if (((TnxlogConfig)config.parent).getMainFormPanelVisible("callsignId"))
            {
                updateListBoxCallsigns(listBoxCallsignsDb, callsignsDb);
                updateListBoxCallsigns(listBoxCallsignsQso, callsignsQso);
            }
            if (textBoxCorrespondent.Text.Length > 2)
            {
                string today = DateTime.UtcNow.ToString("yyyy-MM-dd");
                TnxlogConfig tnxlogConfig = (TnxlogConfig)config.parent;
                if (tnxlog.qsoList.FirstOrDefault(x =>
                {
                    return x.cs == textBoxCorrespondent.Text && x.mode == comboBoxMode.SelectedItem.ToString() && x.band == Band.fromFreq(numericUpDownFreq.Value)
                        && x.qth[0] == tnxlogConfig.getQthFieldValue(0) && x.ts.StartsWith(today);
                }) != null) {
                    toggleDupe(true);
                    return;
                }
            }
            toggleDupe(false);
        }

        private void toggleDupe(bool val)
        {
            labelDupe.Visible = val;
            textBoxCorrespondent.ForeColor = val ? Color.Gray : SystemColors.WindowText;
        }

        private void ListBoxCallsigns_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox box = (ListBox)sender;
            if (box.SelectedItem != null)
            {
                textBoxCorrespondent.Text = box.SelectedItem.ToString();
                box.ClearSelected();
            }

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            AutoUpdater.CheckForUpdateEvent += AutoUpdater_CheckForUpdateEvent;
            AutoUpdater.Start(AutoUpdaterURI);
        }

        private void AutoUpdater_CheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            if (args != null && args.IsUpdateAvailable)
            {
                TopMost = false;
                AutoUpdater.ShowUpdateForm();
                TopMost = config.topmost;
            }
        }

        private void NumericUpDownFreq_TextChanged(object sender, EventArgs e)
        {
            searchDefFreq();
        }

        private void searchDefFreq()
        {
            string sep = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            string searchVal = numericUpDownFreq.Text.Split(sep.ToCharArray())[0];
            TextBox freqTextBox = numericUpDownFreq.Controls.OfType<TextBox>().FirstOrDefault() as TextBox;
            int selStart = freqTextBox.SelectionStart;
            char[] charSrc = searchVal.ToCharArray();
            int dstIdx = 0;
            int srcLen = charSrc.Length;
            for (int i = 0; i < srcLen; i++)
                if (Char.IsDigit(charSrc[i]))
                    charSrc[dstIdx++] = charSrc[i];
                else if (i < selStart)
                    selStart--;
            searchVal = new string(charSrc, 0, dstIdx);
            searchVal = searchVal.TrimStart('0');
            if (searchVal.Length > 1 && comboBoxMode.SelectedIndex != -1 && HamRadio.Mode.DefFreq.ContainsKey(comboBoxMode.SelectedItem.ToString()))
            {
                decimal[] defFreqs = HamRadio.Mode.DefFreq[comboBoxMode.SelectedItem.ToString()].Where(item => item.ToString().StartsWith(searchVal.Substring(0,2))).ToArray();
                if (defFreqs.Length > 0)
                {
                    decimal defFreq = defFreqs.FirstOrDefault(item => Convert.ToInt32(item).ToString().Length == searchVal.Length);
                    if (defFreq == 0)
                        defFreq = defFreqs[0];
                    if (Convert.ToInt32(defFreq).ToString() != searchVal)
                    {
                        numericUpDownFreq.TextChanged -= NumericUpDownFreq_TextChanged;
                        numericUpDownFreq.Text = defFreq.ToString();
                        numericUpDownFreq.TextChanged += NumericUpDownFreq_TextChanged;
                        char[] charFreq = numericUpDownFreq.Text.ToCharArray();
                        for (int i = 0; i < selStart; i++)
                            if (!Char.IsDigit(charFreq[i]))
                                selStart++;
                        numericUpDownFreq.Select(selStart, 0);
                    }
                }
            }
        }

        private void ComboBoxMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mode = comboBoxMode.SelectedItem.ToString();
            config.mode = mode;
            config.write();
            setDefRst();
            setStatFilter();
            searchDefFreq();
            updateLabelEsm();
        }

        private void setStatFilter()
        {
            if (checkBoxAutoStatFilter.Checked)
            {
                object selection = comboBoxStatFilterQth.SelectedItem;
                if (!string.IsNullOrEmpty(textBoxQth1.Text) && (selection == null || string.IsNullOrEmpty(selection.ToString()) ||
                    !textBoxQth1.Text.Contains(selection.ToString())))
                {
                    string[] rdas = textBoxQth1.Text.Split(' ');
                    bool flag = false;
                    foreach (string rda in rdas)
                        if (comboBoxStatFilterQth.Items.Contains(rda))
                        {
                            comboBoxStatFilterQth.SelectedItem = rda;
                            flag = true;
                            break;
                        }
                    if (!flag)
                        comboBoxStatFilterQth.SelectedIndex = 0;
                }
                comboBoxStatFilterMode.SelectedItem = comboBoxMode.Text;
                comboBoxStatFilterBand.SelectedItem = Band.fromFreq(numericUpDownFreq.Value);
            }
        }

        private void updateStats()
        {
            DoInvoke(() =>
            {
                HashSet<string> callsigns = new HashSet<string>();
                int qsoCount = 0;
                foreach (QSO qso in tnxlog.qsoList)
                    if ((comboBoxStatFilterQth.SelectedIndex == 0 || comboBoxStatFilterQth.SelectedItem == null ||
                        (!string.IsNullOrEmpty(qso.qth[0]) && qso.qth[0].Contains(comboBoxStatFilterQth.SelectedItem.ToString()))) &&
                        (comboBoxStatFilterMode.SelectedIndex == 0 || comboBoxStatFilterMode.SelectedItem == null || comboBoxStatFilterMode.SelectedItem.ToString() == qso.mode) &&
                        (comboBoxStatFilterBand.SelectedIndex == 0 || comboBoxStatFilterBand.SelectedItem == null || comboBoxStatFilterBand.SelectedItem.ToString() == qso.band))
                    {
                        qsoCount++;
                        callsigns.Add(qso.cs);
                    }
                labelStatQso.Text = qsoCount.ToString();
                labelStatCallsigns.Text = callsigns.Count.ToString();
            });
        }

        private void StatFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            config.statFilterBand = comboBoxStatFilterBand.SelectedItem.ToString(); 
            config.statFilterMode = comboBoxStatFilterMode.SelectedItem.ToString();
            config.statFilterQth = comboBoxStatFilterQth.SelectedItem.ToString();
            config.write();
            updateStats();
            
        }

        private void CheckBoxTop_CheckedChanged(object sender, EventArgs e)
        {
            config.topmost = checkBoxTop.Checked;
            config.write();
            TopMost = checkBoxTop.Checked;
        }

        private async void ButtonPostFreq_Click(object sender, EventArgs e)
        {
            await tnxlog.postFreq(numericUpDownFreq.Value);
        }


        private void MenuItemAdifExportAll_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(config.exportPath))
                saveFileDialog.FileName = config.exportPath;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                config.exportPath =  saveFileDialog.FileName;
                if (!config.exportPath.EndsWith(".adi"))
                    config.exportPath += ".adi";
                config.write();
                writeADIF("", config.exportPath, tnxlog.qsoList.ToList(), new Dictionary<string, string>());
            }

        }

        private void MenuItemFileClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("All QSO will be deleted. Do you really want to continue?", Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                tnxlog.clearQso();
                buildQsoIndices();
            }
        }

        private async Task processCwMacro(string _macro)
        {
            if (tnxlogConfig.transceiverController.transceiverType != 0 && tnxlog.transceiverController.connected)
            {
                string macro = _macro;
                if (macro.Contains('}'))
                {
                    Dictionary<string, string[]> substs = new Dictionary<string, string[]>()
                                {
                                    { "MY_CALL", new string[] { textBoxCallsign.Text } },
                                    { "CALL", new string[] { textBoxCorrespondent.Text, tnxlog.qsoList.FirstOrDefault()?.cs } },
                                    { "RDA", new string[] { textBoxQth1.Text } },
                                    { "RAFA", new string[] { textBoxQth2.Text } },
                                    { "LOCATOR", new string[] { textBoxLocator.Text } },
                                    { "USER_FIELD", new string[] { textBoxQth3.Text } }
                                };
                    foreach (string subst in substs.Keys)
                    {
                        string tmplt = $"{{{subst}}}";
                        if (macro.Contains(tmplt))
                        {
                            string substStr = substs[subst].FirstOrDefault(item => item != null && item != "");
                            if (substStr == null || substStr == "")
                                return;
                            substStr = substStr.Replace("-", "");
                            macro = macro.Replace(tmplt, substStr);
                        }
                    }
                    if (macro.Contains('{'))
                    {
                        showBalloon($"Bad CW macro template: \"{_macro}\".", 5000);
                        return;
                    }
                }
                if (macro != "")
                    await sendCwMsg(macro);
            }
        }

        private async Task sendCwMsg(string msg)
        {
            if (tnxlog.transceiverController.busy)
                cwQueue.Enqueue(msg);
            else
            {
                await _sendCwMsg(msg);
                while (!cwQueue.IsEmpty)
                {
                    cwQueue.TryDequeue(out string qMsg);
                    if (qMsg != null)
                        await _sendCwMsg(qMsg);
                }
                if (autoCq)
                    autoCqTimer.Change(1000 * tnxlogConfig.autoCqRxPause, Timeout.Infinite);
            }
        }

        private async Task _sendCwMsg(string msg)
        {
            tokenSource = new CancellationTokenSource();
            await Task.Run(() => tnxlog.transceiverController.morseString(msg.ToUpper(), Convert.ToUInt32(1200 / tnxlogConfig.morseSpeed), tokenSource.Token));
        }

        private void updateLabelEsm()
        {
            labelEsm.Visible = tnxlogConfig.esm && comboBoxMode.SelectedItem.ToString() == "CW";
        }

        private async void storeQso()
        {
            if (!textBoxCorrespondent.validCallsign)
            {
                textBoxCorrespondent.Focus();
                return;
            }
            if (!textBoxCallsign.validCallsign)
            {
                MessageBox.Show(string.IsNullOrEmpty(textBoxCallsign.Text) ? "Please enter your callsign." : $"Your callsign {textBoxCallsign.Text} is invalid.",
                    Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBoxCallsign.Focus();
                return;
            }
            if (ActiveControl != textBoxCorrespondent)
            {
                QsoValues newQsoValues = currentQsoValues();
                if (!newQsoValues.isEqual(qsoValues))
                {
                    textBoxCorrespondent.Focus();
                    qsoValues = newQsoValues;
                    return;
                }
            }
            qsoValues = currentQsoValues();
            string correspondent = textBoxCorrespondent.Text;
            string comments = textBoxComments.Text;
            textBoxCorrespondent.Text = "";
            textBoxComments.Text = "";
            textBoxCorrespondent.Focus();
            setDefRst();
            if (labelEsm.Visible)
                await Task.Run(async () => await processCwMacro(tnxlogConfig.esmMacro));
            await tnxlog.newQso(correspondent, qsoValues.callsign, qsoValues.freq, qsoValues.mode, qsoValues.rstRcvd, qsoValues.rstSnt, comments);
        }

        private async void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (autoCq)
                stopAutoCq();
            if (e.KeyData == (Keys.Control | Keys.M)) //ESM switch
            {
                tnxlogConfig.esm = !tnxlogConfig.esm;
                updateLabelEsm();
            }
            else if (e.KeyData == Keys.Enter) //store QSO
                storeQso();
            else if (e.KeyData == (Keys.Control | Keys.Q)) //recall last QSO
            {
                QSO qso = tnxlog.qsoList[0];
                textBoxCallsign.Text = qso.myCS;
                textBoxCorrespondent.Text = qso.cs;
                comboBoxMode.SelectedItem = qso.mode;
                numericUpDownFreq.Value = Convert.ToDecimal(qso.freq, System.Globalization.NumberFormatInfo.InvariantInfo);
                textBoxRstRcvd.Text = qso.rcv;
                textBoxRstSent.Text = qso.snt;
                await tnxlog.deleteQso(qso);
            }
            else if (e.KeyData == (Keys.Control | Keys.K) || e.KeyData == (Keys.Alt | Keys.K)) //manual CW
            {
                FormCwSend fSend = new FormCwSend();
                fSend.Width = Width;
                fSend.TopMost = TopMost;
                fSend.charEntered += async delegate (object _sender, CharEnteredEventArgs _e)
                {
                    await sendCwMsg(new string(_e.Char, 1));
                };
                fSend.ShowDialog();
                fSend.Dispose();
            }
            else if (e.KeyData == Keys.Oemtilde || e.KeyData == (Keys.W | Keys.Alt) || e.KeyData == (Keys.W | Keys.Control))
            //clear/restore corrrespondent field
            {
                if (textBoxCorrespondent.Text != "")
                {
                    clearedCS = textBoxCorrespondent.Text;
                    textBoxCorrespondent.Text = "";
                } else if (clearedCS != "")
                {
                    textBoxCorrespondent.Text = clearedCS;
                    textBoxCorrespondent.SelectionStart = clearedCS.Length;
                    clearedCS = "";
                }
            }           
            else if (e.KeyData == (CwMacrosKeys[0] | Keys.Control))//auto cq toggle
            {
                autoCq = true;
                await processCwMacro(tnxlogConfig.cwMacros[0][1]);
            }
            else 
            {
                int cwMacroIdx = Array.IndexOf(CwMacrosKeys, e.KeyData);
                if (cwMacroIdx != -1) //CW macro
                    await processCwMacro(tnxlogConfig.cwMacros[cwMacroIdx][1]);
                else if (e.KeyData == Keys.Escape) //cancel CW transmission
                {
                    tnxlog.transceiverController.stop();
                    if (tnxlog.transceiverController.busy)
                    {
                        while (!cwQueue.IsEmpty)
                            cwQueue.TryDequeue(out string discard);
                        tokenSource.Cancel();
                    }
                }
            }
        }

        private void stopAutoCq()
        {
            autoCq = false;
            autoCqTimer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        private void FormMain_Activated(object sender, EventArgs e)
        {
            if (!InputLanguage.CurrentInputLanguage.Culture.EnglishName.StartsWith("English") && englishInputLanguage != null)
                InputLanguage.CurrentInputLanguage = englishInputLanguage;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Logger.Info("------------------- Closed by user -------------------------------------");
        }

        private void MenuItemAdifExportLoc_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(config.exportPathLoc))
                folderBrowserDialog.SelectedPath = config.exportPathLoc;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                config.exportPathLoc = folderBrowserDialog.SelectedPath;
                config.write();
                Dictionary<string, List<QSO>> data = qsoByLambda(qso => qso.loc);
                data.Keys.ToList().ForEach(val =>
                {
                    writeADIF(folderBrowserDialog.SelectedPath, val + ".adi", data[val], new Dictionary<string, string>(), true);
                });
            }
        }


        private void NumericUpDownMorseSpeed_ValueChanged(object sender, EventArgs e)
        {
            tnxlogConfig.morseSpeed = Convert.ToInt32(numericUpDownMorseSpeed.Value);
        }

        private void NumericUpDownFreq_ValueChanged(object sender, EventArgs e)
        {
            config.freq = numericUpDownFreq.Value;
            config.write();
            setStatFilter();
        }

        private void TextBoxLocator_TextChanged(object sender, EventArgs e)
        {
            int selStart = textBoxLocator.SelectionStart;
            textBoxLocator.Text = textBoxLocator.Text.ToUpper();
            textBoxLocator.SelectionStart = selStart;
        }

        private void TextBoxLocator_Validated(object sender, EventArgs e)
        {
            if (this.ActiveControl.Equals(sender))
                return;
            tnxlog.loc = textBoxLocator.Text;
        }

        private void CheckBoxAutoLocator_CheckedChanged(object sender, EventArgs e)
        {
            tnxlogConfig.locAuto = checkBoxAutoLocator.Checked;
            textBoxLocator.Enabled = !checkBoxAutoLocator.Checked;
        }

        private async void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAdifImportDialog formAdifImportDialog = new FormAdifImportDialog();
            if (!string.IsNullOrEmpty(config.exportPath))
                formAdifImportDialog.fileName = config.importPath;
            for (int field = 0; field < TnxlogConfig.QthFieldCount; field++)
            {
                formAdifImportDialog.setQthFieldAdifLabel(field, tnxlogConfig.qthFieldTitles[field]);
                formAdifImportDialog.setQthFieldAdif(field, config.importQthFields[field]);
            }
            bool successFlag = false;
            if (formAdifImportDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    config.importPath = formAdifImportDialog.fileName;
                    for (int field = 0; field < TnxlogConfig.QthFieldCount; field++)
                        config.importQthFields[field] = formAdifImportDialog.getQthFieldAdif(field);
                    config.write();
                    using (FileStream stream = File.Open(config.importPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        List<QSO> updatedQsos = new List<QSO>();
                        QSO[] qsos = AdifLogWatcher.adifEntries(stream)
                            .Select(entry => tnxlog.qsoFactory.fromADIF(entry, config.importQthFields))
                            .Where(qso =>
                            {
                                int idx = tnxlog.qsoList.ToList().IndexOf(qso);
                                if (idx != -1)
                                {
                                    QSO listQso = tnxlog.qsoList[idx];
                                    for (int field = 0; field < TnxlogConfig.QthFieldCount; field++)
                                        if (!string.IsNullOrEmpty(qso.qth[field])) {
                                            bool updFlag = false;
                                            if (string.IsNullOrEmpty(listQso.qth[field]))
                                            {
                                                listQso._qth[field] = qso.qth[field];
                                                updFlag = true;
                                            }
                                            else
                                            {
                                                string[] items = qso.qth[field].Split(' ');
                                                foreach (string item in items)
                                                {
                                                    string _item = item.Trim();
                                                    if (!string.IsNullOrEmpty(_item) && !listQso.qth[field].Contains(item))
                                                    {
                                                        listQso._qth[field] += ' ' + item;
                                                        updFlag = true;
                                                    }
                                                }
                                            }
                                            if (updFlag)
                                            {
                                                updatedQsos.Add(listQso);
                                                tnxlog.qsoList.ResetItem(idx);
                                            }
                                        }
                                    return false;
                                }
                                else
                                {
                                    idx = tnxlog.qsoList.ToList().FindIndex(item => string.Compare(qso.ts, item.ts) == -1) + 1;
                                    tnxlog.qsoList.Insert(idx, qso);
                                    return true;
                                }
                            })
                            .ToArray();
                        if (qsos.Length > 0 || updatedQsos.Count > 0)
                        {
                            if (MessageBox.Show($"Found new qsos: {qsos.Length}. Updated QTH fields: {updatedQsos.Count}.\nDo you want to upload imported qsos to Tnxqso.com?\nЗагрузить импортированные qso на Tnxqso.com?",
                                Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                await tnxlog.httpService.postQso(qsos);
                                await tnxlog.httpService.postQso(updatedQsos.ToArray());
                            }
                            successFlag = true;
                        }
                    }
                }
                catch (Exception er)
                {
                    Logger.Error(er, "ADIF import error");
                }
                finally
                {
                    formAdifImportDialog.Dispose();
                    if (!successFlag)
                        MessageBox.Show("No new qsos were found.\nНовые qso не найдены.",
                                    Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private async void uploadAllQSOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await tnxlog.httpService.postQso(tnxlog.qsoList.ToArray());
        }
    }

    [DataContract]
    public class FormMainConfig: StorableFormConfig
    {
        public bool topmost = false;
        public bool statFilterAuto = true;
        public decimal freq = 14000;
        public string[] exportPathQth = new string[TnxlogConfig.QthFieldCount];
        public string[] importQthFields = new string[TnxlogConfig.QthFieldCount];
        public string exportPathLoc;
        public string exportPath;
        public string importPath;
        public string statFilterQth;
        public string statFilterBand;
        public string statFilterMode;
        public string mode;
        public string callsign;

        public FormMainConfig(XmlConfig _parent) : base(_parent) { }

        public FormMainConfig() : base() { }

    }
}
