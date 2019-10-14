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
            for (int co = 0; co < _config.dataGridColumnsWidth.Count; co++)
                if (co < dataGridView.Columns.Count)
                    dataGridView.Columns[co].Width = _config.dataGridColumnsWidth[co];
                else
                    break;
            if (_config.dataGridColumnsWidth.Count < dataGridView.Columns.Count)
                for (int co = _config.dataGridColumnsWidth.Count; co < dataGridView.Columns.Count; co++)
                    _config.dataGridColumnsWidth.Add(dataGridView.Columns[co].Width);
        }

        private void DataGridView_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            ((FormLogConfig)config).dataGridColumnsWidth[e.Column.Index] = e.Column.Width;
            config.write();
        }
    }

    [DataContract]
    public class FormLogConfig : StorableFormConfig
    {
        public List<int> dataGridColumnsWidth = new List<int>();
        public FormLogConfig(XmlConfig _parent) : base(_parent) { }

        public FormLogConfig() : base() { }
    }

}
