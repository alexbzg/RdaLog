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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmsDataGridCell = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemDeleteQso = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEditCell = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.filterTextBox = new System.Windows.Forms.ToolStripTextBoxCallsign();
            this.filterButton = new System.Windows.Forms.ToolStripButton();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServerState = new System.Windows.Forms.DataGridViewImageColumn();
            this.ts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.myCS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.snt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.myRST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Freq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QthField0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QthField1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QthField2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Locator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comments = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imageListServerStates = new System.Windows.Forms.ImageList(this.components);
            this.cmsDataGridCell.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
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
            this.ServerState,
            this.ts,
            this.myCS,
            this.cs,
            this.snt,
            this.myRST,
            this.Freq,
            this.Mode,
            this.QthField0,
            this.QthField1,
            this.QthField2,
            this.Locator,
            this.Comments});
            this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView.Location = new System.Drawing.Point(0, 28);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView.Size = new System.Drawing.Size(800, 422);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellContentClick);
            this.dataGridView.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.DataGridView_CellContextMenuStripNeeded);
            this.dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellDoubleClick);
            this.dataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellEndEdit);
            this.dataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridView_CellFormatting);
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
            // ServerState
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = null;
            this.ServerState.DefaultCellStyle = dataGridViewCellStyle1;
            this.ServerState.HeaderText = "Server";
            this.ServerState.Name = "ServerState";
            this.ServerState.ReadOnly = true;
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
            // QthField0
            // 
            this.QthField0.DataPropertyName = "QthField0";
            this.QthField0.HeaderText = "QTH Field";
            this.QthField0.Name = "QthField0";
            // 
            // QthField1
            // 
            this.QthField1.DataPropertyName = "QthField1";
            this.QthField1.HeaderText = "QTH Field";
            this.QthField1.Name = "QthField1";
            // 
            // QthField2
            // 
            this.QthField2.DataPropertyName = "QthField2";
            this.QthField2.HeaderText = "QTH Field";
            this.QthField2.Name = "QthField2";
            // 
            // Locator
            // 
            this.Locator.DataPropertyName = "loc";
            this.Locator.HeaderText = "Locator";
            this.Locator.Name = "Locator";
            // 
            // Comments
            // 
            this.Comments.DataPropertyName = "comments";
            this.Comments.HeaderText = "Comment";
            this.Comments.Name = "Comments";
            // 
            // imageListServerStates
            // 
            this.imageListServerStates.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListServerStates.ImageStream")));
            this.imageListServerStates.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListServerStates.Images.SetKeyName(0, "icon_ok.png");
            this.imageListServerStates.Images.SetKeyName(1, "icon_error.png");
            this.imageListServerStates.Images.SetKeyName(2, "icon_wait.png");
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
            this.Text = "TNXLOG - Log";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormLog_FormClosed);
            this.cmsDataGridCell.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton filterButton;
        private System.Windows.Forms.ContextMenuStrip cmsDataGridCell;
        private System.Windows.Forms.ToolStripMenuItem menuItemDeleteQso;
        private System.Windows.Forms.ToolStripMenuItem menuItemEditCell;
        private System.Windows.Forms.ToolStripTextBoxCallsign filterTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn no;
        private System.Windows.Forms.DataGridViewImageColumn ServerState;
        private System.Windows.Forms.DataGridViewTextBoxColumn ts;
        private System.Windows.Forms.DataGridViewTextBoxColumn myCS;
        private System.Windows.Forms.DataGridViewTextBoxColumn cs;
        private System.Windows.Forms.DataGridViewTextBoxColumn snt;
        private System.Windows.Forms.DataGridViewTextBoxColumn myRST;
        private System.Windows.Forms.DataGridViewTextBoxColumn Freq;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mode;
        private System.Windows.Forms.DataGridViewTextBoxColumn QthField0;
        private System.Windows.Forms.DataGridViewTextBoxColumn QthField1;
        private System.Windows.Forms.DataGridViewTextBoxColumn QthField2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Locator;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comments;
        private System.Windows.Forms.ImageList imageListServerStates;
    }
}