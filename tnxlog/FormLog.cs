﻿using StorableForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using tnxlog.Properties;
using XmlConfigNS;

namespace tnxlog
{ 
    public partial class FormLog : StorableForm.StorableForm<FormLogConfig>
    {
        private Tnxlog tnxlog;
        private BindingSource bsQSO;
        private BindingList<QSO> searchResults = new BindingList<QSO>();
        private Regex reFilter;
        private DataGridViewTextBoxColumn[] qthColumns;
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

            qthColumns = new DataGridViewTextBoxColumn[] { QthField0, QthField1, QthField2 };
            for (int field = 0; field < TnxlogConfig.QthFieldCount; field++)
                qthColumns[field].HeaderText = ((TnxlogConfig)(config.parent)).qthFieldTitles[field];
            tnxlog.qthFieldTitleChange += qthFieldTitleChange;

            tnxlog.qsoList.ListChanged += QsoList_ListChanged;
        }

        private void qthFieldTitleChange(object sender, QthFieldChangeEventArgs e)
        {
            DoInvoke(() => { qthColumns[e.field].HeaderText = e.value; });
        }

        private void BsQSO_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                DoInvoke(() =>
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
                });
            } 
            else if (e.ListChangedType == ListChangedType.ItemChanged && e.PropertyDescriptor.Name == "serverState")
            {
                DoInvoke(() =>
                {
                    dataGridView.InvalidateCell(dataGridView.Columns["ServerState"].Index, e.NewIndex);
                    dataGridView.InvalidateCell(-1, e.NewIndex);
                });
            }
        }


        private bool qsoFilterPredicate(QSO qso)
        {
            if (filterTextBox.Text.Contains("*"))
                return reFilter.IsMatch(qso.cs);
            else
                return qso.cs == filterTextBox.Text;
        }

        private void updateSearchResults()
        {
            searchResults = new BindingList<QSO>(tnxlog.qsoList.Where(item => qsoFilterPredicate(item)).ToList());
        }

        private void QsoList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded && qsoFilterPredicate(tnxlog.qsoList[e.NewIndex]))
            {
                if (bsQSO.DataSource == searchResults)
                    searchResults.Insert(0, tnxlog.qsoList[e.NewIndex]);
            } 
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


        private void DataGridView_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            ((FormLogConfig)config).dataGridColumnsWidth[e.Column.Index] = e.Column.Width;
            config.write();
        }

        private void filterQso(bool warning=false)
        {
            if (filterTextBox.Text != "")
            {
                if (filterTextBox.Text.Contains("*"))
                    reFilter = new Regex(filterTextBox.Text.Replace("*", ".*"), RegexOptions.Compiled);
                updateSearchResults();
                if (searchResults.Count > 0)
                    bsQSO.DataSource = searchResults;
                else
                {
                    if (warning)
                        MessageBox.Show("Callsign not found!", Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    bsQSO.DataSource = tnxlog.qsoList;
                    filterButton.Checked = false;
                }
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
            dataGridView.CurrentCell = dataGridView[e.ColumnIndex == -1 ? 0 : e.ColumnIndex, e.RowIndex];
            menuItemEditCell.Visible = e.ColumnIndex != -1;
            e.ContextMenuStrip = cmsDataGridCell;
        }

        private void DataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridView[e.ColumnIndex, e.RowIndex].IsInEditMode)
                dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private async void MenuItemDeleteQso_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to delete the QSO?", Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                await tnxlog.deleteQso(getCurrentQso());
        }

        private QSO getCurrentQso()
        {
            return ((BindingList<QSO>)bsQSO.DataSource)[dataGridView.CurrentCell.RowIndex];
        }

        private QSO getQsoByIndex(int row)
        {
            return ((BindingList<QSO>)bsQSO.DataSource)[row];
        }


        private void MenuItemEditCell_Click(object sender, EventArgs e)
        {
            editCell();
        }

        private void editCell()
        {
            if (dataGridView.SelectedCells.Count > 0)
            {
                dataGridView.SelectedCells[0].Style.BackColor = SystemColors.Info;
                dataGridView.BeginEdit(true);
            }
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
                QSO qso = getCurrentQso();
                tnxlog.qsoDB.qso.Update(qso);
                qso.serverState = ServerState.None;
                await tnxlog.httpService.postQso(getCurrentQso());
            }
        }

        private void DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView.ClearSelection();
            if (e.ColumnIndex != -1)
                dataGridView[e.ColumnIndex, e.RowIndex].Selected = true;
            editCell();
        }

        private void DataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView.Columns[e.ColumnIndex].Name == "ServerState" && e.RowIndex != -1)
            {
                QSO qso = getQsoByIndex(e.RowIndex);
                if (qso.serverState == ServerState.Accepted)
                    e.Value = imageListServerStates.Images[0];
                else if (qso.serverState == ServerState.Rejected)
                    e.Value = imageListServerStates.Images[1];
                if (qso.serverState == ServerState.Pending)
                    e.Value = imageListServerStates.Images[2];
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
