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
        class StatusFieldControls
        {
            internal CheckBox auto;
            internal TextBox value;
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

        static Regex RdaMatchRegex = new Regex(@"[A-Z][A-Z][\- ]?\d\d", RegexOptions.Compiled);
        static Regex LocatorRegex = new Regex(@"[A-Z][A-Z]\d\d[A-Z][A-Z]", RegexOptions.Compiled);
        static Regex RafaRegex = new Regex(@"[A-Z\d]{4}", RegexOptions.Compiled);
        static RegexReplace RdaEditRegex = new RegexReplace()
        {
            re = new Regex(@"([A-Z][A-Z])[\- ]?(\d\d)", RegexOptions.Compiled),
            rplcmnt = "$1-$2"
        };

        static readonly Keys[] CwMacrosKeys = new Keys[] { Keys.F1, Keys.F2, Keys.F3, Keys.F4, Keys.F5, Keys.F6, Keys.F7, Keys.F8, Keys.F9 };

        static readonly string AutoUpdaterURI = "http://tnxqso.com/static/files/tnxlog.xml";
        private Tnxlog tnxlog;
        private Dictionary<string, StatusFieldControls> statusFieldsControls;
        private Dictionary<string, Panel> panels;
        private HashSet<string> rdaValues;
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
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_LBUTTONDOWN && autoCq)
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
                string rdaValuesStr = File.ReadAllText(Application.StartupPath + @"\rdaValues.json");
                rdaValues = new HashSet<string>(JsonConvert.DeserializeObject<string[]>(rdaValuesStr));
            }
            catch (Exception e)
            {
                Logger.Error(e, "Error loading RDA data");
                MessageBox.Show("RDA data could not be loaded: " + e.ToString(), Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            statusFieldsControls = new Dictionary<string, StatusFieldControls>()
                {
                    {"rda", new StatusFieldControls(){
                        auto = checkBoxAutoRda,
                        value = textBoxRda
                    } },
                    {"rafa", new StatusFieldControls(){
                        auto = checkBoxAutoRafa,
                        value = textBoxRafa
                    } },
                    {"locator", new StatusFieldControls(){
                        auto = checkBoxAutoLocator,
                        value = textBoxLocator
                    } }
                };

            panels = new Dictionary<string, Panel>()
            {
                {"statusFields", panelStatusFields },
                {"statFilter", panelStatFilter },
                {"callsignId", panelCallsignId },
                {"cwMacros", panelCwMacro }
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
            comboBoxStatFilterRda.SelectedItem = string.IsNullOrEmpty(config.statFilterRda) || !comboBoxStatFilterRda.Items.Contains(config.statFilterRda) ? 
                comboBoxStatFilterRda.Items[0] : config.statFilterRda;

            checkBoxTop.Checked = config.topmost;
            comboBoxMode.SelectedItem = string.IsNullOrEmpty(config.mode) || !comboBoxMode.Items.Contains(config.mode) ? comboBoxMode.Items[0] : config.mode;
            if (config.freq != 0)
                numericUpDownFreq.Value = config.freq;

            checkBoxAutoStatFilter.Checked = config.statFilterAuto;

            foreach (KeyValuePair<string, StatusFieldControls> item in statusFieldsControls)
            {
                string field = item.Key;
                bool auto = tnxlogConfig.getStatusFieldAuto(field);
                CheckBox checkBoxAuto = item.Value.auto;
                TextBox textBoxValue = item.Value.value;
                checkBoxAuto.Checked = auto;
                textBoxValue.Enabled = !auto;
                textBoxValue.Text = tnxlogConfig.getStatusFieldValue(field);
                checkBoxAuto.CheckedChanged += delegate (object sender, EventArgs e)
                {
                    tnxlogConfig.setStatusFieldAuto(field, checkBoxAuto.Checked);
                    textBoxValue.Enabled = !checkBoxAuto.Checked;
                };
                textBoxValue.Validated += delegate (object sender, EventArgs e)
                {
                    tnxlog.setStatusFieldValue(field, textBoxValue.Text);
                };
                textBoxValue.TextChanged += delegate (object sender, EventArgs e)
                {
                    int selStart = textBoxValue.SelectionStart;
                    textBoxValue.Text = textBoxValue.Text.ToUpper();
                    textBoxValue.SelectionStart = selStart;
                };

                tnxlog.statusFieldChange += delegate (object sender, StatusFieldChangeEventArgs e)
                {
                    if (e.field == field)
                        DoInvoke(() =>
                        {
                            textBoxValue.Text = e.value;
                        });
                };
            }

            tnxlog.statusFieldChange += rdaLog_statusFieldChange;

            textBoxUserField.Text = tnxlogConfig.userField;
            textBoxCallsign.Text = config.callsign;

            tnxlog.qsoList.ListChanged += QsoList_ListChanged;

            timer.Tick += Timer_Tick;
            timer.Interval = 500;
            timer.Enabled = true;

            Text = Assembly.GetExecutingAssembly().GetName().Name + " " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            qsoValues = currentQsoValues();

            connectionStatusLabel.Alignment = ToolStripItemAlignment.Right;

            autoCqTimer = new System.Threading.Timer(async obj => await sendCwMsg(tnxlogConfig.cwMacros[0][1]), null, Timeout.Infinite, Timeout.Infinite);
            Application.AddMessageFilter(this);
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

        private void rdaLog_statusFieldChange (object sender, StatusFieldChangeEventArgs e)
        {
            string value = string.IsNullOrEmpty(e.value) ? "N/A" : e.value;
            //showBalloon($"New {e.field}: {value}", 20 * 1000);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            labelDateTime.Text = string.Format("{0:dd MMMM yyyy HH:mm'z'}", DateTime.UtcNow);
        }

        private void QsoList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded)
                indexQso(tnxlog.qsoList[e.NewIndex], true);
            else if (e.ListChangedType == ListChangedType.Reset)
                buildQsoIndices();
        }

        private void indexQso(QSO qso, bool updateStatsFlag = false)
        {
            callsignsQso.add(qso.cs);
            if (!string.IsNullOrEmpty(qso.rda))
            {
                bool flag = false;
                string[] rdas = qso.rda.Split(' ');
                foreach (string rda in rdas)
                    if (!comboBoxStatFilterRda.Items.Contains(rda))
                    {
                        comboBoxStatFilterRda.Items.Add(rda);
                        if (!flag)
                        {
                            flag = true;
                            comboBoxStatFilterRda.SelectedItem = rda;
                        }
                    }
                if (!flag && updateStatsFlag)
                    updateStats();
            }
        }

        private void buildQsoIndices()
        {
            if (comboBoxStatFilterRda.Items.Count > 1)
                for (int co = 1; co < comboBoxStatFilterRda.Items.Count; co++)
                    comboBoxStatFilterRda.Items.RemoveAt(co);
            callsignsQso.clear();
            foreach (QSO qso in tnxlog.qsoList)
                indexQso(qso);
            setStatFilter();
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

        private async void FormMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
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
                await tnxlog.newQso(correspondent, qsoValues.callsign, qsoValues.freq, qsoValues.mode, qsoValues.rstRcvd, qsoValues.rstSnt, comments);                
            }
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


        private List<string> parseValues(Regex reMatch, ref string values, HashSet<string> valuesSet, RegexReplace reReplace)
        {
            values = values.ToUpper().Trim();
            List<string> result = new List<string>();
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

        private void TextBoxRda_Validating(object sender, CancelEventArgs e)
        {
            string txt = textBoxRda.Text;
            try
            {
                List<string> rdas = parseValues(RdaMatchRegex, ref txt, rdaValues, RdaEditRegex);
                textBoxRda.Text = String.Join(" ", rdas);
            }
            catch (ArgumentException ex)
            {
                txt = ex.Message;
            }
            if (txt.Length > 0)
            {
                MessageBox.Show("Invalid rda: " + txt, Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void TextBoxLocator_Validating(object sender, CancelEventArgs e)
        {
            string txt = textBoxLocator.Text;
            bool ok = false;
            try
            {
                List<string> locators = parseValues(LocatorRegex, ref txt, null, null);
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

        private void MenuItemAdifExportRda_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(config.exportPathRda))
                folderBrowserDialog.SelectedPath = config.exportPathRda;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                config.exportPathRda = folderBrowserDialog.SelectedPath;
                config.write();
                Dictionary<string, List<QSO>> data = qsoByField("rda");
                data.Keys.ToList().ForEach(val =>
                {
                    writeADIF(folderBrowserDialog.SelectedPath, val + ".adi", data[val], new Dictionary<string, string>() { { "RDA", val } }, true);
                });
            }
        }

        private void MenuItemAdifExportRafa_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(config.exportPathRafa))
                folderBrowserDialog.SelectedPath = config.exportPathRafa;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                config.exportPathRafa = folderBrowserDialog.SelectedPath;
                config.write();
                Dictionary<string, List<QSO>> data = qsoByField("rafa");
                data.Keys.ToList().ForEach(val =>
                {
                    writeADIF(folderBrowserDialog.SelectedPath, val + ".adi", data[val], new Dictionary<string, string>() { { "RAFA", val } }, true);
                });
            }
        }

        private Dictionary<string, List<QSO>> qsoByField (string field)
        {
            Dictionary<string, List<QSO>> r = new Dictionary<string, List<QSO>>();
            foreach (QSO qso in tnxlog.qsoList)
            {
                var varVal = qso.GetType().GetProperty(field).GetValue(qso, null);
                if (varVal != null)
                {
                    string fieldValFull = varVal.ToString();
                    if (!string.IsNullOrEmpty(fieldValFull))
                    {
                        string[] fieldValItems = fieldValFull.Split(new string[] { " " }, StringSplitOptions.None);
                        for (int co = 0; co < fieldValItems.Length; co++)
                        {
                            string valItem = fieldValItems[co];
                            if (!r.ContainsKey(valItem))
                                r[valItem] = new List<QSO>();
                            r[valItem].Add(co == 0 ? qso :
                                new QSO
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
                                    _rda = qso.rda,
                                    _rafa = qso.rafa,
                                    _loc = qso.loc,
                                    _userFields = qso.userFields
                                });
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
                            sw.WriteLine(qso.adif(adifParams));
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e, "Error while ADIF export to file {file}", entryPath);
                    MessageBox.Show("Can not export to text file: " + e.ToString(), Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        && x.rda == tnxlogConfig.getStatusFieldValue("rda") && x.ts.StartsWith(today);
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

        private void NumericUpDownFreq_ValueChanged(object sender, EventArgs e)
        {
            config.freq = numericUpDownFreq.Value;
            config.write();
            setStatFilter();
        }

        private void ComboBoxMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mode = comboBoxMode.SelectedItem.ToString();
            config.mode = mode;
            config.write();
            setDefRst();
            setStatFilter();
        }

        private void setStatFilter()
        {
            if (checkBoxAutoStatFilter.Checked)
            {
                object selection = comboBoxStatFilterRda.SelectedItem;
                if (!string.IsNullOrEmpty(textBoxRda.Text) && (selection == null || string.IsNullOrEmpty(selection.ToString()) ||
                    !textBoxRda.Text.Contains(selection.ToString())))
                {
                    string[] rdas = textBoxRda.Text.Split(' ');
                    bool flag = false;
                    foreach (string rda in rdas)
                        if (comboBoxStatFilterRda.Items.Contains(rda))
                        {
                            comboBoxStatFilterRda.SelectedItem = rda;
                            flag = true;
                            break;
                        }
                    if (!flag)
                        comboBoxStatFilterRda.SelectedIndex = 0;
                }
                comboBoxStatFilterMode.SelectedItem = comboBoxMode.Text;
                comboBoxStatFilterBand.SelectedItem = Band.fromFreq(numericUpDownFreq.Value);
            }
        }

        private void updateStats()
        {
            HashSet<string> callsigns = new HashSet<string>();
            int qsoCount = 0;
            foreach (QSO qso in tnxlog.qsoList)
                if ((comboBoxStatFilterRda.SelectedIndex == 0 || comboBoxStatFilterRda.SelectedItem == null || 
                    (!string.IsNullOrEmpty(qso.rda) && qso.rda.Contains(comboBoxStatFilterRda.SelectedItem.ToString()))) &&
                    (comboBoxStatFilterMode.SelectedIndex == 0 || comboBoxStatFilterMode.SelectedItem == null || comboBoxStatFilterMode.SelectedItem.ToString() == qso.mode) &&
                    (comboBoxStatFilterBand.SelectedIndex == 0 || comboBoxStatFilterBand.SelectedItem == null || comboBoxStatFilterBand.SelectedItem.ToString() == qso.band))
                {
                    qsoCount++;
                    callsigns.Add(qso.cs);
                }
            labelStatQso.Text = qsoCount.ToString();
            labelStatCallsigns.Text = callsigns.Count.ToString();
        }

        private void StatFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            config.statFilterBand = comboBoxStatFilterBand.SelectedItem.ToString(); 
            config.statFilterMode = comboBoxStatFilterMode.SelectedItem.ToString();
            config.statFilterRda = comboBoxStatFilterRda.SelectedItem.ToString();
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
            if (tnxlogConfig.enableCwMacros && tnxlog.transceiverController.connected)
            {
                string macro = _macro;
                if (macro.Contains('}'))
                {
                    Dictionary<string, string> substs = new Dictionary<string, string>()
                                {
                                    { "MY_CALL", textBoxCallsign.Text },
                                    { "CALL", textBoxCorrespondent.Text },
                                    { "RDA", textBoxRda.Text },
                                    { "RAFA", textBoxRafa.Text },
                                    { "LOCATOR", textBoxLocator.Text },
                                    { "USER_FIELD", textBoxUserField.Text }
                                };
                    foreach (string subst in substs.Keys)
                    {
                        string tmplt = $"{{{subst}}}";
                        string substStr = substs[subst].Replace("-", "");
                        if (macro.Contains(tmplt) && substStr == "")
                            return;
                        macro = macro.Replace(tmplt, substStr);
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
            await Task.Run(() => tnxlog.transceiverController.morseString(msg, Convert.ToInt32(1200 / tnxlogConfig.morseSpeed), tokenSource.Token));
        }

        private async void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (autoCq)
                stopAutoCq();
            if (e.KeyData == Keys.Oemtilde || e.KeyData == (Keys.W | Keys.Alt) || e.KeyData == (Keys.W | Keys.Control))
            //clear corrrespondent field
            {
                e.Handled = true;
                textBoxCorrespondent.Text = "";
            }           
            else if (e.KeyData == (CwMacrosKeys[0] | Keys.Control))//auto cq
            {
                autoCq = true;
                await processCwMacro(tnxlogConfig.cwMacros[0][1]);
            }
            else 
            {
                int cwMacroIdx = Array.IndexOf(CwMacrosKeys, e.KeyData);
                if (cwMacroIdx != -1)
                    await processCwMacro(tnxlogConfig.cwMacros[cwMacroIdx][1]);
                else if (e.KeyData == Keys.Escape && tnxlog.transceiverController.busy)
                {
                    while (!cwQueue.IsEmpty)
                        cwQueue.TryDequeue(out string discard);
                    tokenSource.Cancel();
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
                Dictionary<string, List<QSO>> data = qsoByField("loc");
                data.Keys.ToList().ForEach(val =>
                {
                    writeADIF(folderBrowserDialog.SelectedPath, val + ".adi", data[val], new Dictionary<string, string>(), true);
                });
            }
        }

        private void TextBoxUserField_Validated(object sender, EventArgs e)
        {
            ((TnxlogConfig)config.parent).userField = textBoxUserField.Text;
        }



        private void NumericUpDownMorseSpeed_ValueChanged(object sender, EventArgs e)
        {
            tnxlogConfig.morseSpeed = Convert.ToInt32(numericUpDownMorseSpeed.Value);
        }


    }

    [DataContract]
    public class FormMainConfig: StorableFormConfig
    {
        public bool topmost = false;
        public bool statFilterAuto = true;
        public decimal freq = 14000;
        public string exportPathRda;
        public string exportPathRafa;
        public string exportPathLoc;
        public string exportPath;
        public string statFilterRda;
        public string statFilterBand;
        public string statFilterMode;
        public string mode;
        public string callsign;

        public FormMainConfig(XmlConfig _parent) : base(_parent) { }

        public FormMainConfig() : base() { }
    }
}
