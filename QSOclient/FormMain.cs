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
        private RdaLog rdaLog;
        public FormMain(FormMainConfig _config, RdaLog _rdaLog) : base(_config)
        {
            rdaLog = _rdaLog;
            InitializeComponent();
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
