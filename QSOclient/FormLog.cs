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
    public partial class FormLog : StorableForm.StorableForm<FormLogConfig>
    {
        private RdaLog rdaLog;
        private BindingSource bsQSO;
        private Dictionary<string, BindingList<QSO>> qsoIndex = new Dictionary<string, BindingList<QSO>>();
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
            foreach (QSO qso in rdaLog.qsoList)
                addToIndex(qso);
            rdaLog.qsoList.ListChanged += QsoList_ListChanged;
        }

        private void QsoList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                addToIndex(rdaLog.qsoList[e.NewIndex]);
                DataGridViewRow r = dataGridView.Rows[e.NewIndex];
                setRowColors(r, Color.White, Color.SteelBlue);
                Task.Run(async () =>
                {
                    await Task.Delay(5000);
                    DoInvoke(() =>
                    {
                        setRowColors(r, dataGridView.DefaultCellStyle.ForeColor, dataGridView.DefaultCellStyle.BackColor);
                        dataGridView.Refresh();
                    });
                });
                dataGridView.FirstDisplayedScrollingRowIndex = e.NewIndex;
                dataGridView.Refresh();
            }
        }

        private void setRowColors(DataGridViewRow r, Color f, Color b)
        {
            for (int co = 0; co < dataGridView.ColumnCount; co++)
            {
                r.Cells[co].Style.BackColor = b;
                r.Cells[co].Style.ForeColor = f;
            }
        }

        private void addToIndex(QSO qso)
        {
            if (!qsoIndex.ContainsKey(qso.cs))
                qsoIndex[qso.cs] = new BindingList<QSO>();
            qsoIndex[qso.cs].Add(qso);
        }

        private void DataGridView_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            ((FormLogConfig)config).dataGridColumnsWidth[e.Column.Index] = e.Column.Width;
            config.write();
        }

        private void filterQso()
        {
            if (filterTextBox.Text != "" && qsoIndex.ContainsKey(filterTextBox.Text))
                bsQSO.DataSource = qsoIndex[filterTextBox.Text];
            else
                MessageBox.Show("Callsign not found!", "RDA Log", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void FilterButton_CheckedChanged(object sender, EventArgs e)
        {
            if (filterButton.Checked)
            {
                filterQso();
                filterButton.BackColor = SystemColors.MenuHighlight;
            }
            else
            {
                bsQSO.DataSource = rdaLog.qsoList;
                filterButton.BackColor = DefaultBackColor;
            }
        }

        private void FilterTextBox_Validated(object sender, EventArgs e)
        {
            if (filterButton.Checked)
                filterQso();
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
