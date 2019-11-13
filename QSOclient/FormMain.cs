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

namespace tnxlog
{
    public partial class FormMain : StorableForm.StorableForm<FormMainConfig>
    {
        class StatusFieldControls
        {
            internal CheckBox auto;
            internal TextBox value;
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

        static readonly string AutoUpdaterURI = "http://tnxqso.com/static/files/rda_log.xml";
        private Tnxlog tnxlog;
        private Dictionary<string, StatusFieldControls> statusFieldsControls;
        private Dictionary<string, Panel> panels;
        private HashSet<string> rdaValues;
        private HashSet<string> rafaValues;
        private StringIndex callsignsDb = new StringIndex();
        private StringIndex callsignsQso = new StringIndex();
        private Timer timer = new Timer();
        private Control[] qsoControls;
        private Object[] qsoValues;
        private InputLanguage englishInputLanguage;
        public FormMain(FormMainConfig _config, Tnxlog _tnxlog) : base(_config)
        {
            tnxlog = _tnxlog;

            InitializeComponent();

            TnxlogConfig tnxlogConfig = (TnxlogConfig)config.parent;

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
                System.Diagnostics.Trace.TraceInformation(e.ToString());
                MessageBox.Show("RDA data could not be loaded: " + e.ToString(), "RDA Log", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                string callsignsStr = File.ReadAllText(Application.StartupPath + @"\callsigns.json");
                foreach (string callsign in JsonConvert.DeserializeObject<string[]>(callsignsStr))
                    callsignsDb.add(callsign);
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.TraceInformation(e.ToString());
                MessageBox.Show("Callsigns list could not be loaded: " + e.ToString(), "RDA Log", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            try
            {
                using (StreamReader sr = new StreamReader(Application.StartupPath + "\\rafa.csv"))
                {
                    List<string> values = new List<string>();
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
                                if (!values.Contains(entry))
                                    values.Add(entry);
                            }
                        }
                    } while (sr.Peek() >= 0);
                    if (values.Count > 0)
                        rafaValues = new HashSet<string>(values);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.TraceInformation(e.ToString());
                MessageBox.Show("RAFA data could not be loaded: " + e.ToString(), "RDA Log", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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
            comboBoxMode.Items.AddRange(HamRadio.Mode.Names);

            checkBoxTop.Checked = config.topmost;
            comboBoxMode.SelectedItem = string.IsNullOrEmpty(config.mode) ? comboBoxMode.Items[0] : config.mode;
            if (config.freq != 0)
                numericUpDownFreq.Value = config.freq;

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

            buildQsoIndices();
            tnxlog.qsoList.ListChanged += QsoList_ListChanged;

            timer.Tick += Timer_Tick;
            timer.Interval = 500;
            timer.Enabled = true;

            Text = Assembly.GetExecutingAssembly().GetName().Name + " " + Assembly.GetExecutingAssembly().GetName().Version.ToString();

            qsoControls = new Control[] { textBoxCallsign, numericUpDownFreq, comboBoxMode, textBoxRstRcvd, textBoxRstSent };
            saveQsoValues();
        }

        private void onLogInOut(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(((TnxlogConfig)config.parent).httpService.token))
                connectionStatusLabel.Text = "Not logged in.";
            else if (tnxlog.httpService.connected)
                connectionStatusLabel.Text = "Logged in as " + ((TnxlogConfig)config.parent).httpService.callsign.ToUpper() + ".";
            else
                connectionStatusLabel.Text = "Logged in as " + ((TnxlogConfig)config.parent).httpService.callsign + ". No connection.";
        }

        private void rdaLog_statusFieldChange (object sender, StatusFieldChangeEventArgs e)
        {
            string value = string.IsNullOrEmpty(e.value) ? "N/A" : e.value;
            showBalloon($"New {e.field}: {value}");
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
            TnxlogConfig rdaLogConfig = ((TnxlogConfig)config.parent);
            foreach (string panel in TnxlogConfig.MainFormPanels)
            {
                //cwMacros temporary disabled
                if (panels.ContainsKey(panel) && panel != "cwMacros" && rdaLogConfig.getMainFormPanelVisible(panel))
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

        private void showBalloon(string text)
        {
            DoInvoke(() =>
            {
                NotifyIcon notifyIcon = new NotifyIcon();
                notifyIcon.Visible = true;
                notifyIcon.Icon = SystemIcons.Information;
                notifyIcon.BalloonTipTitle = "RDA Log";
                notifyIcon.BalloonTipText = text;
                notifyIcon.ShowBalloonTip(0);
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
                        "RDA Log", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    textBoxCallsign.Focus();
                    return;
                }
                if (ActiveControl != textBoxCorrespondent)
                {
                    for (int co = 0; co < qsoControls.Length; co++)
                    {
                        if ((qsoControls[co].GetType() == typeof(NumericUpDown) && ((NumericUpDown)qsoControls[co]).Value != (decimal)qsoValues[co])
                            || (qsoControls[co].GetType() != typeof(NumericUpDown) && qsoControls[co].Text != qsoValues[co].ToString()))
                        {
                            textBoxCorrespondent.Focus();
                            saveQsoValues();
                            return;
                        }
                    }
                }
                saveQsoValues();
                string correspondent = textBoxCorrespondent.Text;
                textBoxCorrespondent.Text = "";
                setDefRst();
                textBoxCorrespondent.Focus();
                await tnxlog.newQso(correspondent, textBoxCallsign.Text, numericUpDownFreq.Value, comboBoxMode.Text, textBoxRstRcvd.Text, textBoxRstSent.Text);
            }
        }

        private void setDefRst()
        {
            string mode = comboBoxMode.SelectedItem.ToString();
            textBoxRstSent.Text = HamRadio.Mode.DefRst[mode];
            textBoxRstRcvd.Text = HamRadio.Mode.DefRst[mode];
        }

        private void saveQsoValues()
        {
            qsoValues = new object[] { textBoxCallsign.Text, numericUpDownFreq.Value, comboBoxMode.Text, textBoxRstRcvd.Text, textBoxRstSent.Text };
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
                MessageBox.Show("Invalid rda: " + txt, "Rda Log", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void TextBoxRafa_Validating(object sender, CancelEventArgs e)
        {
            string txt = textBoxRafa.Text;
            try
            {
                List<string> rafas = parseValues(RafaRegex, ref txt, rafaValues, null);
                textBoxRafa.Text = String.Join(" ", rafas);
            }
            catch (ArgumentException ex)
            {
                txt = ex.Message;
            }
            if (txt.Length > 0)
            {
                MessageBox.Show("Invalid rafa: " + txt, "Rda Log", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Invalid locator: " + txt, "Rda Log", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            try
            {
                DateTime ts = DateTime.UtcNow;
                foreach (var entry in data)
                {
                    string entryFileName = string.IsNullOrEmpty(entry.callsign) ? fileName : entry.callsign.Replace('/', '_') + " " + fileName;
                    using (StreamWriter sw = new StreamWriter(Path.Combine(folder, entryFileName)))
                    {
                        sw.WriteLine("ADIF Export from RDA Log");
                        sw.WriteLine("Logs generated @ {0:yyyy-MM-dd HH:mm:ssZ}", ts);
                        sw.WriteLine("<EOH>");
                        foreach (QSO qso in entry.qso)
                            sw.WriteLine(qso.adif(adifParams));
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceInformation(ex.ToString());
                MessageBox.Show("Can not export to text file: " + ex.ToString(), "DXpedition", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (MessageBox.Show("All QSO will be deleted. Do you really want to continue?", "RDA Log", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                tnxlog.clearQso();
                buildQsoIndices();
            }
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Oemtilde || e.KeyData == (Keys.V | Keys.Alt))
            {
                e.Handled = true;
                textBoxCorrespondent.Text = "";
            }
        }

        private void FormMain_Activated(object sender, EventArgs e)
        {
            if (!InputLanguage.CurrentInputLanguage.Culture.EnglishName.StartsWith("English") && englishInputLanguage != null)
                InputLanguage.CurrentInputLanguage = englishInputLanguage;
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
