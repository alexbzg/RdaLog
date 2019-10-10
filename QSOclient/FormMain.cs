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

        static bool CallsignChar(char c)
        {
            return char.IsWhiteSpace(c) || char.IsLetterOrDigit(c) || c == '/';
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

        static Regex CallsignRegex = new Regex(@"^(:?[A-Z\d]+\/)?\d?[A-Z]+\d+[A-Z]+(:?\/[A-Z\d]+)*$", RegexOptions.Compiled);
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
        private string[] rdaValues;
        private string[] rafaValues;
        public FormMain(FormMainConfig _config, RdaLog _rdaLog) : base(_config)
        {
            rdaLog = _rdaLog;

            InitializeComponent();

            RdaLogConfig rdaLogConfig = (RdaLogConfig)config.parent;

            try
            {
                string rdaValuesStr = File.ReadAllText(Application.StartupPath + @"\rdaValues.json");
                rdaValues = JsonConvert.DeserializeObject<string[]>(rdaValuesStr);
            }
            catch (Exception e) {
                System.Diagnostics.Trace.TraceInformation(e.ToString());
                MessageBox.Show("RDA data could not be loaded: " + e.ToString(), "RDA Log", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        rafaValues = values.ToArray();
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
                textBoxCorrespondent.Text = "";
                await rdaLog.newQso(textBoxCorrespondent.Text, textBoxCallsign.Text, numericUpDownFreq.Value, comboBoxMode.Text, textBoxRstRcvd.Text, textBoxRstSent.Text);
            }
        }

        private List<string> parseValues(Regex reMatch, ref string values, string[] valuesList, RegexReplace reReplace)
        {
            values = values.ToUpper().Trim();
            List<string> result = new List<string>();
            Match match = reMatch.Match(values);
            while (match.Success) { 
                string matchStr = match.Value;
                if (reReplace != null)
                    matchStr = reReplace.replace(matchStr);
                if (valuesList == null || valuesList.Contains(matchStr))
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

        private void TextBoxCallsign_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = ((TextBox)sender);
            textBox.TextChanged -= TextBoxCallsign_TextChanged;
            int selStart = textBox.SelectionStart;
            int newSelStart = 0;
            string newText = "";
            textBox.Text = textBox.Text.ToUpper();
            for (int co = 0; co < textBox.Text.Length; co++)
                if (CallsignChar(textBox.Text[co]))
                {
                    if (co < selStart)
                        newSelStart++;
                    newText += textBox.Text[co];
                }
            textBox.Text = newText;
            textBox.BackColor = CallsignRegex.IsMatch(textBox.Text) || textBox.Text.Equals(string.Empty) ? 
                (textBox == textBoxCorrespondent ? SystemColors.Info : SystemColors.Window) : 
                Color.IndianRed;
            textBox.SelectionStart = newSelStart;
            textBox.TextChanged += TextBoxCallsign_TextChanged;
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

        private void NumericUpDownFreq_ValueChanged(object sender, EventArgs e)
        {

        }

        private void ToolStripLabelLog_Click(object sender, EventArgs e)
        {
            rdaLog.showFormLog();
        }
    }


    [DataContract]
    public class FormMainConfig: StorableFormConfig
    {
        public FormMainConfig(XmlConfig _parent) : base(_parent) { }

        public FormMainConfig() : base() { }
    }
}
