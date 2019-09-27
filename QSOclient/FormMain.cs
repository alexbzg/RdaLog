using StorableForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        //static Regex CallsignRegex = new Regex(@"^(:?[A - Z\d]+\/)?\d?[A - Z]+\d+[A-Z]+(:?\/[A-Z\d]+)*$", RegexOptions.Compiled);
        static Regex CallsignRegex = new Regex(@"^(:?[A-Z\d]+\/)?\d?[A-Z]+\d+[A-Z]+(:?\/[A-Z\d]+)*$", RegexOptions.Compiled);

        private RdaLog rdaLog;
        private Dictionary<string, StatusFieldControls> statusFieldsControls;
        private Dictionary<string, Panel> panels;
        public FormMain(FormMainConfig _config, RdaLog _rdaLog) : base(_config)
        {
            rdaLog = _rdaLog;

            InitializeComponent();

            RdaLogConfig rdaLogConfig = (RdaLogConfig)config.parent;

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
                textBoxValue.Validating += delegate (object sender, CancelEventArgs e)
                {
                    textBoxValue.Text = textBoxValue.Text.ToUpper();
                };
                rdaLog.statusFieldChange += delegate (object sender, StatusFieldChangeEventArgs e)
                {
                    if (e.field == field)
                        textBoxValue.Text = e.value;
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

        private void FormMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {              
                e.Handled = true;
            }
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
    }


    [DataContract]
    public class FormMainConfig: StorableFormConfig
    {
        public FormMainConfig(XmlConfig _parent) : base(_parent) { }

        public FormMainConfig() : base() { }
    }
}
