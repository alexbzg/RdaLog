namespace RdaLog
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
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
            this.userField});
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(800, 450);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.DataGridView_ColumnWidthChanged);
            // 
            // no
            // 
            this.no.DataPropertyName = "no";
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
            // FormLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView);
            this.Name = "FormLog";
            this.Text = "RDA Log - Log";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
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
    }
}