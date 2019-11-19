using StorableForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XmlConfigNS;

namespace tnxlog
{ 
    public partial class FormLog : StorableForm.StorableForm<FormLogConfig>
    {
        private Tnxlog tnxlog;
        private BindingSource bsQSO;
        private Dictionary<string, BindingList<QSO>> qsoIndex = new Dictionary<string, BindingList<QSO>>();
        public FormLog(FormLogConfig _config, Tnxlog _tnxlog) : base(_config)
        {
            tnxlog = _tnxlog;
            InitializeComponent();
            bsQSO = new BindingSource(tnxlog.qsoList, null);
            bsQSO.ListChanged += BsQSO_ListChanged;
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

            buildIndex();
            tnxlog.qsoList.ListChanged += QsoList_ListChanged;
        }

        private void BsQSO_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
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

        private void buildIndex()
        {
            qsoIndex.Clear();
            foreach (QSO qso in tnxlog.qsoList)
                addToIndex(qso);
            if (filterButton.Checked)
                filterQso();
        }

        private void QsoList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded)
                addToIndex(tnxlog.qsoList[e.NewIndex], true);
            else if (e.ListChangedType == ListChangedType.Reset)
                buildIndex();
        }

        private void setRowColors(DataGridViewRow r, Color f, Color b)
        {
            for (int co = 0; co < dataGridView.ColumnCount; co++)
            {
                r.Cells[co].Style.BackColor = b;
                r.Cells[co].Style.ForeColor = f;
            }
        }

        private void addToIndex(QSO qso, bool reverse = false)
        {
            if (!qsoIndex.ContainsKey(qso.cs))
                qsoIndex[qso.cs] = new BindingList<QSO>();
            if (reverse)
                qsoIndex[qso.cs].Insert(0, qso);
            else
                qsoIndex[qso.cs].Add(qso);
        }

        private void DataGridView_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            ((FormLogConfig)config).dataGridColumnsWidth[e.Column.Index] = e.Column.Width;
            config.write();
        }

        private void filterQso(bool warning=false)
        {
            if (filterTextBox.Text != "" && qsoIndex.ContainsKey(filterTextBox.Text))
                bsQSO.DataSource = qsoIndex[filterTextBox.Text];
            else
            {
                if (warning)
                    MessageBox.Show("Callsign not found!", Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bsQSO.DataSource = tnxlog.qsoList;
                filterButton.Checked = false;
            }
        }

        private void FilterButton_CheckedChanged(object sender, EventArgs e)
        {
            if (filterButton.Checked)
            {
                filterQso(true);
                filterButton.BackColor = SystemColors.MenuHighlight;
            }
            else
            {
                bsQSO.DataSource = tnxlog.qsoList;
                filterButton.BackColor = DefaultBackColor;
            }
        }

        private void FilterTextBox_Validated(object sender, EventArgs e)
        {
            if (filterButton.Checked)
                filterQso();
        }


        private void FormLog_FormClosed(object sender, FormClosedEventArgs e)
        {
            tnxlog.qsoList.ListChanged -= QsoList_ListChanged;
            bsQSO.ListChanged -= BsQSO_ListChanged;
        }

        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            tnxlog.writeQsoList();
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == dataGridView.NewRowIndex || e.RowIndex < 0)
                return;

            if (e.ColumnIndex == dataGridView.Columns["DeleteButton"].Index)
            {
                if (MessageBox.Show("Do you really want to delete the qso?", "TNXLOG", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.OK)
                    tnxlog.qsoList.RemoveAt(e.RowIndex);
            }
        }

        private void DataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == dataGridView.NewRowIndex || e.RowIndex < 0)
                return;

            if (e.ColumnIndex == dataGridView.Columns["DeleteButton"].Index)
            {
                var image = Properties.Resources.icon_delete;
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var x = e.CellBounds.Left + (e.CellBounds.Width - image.Width) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - image.Height) / 2;
                e.Graphics.DrawImage(image, new Point(x, y));

                e.Handled = true;
            }
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
