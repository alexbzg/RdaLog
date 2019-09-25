using StorableForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
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

        private RdaLog rdaLog;
        private Dictionary<string, StatusFieldControls> statusFieldsControls;
        public FormMain(FormMainConfig _config, RdaLog _rdaLog) : base(_config)
        {
            rdaLog = _rdaLog;
            InitializeComponent();
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
            RdaLogConfig rdaLogConfig = (RdaLogConfig)config.parent;
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
        }


        private void ToolStripLabelSettings_Click(object sender, EventArgs e)
        {
            rdaLog.showSettings();
        }

    }


    [DataContract]
    public class FormMainConfig: StorableFormConfig
    {
        public FormMainConfig(XmlConfig _parent) : base(_parent) { }

        public FormMainConfig() : base() { }
    }
}
