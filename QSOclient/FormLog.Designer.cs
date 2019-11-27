namespace tnxlog
{
    partial class FormLog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLog));
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.myCS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.snt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.myRST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Freq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RDA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RAFA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Locator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userField = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comments = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.filterTextBox = new System.Windows.Forms.ToolStripTextBoxCallsign();
            this.filterButton = new System.Windows.Forms.ToolStripButton();
            this.cmsDataGridCell = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemDeleteQso = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEditCell = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.cmsDataGridCell.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.no,
            this.ts,
            this.myCS,
            this.cs,
            this.snt,
            this.myRST,
            this.Freq,
            this.Mode,
            this.RDA,
            this.RAFA,
            this.Locator,
            this.userField,
            this.Comments});
            this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView.Location = new System.Drawing.Point(0, 28);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView.Size = new System.Drawing.Size(800, 422);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.DataGridView_CellContextMenuStripNeeded);
            this.dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellDoubleClick);
            this.dataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellEndEdit);
            this.dataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView_CellMouseDown);
            this.dataGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.DataGridView_CellValidating);
            this.dataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellValueChanged);
            this.dataGridView.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.DataGridView_ColumnWidthChanged);
            // 
            // no
            // 
            this.no.DataPropertyName = "no";
            this.no.Frozen = true;
            this.no.HeaderText = "Nr";
            this.no.Name = "no";
            // 
            // ts
            // 
            this.ts.DataPropertyName = "ts";
            this.ts.HeaderText = "Time";
            this.ts.Name = "ts";
            // 
            // myCS
            // 
            this.myCS.DataPropertyName = "myCS";
            this.myCS.HeaderText = "My Call";
            this.myCS.Name = "myCS";
            // 
            // cs
            // 
            this.cs.DataPropertyName = "cs";
            this.cs.HeaderText = "Ur Call";
            this.cs.Name = "cs";
            // 
            // snt
            // 
            this.snt.DataPropertyName = "snt";
            this.snt.HeaderText = "Ur RST";
            this.snt.Name = "snt";
            // 
            // myRST
            // 
            this.myRST.DataPropertyName = "rcv";
            this.myRST.HeaderText = "My RST";
            this.myRST.Name = "myRST";
            // 
            // Freq
            // 
            this.Freq.DataPropertyName = "freq";
            this.Freq.HeaderText = "Freq";
            this.Freq.Name = "Freq";
            // 
            // Mode
            // 
            this.Mode.DataPropertyName = "mode";
            this.Mode.HeaderText = "Mode";
            this.Mode.Name = "Mode";
            // 
            // RDA
            // 
            this.RDA.DataPropertyName = "rda";
            this.RDA.HeaderText = "RDA";
            this.RDA.Name = "RDA";
            // 
            // RAFA
            // 
            this.RAFA.DataPropertyName = "rafa";
            this.RAFA.HeaderText = "RAFA";
            this.RAFA.Name = "RAFA";
            // 
            // Locator
            // 
            this.Locator.DataPropertyName = "loc";
            this.Locator.HeaderText = "Locator";
            this.Locator.Name = "Locator";
            // 
            // userField
            // 
            this.userField.DataPropertyName = "userField0";
            this.userField.HeaderText = "User Field";
            this.userField.Name = "userField";
            // 
            // Comments
            // 
            this.Comments.DataPropertyName = "comments";
            this.Comments.HeaderText = "Comments";
            this.Comments.Name = "Comments";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filterTextBox,
            this.filterButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // filterTextBox
            // 
            this.filterTextBox.AllowWildcards = true;
            this.filterTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.filterTextBox.Name = "filterTextBox";
            this.filterTextBox.Size = new System.Drawing.Size(100, 25);
            this.filterTextBox.Validated += new System.EventHandler(this.FilterTextBox_Validated);
            // 
            // filterButton
            // 
            this.filterButton.CheckOnClick = true;
            this.filterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.filterButton.Image = ((System.Drawing.Image)(resources.GetObject("filterButton.Image")));
            this.filterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.filterButton.Name = "filterButton";
            this.filterButton.Size = new System.Drawing.Size(37, 22);
            this.filterButton.Text = "Filter";
            this.filterButton.CheckedChanged += new System.EventHandler(this.FilterButton_CheckedChanged);
            // 
            // cmsDataGridCell
            // 
            this.cmsDataGridCell.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemDeleteQso,
            this.menuItemEditCell});
            this.cmsDataGridCell.Name = "cmsDataGridCell";
            this.cmsDataGridCell.Size = new System.Drawing.Size(135, 48);
            // 
            // menuItemDeleteQso
            // 
            this.menuItemDeleteQso.Name = "menuItemDeleteQso";
            this.menuItemDeleteQso.Size = new System.Drawing.Size(134, 22);
            this.menuItemDeleteQso.Text = "Delete QSO";
            this.menuItemDeleteQso.Click += new System.EventHandler(this.MenuItemDeleteQso_Click);
            // 
            // menuItemEditCell
            // 
            this.menuItemEditCell.Name = "menuItemEditCell";
            this.menuItemEditCell.Size = new System.Drawing.Size(134, 22);
            this.menuItemEditCell.Text = "Edit data";
            this.menuItemEditCell.Click += new System.EventHandler(this.MenuItemEditCell_Click);
            // 
            // FormLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dataGridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormLog";
            this.Text = "RDA Log - Log";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormLog_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.cmsDataGridCell.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton filterButton;
        private System.Windows.Forms.DataGridViewButtonColumn DeleteButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn no;
        private System.Windows.Forms.DataGridViewTextBoxColumn ts;
        private System.Windows.Forms.DataGridViewTextBoxColumn myCS;
        private System.Windows.Forms.DataGridViewTextBoxColumn cs;
        private System.Windows.Forms.DataGridViewTextBoxColumn snt;
        private System.Windows.Forms.DataGridViewTextBoxColumn myRST;
        private System.Windows.Forms.DataGridViewTextBoxColumn Freq;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mode;
        private System.Windows.Forms.DataGridViewTextBoxColumn RDA;
        private System.Windows.Forms.DataGridViewTextBoxColumn RAFA;
        private System.Windows.Forms.DataGridViewTextBoxColumn Locator;
        private System.Windows.Forms.DataGridViewTextBoxColumn userField;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comments;
        private System.Windows.Forms.ContextMenuStrip cmsDataGridCell;
        private System.Windows.Forms.ToolStripMenuItem menuItemDeleteQso;
        private System.Windows.Forms.ToolStripMenuItem menuItemEditCell;
        private System.Windows.Forms.ToolStripTextBoxCallsign filterTextBox;
    }
}