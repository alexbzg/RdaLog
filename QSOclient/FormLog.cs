using StorableForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
                if (b != null)
                    r.Cells[co].Style.BackColor = b;
                if (f != null)
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
            dataGridView[e.ColumnIndex, e.RowIndex].Style.BackColor = SystemColors.Window;
        }

        private void DataGridView_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            menuItemEditCell.Visible = e.ColumnIndex != -1;
            e.ContextMenuStrip = cmsDataGridCell;
        }

        private void DataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridView[e.ColumnIndex, e.RowIndex].IsInEditMode) {
                DialogResult mbres = MessageBox.Show("Save changes?", Assembly.GetExecutingAssembly().GetName().Name,
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (mbres == DialogResult.Cancel)
                    e.Cancel = true;
                else if (mbres == DialogResult.Yes)
                    dataGridView.EndEdit();
                else
                    dataGridView.CancelEdit();
            }
        }

        private void MenuItemDeleteQso_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to delete the QSO?", Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                QSO qso = getCurrentQso();
                tnxlog.qsoList.Remove(qso);
                tnxlog.httpService.deleteQso(qso);
            }
        }

        private QSO getCurrentQso()
        {
            return ((BindingList<QSO>)bsQSO.DataSource)[dataGridView.SelectedCells[0].RowIndex];
        }

        private void MenuItemEditCell_Click(object sender, EventArgs e)
        {
            dataGridView.SelectedCells[0].Style.BackColor = SystemColors.Info;
            dataGridView.BeginEdit(true);
        }

        private void DataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex != -1)
                {
                    dataGridView.ClearSelection();
                    if (e.ColumnIndex != -1)
                        dataGridView[e.ColumnIndex, e.RowIndex].Selected = true;
                    else
                        dataGridView.Rows[e.RowIndex].Selected = true;
                }
            }
        }

        private async void DataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.IsCurrentCellInEditMode)
            {
                tnxlog.writeQsoList();
                await tnxlog.httpService.postQso(getCurrentQso());
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
