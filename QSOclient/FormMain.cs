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

namespace RdaLog
{
    public partial class FormMain : StorableForm.StorableForm
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

        private RdaLog rdaLog;
        private Dictionary<string, StatusFieldControls> statusFieldsControls;
        private Dictionary<string, Panel> panels;
        private HashSet<string> rdaValues;
        private HashSet<string> rafaValues;
        private HashSet<string> callsignsDb;
        private HashSet<string> callsignsQso;

        public FormMain(FormMainConfig _config, RdaLog _rdaLog) : base(_config)
        {
            rdaLog = _rdaLog;

            InitializeComponent();

            RdaLogConfig rdaLogConfig = (RdaLogConfig)config.parent;

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
                callsignsDb = new HashSet<string>(JsonConvert.DeserializeObject<string[]>(callsignsStr));
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
            rdaLogConfig.mainFormPanelVisibleChange += delegate (object sender, EventArgs e)
            {
                arrangePanels();
            };

            foreach (KeyValuePair<string, StatusFieldControls> item in statusFieldsControls)
            {
                string field = item.Key;
                bool auto = rdaLogConfig.getStatusFieldAuto(field);
                CheckBox checkBoxAuto = item.Value.auto;
                TextBox textBoxValue = item.Value.value;
                checkBoxAuto.Checked = auto;
                textBoxValue.Enabled = !auto;
                textBoxValue.Text = rdaLogConfig.getStatusFieldValue(field);
                checkBoxAuto.CheckedChanged += delegate (object sender, EventArgs e)
                {
                    rdaLogConfig.setStatusFieldAuto(field, checkBoxAuto.Checked);
                    textBoxValue.Enabled = !checkBoxAuto.Checked;
                };
                textBoxValue.Validated += delegate (object sender, EventArgs e)
                {
                    rdaLog.setStatusFieldValue(field, textBoxValue.Text);
                };
                rdaLog.statusFieldChange += delegate (object sender, StatusFieldChangeEventArgs e)
                {
                    if (e.field == field)
                        DoInvoke(() =>
                        {
                            textBoxValue.Text = e.value;
                        });
                };
            }

            textBoxUserField.Text = rdaLogConfig.userField;
            textBoxCallsign.Text = rdaLogConfig.callsign;

            callsignsQso = new HashSet<string>(rdaLog.qsoList.Select(item => item.cs));
            rdaLog.qsoList.ListChanged += QsoList_ListChanged;
        }

        private void QsoList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded)
                callsignsQso.Add(rdaLog.qsoList[e.NewIndex].cs);
        }

        private void arrangePanels()
        {
            foreach (Panel panel in panels.Values)
            {
                if (panel.Parent == flowLayoutPanel)
                    flowLayoutPanel.Controls.Remove(panel);
            }
            RdaLogConfig rdaLogConfig = ((RdaLogConfig)config.parent);
            foreach (string panel in RdaLogConfig.MainFormPanels)
            {
                if (panels.ContainsKey(panel) && rdaLogConfig.getMainFormPanelVisible(panel))
                    flowLayoutPanel.Controls.Add(panels[panel]);
            }
            statusStrip.SendToBack();
        }

        private void ToolStripLabelSettings_Click(object sender, EventArgs e)
        {
            rdaLog.showSettings();
        }


        private void TextBoxCallsign_Validated(object sender, EventArgs e)
        {
            ((RdaLogConfig)config.parent).callsign = textBoxCallsign.Text;
        }

        private void TextBoxCorrespondent_Validating(object sender, CancelEventArgs e)
        {
            textBoxCorrespondent.Text = textBoxCorrespondent.Text.ToUpper();
        }

        private async void FormMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Validate();
                e.Handled = true;
                string correspondent = textBoxCorrespondent.Text;
                textBoxCorrespondent.Text = "";
                await rdaLog.newQso(correspondent, textBoxCallsign.Text, numericUpDownFreq.Value, comboBoxMode.Text, textBoxRstRcvd.Text, textBoxRstSent.Text);
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
            rdaLog.showFormLog();
        }

        private void MenuItemAdifExportRda_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                Dictionary<string, List<QSO>> data = new Dictionary<string, List<QSO>>();
                rdaLog.qsoList
                    .Where(qso => qso.rda != null).ToList()
                    .ForEach(qso =>
                    {
                        string[] rdas = qso.rda.Split(new string[] { " " }, StringSplitOptions.None);
                        foreach (string rda in rdas)
                        {
                            if (!data.ContainsKey(rda))
                                data[rda] = new List<QSO>();
                            data[rda].Add(qso);
                        }
                    });
                data.Keys.ToList().ForEach(val =>
                {
                    writeADIF(Path.Combine(folderBrowserDialog.SelectedPath, val + ".adi"), data[val], new Dictionary<string, string>() { { "RDA", val } });
                });
            }
        }

        private void MenuItemAdifExportRafa_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                Dictionary<string, List<QSO>> data = new Dictionary<string, List<QSO>>();
                rdaLog.qsoList
                    .Where(qso => qso.rafa != null).ToList()
                    .ForEach(qso =>
                    {
                        string[] rafas = qso.rafa.Split(new string[] { " " }, StringSplitOptions.None);
                        foreach (string rafa in rafas)
                        {
                            if (!data.ContainsKey(rafa))
                                data[rafa] = new List<QSO>();
                            data[rafa].Add(qso);
                        }
                    });
                data.Keys.ToList().ForEach(val =>
                {
                    writeADIF(Path.Combine(folderBrowserDialog.SelectedPath, val + ".adi"), data[val], new Dictionary<string, string>() { { "RAFA", val } });
                });
            }
        }

        private void writeADIF(string fileName, List<QSO> data, Dictionary<string, string> adifParams)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    DateTime ts = DateTime.UtcNow;
                    sw.WriteLine("ADIF Export from RDA Log");
                    sw.WriteLine("Logs generated @ {0:yyyy-MM-dd HH:mm:ssZ}", ts);
                    sw.WriteLine("<EOH>");
                    foreach (QSO qso in data)
                        sw.WriteLine(qso.adif(adifParams));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceInformation(ex.ToString());
                MessageBox.Show("Can not export to text file: " + ex.ToString(), "DXpedition", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void updateListBoxCallsigns(ListBox box, HashSet<string> callsignsSet)
        {
            box.ClearSelected();
            box.Items.Clear();
            if (callsignsSet != null && textBoxCorrespondent.Text.Length > 2)
                box.Items.AddRange(callsignsSet.Where(item => item.Contains(textBoxCorrespondent.Text)).ToArray());
        }

        private void TextBoxCorrespondent_TextChanged(object sender, EventArgs e)
        {
            if (((RdaLogConfig)config.parent).getMainFormPanelVisible("callsignId"))
            {
                updateListBoxCallsigns(listBoxCallsignsDb, callsignsDb);
                updateListBoxCallsigns(listBoxCallsignsQso, callsignsQso);
            }
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
    }

    [DataContract]
    public class FormMainConfig: StorableFormConfig
    {
        public FormMainConfig(XmlConfig _parent) : base(_parent) { }

        public FormMainConfig() : base() { }
    }
}
