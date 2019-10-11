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
    public partial class FormLog : StorableForm.StorableForm
    {
        private RdaLog rdaLog;
        private BindingSource bsQSO;
        public FormLog(FormLogConfig _config, RdaLog _rdaLog) : base(_config)
        {
            rdaLog = _rdaLog;
            InitializeComponent();
            bsQSO = new BindingSource(rdaLog.qsoList, null);
            dataGridView.AutoGenerateColumns = false;
            dataGridView.DataSource = bsQSO;
        }
    }

    [DataContract]
    public class FormLogConfig : StorableFormConfig
    {
        public FormLogConfig(XmlConfig _parent) : base(_parent) { }

        public FormLogConfig() : base() { }
    }

}
