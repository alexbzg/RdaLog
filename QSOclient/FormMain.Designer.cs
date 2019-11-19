using HamRadio;
namespace tnxlog
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.checkBoxTop = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panelStatusFields = new System.Windows.Forms.Panel();
            this.checkBoxAutoLocator = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoRafa = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoRda = new System.Windows.Forms.CheckBox();
            this.labelUserField = new System.Windows.Forms.Label();
            this.textBoxUserField = new System.Windows.Forms.TextBox();
            this.labelLocator = new System.Windows.Forms.Label();
            this.textBoxLocator = new System.Windows.Forms.TextBox();
            this.labelRafa = new System.Windows.Forms.Label();
            this.textBoxRafa = new System.Windows.Forms.TextBox();
            this.labelRda = new System.Windows.Forms.Label();
            this.textBoxRda = new System.Windows.Forms.TextBox();
            this.panelStatFilter = new System.Windows.Forms.Panel();
            this.labelStatMode = new System.Windows.Forms.Label();
            this.labelStatBand = new System.Windows.Forms.Label();
            this.labelStatCallsignsCaption = new System.Windows.Forms.Label();
            this.labelStatQsoCaption = new System.Windows.Forms.Label();
            this.labelStatQso = new System.Windows.Forms.Label();
            this.labelStatCallsigns = new System.Windows.Forms.Label();
            this.comboBoxStatFilterBand = new System.Windows.Forms.ComboBox();
            this.comboBoxStatFilterRda = new System.Windows.Forms.ComboBox();
            this.comboBoxStatFilterMode = new System.Windows.Forms.ComboBox();
            this.checkBoxAutoStatFilter = new System.Windows.Forms.CheckBox();
            this.labelStatFilter = new System.Windows.Forms.Label();
            this.panelCallsignId = new System.Windows.Forms.Panel();
            this.listBoxCallsignsDb = new System.Windows.Forms.ListBox();
            this.listBoxCallsignsQso = new System.Windows.Forms.ListBox();
            this.panelCwMacro = new System.Windows.Forms.Panel();
            this.labelHotKeyF5 = new System.Windows.Forms.Label();
            this.labelHotKeyF1 = new System.Windows.Forms.Label();
            this.labelHotKeyF2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.labelHotKeyF3 = new System.Windows.Forms.Label();
            this.labelHotKeyF9Bind = new System.Windows.Forms.Label();
            this.labelHotKeyF4 = new System.Windows.Forms.Label();
            this.labelHotKeyF8Bind = new System.Windows.Forms.Label();
            this.labelHotKeyF6 = new System.Windows.Forms.Label();
            this.labelHotKeyF7Bind = new System.Windows.Forms.Label();
            this.labelHotKeyF7 = new System.Windows.Forms.Label();
            this.labelHotKeyF6Bind = new System.Windows.Forms.Label();
            this.labelHotKeyF8 = new System.Windows.Forms.Label();
            this.labelHotKeyF5Bind = new System.Windows.Forms.Label();
            this.labelHotKeyF9 = new System.Windows.Forms.Label();
            this.labelHotKeyF4Bind = new System.Windows.Forms.Label();
            this.labelHotKeyF1Bind = new System.Windows.Forms.Label();
            this.labelHotKeyF3Bind = new System.Windows.Forms.Label();
            this.labelHotKeyF2Bind = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.connectionStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelMode = new System.Windows.Forms.Label();
            this.comboBoxMode = new System.Windows.Forms.ComboBox();
            this.labelFreq = new System.Windows.Forms.Label();
            this.numericUpDownFreq = new System.Windows.Forms.NumericUpDown();
            this.labelCallsign = new System.Windows.Forms.Label();
            this.textBoxCallsign = new System.Windows.Forms.TextBoxCallsign();
            this.labelRstRcvd = new System.Windows.Forms.Label();
            this.textBoxRstRcvd = new System.Windows.Forms.TextBox();
            this.labelRstSent = new System.Windows.Forms.Label();
            this.textBoxRstSent = new System.Windows.Forms.TextBox();
            this.textBoxCorrespondent = new System.Windows.Forms.TextBoxCallsign();
            this.buttonPostFreq = new System.Windows.Forms.Button();
            this.labelDateTime = new System.Windows.Forms.Label();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.dropDownButtonFile = new System.Windows.Forms.ToolStripDropDownButton();
            this.menuItemFileClear = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAdifExport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAdifExportRda = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAdifExportRafa = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAdifExportAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripLabelSettings = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabelLog = new System.Windows.Forms.ToolStripLabel();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.labelDupe = new System.Windows.Forms.Label();
            this.labelComments = new System.Windows.Forms.Label();
            this.textBoxComments = new System.Windows.Forms.TextBoxCallsign();
            this.flowLayoutPanel.SuspendLayout();
            this.panelStatusFields.SuspendLayout();
            this.panelStatFilter.SuspendLayout();
            this.panelCallsignId.SuspendLayout();
            this.panelCwMacro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFreq)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxTop
            // 
            this.checkBoxTop.AutoSize = true;
            this.checkBoxTop.Location = new System.Drawing.Point(312, 8);
            this.checkBoxTop.Name = "checkBoxTop";
            this.checkBoxTop.Size = new System.Drawing.Size(45, 17);
            this.checkBoxTop.TabIndex = 64;
            this.checkBoxTop.Text = "Top";
            this.checkBoxTop.UseVisualStyleBackColor = true;
            this.checkBoxTop.CheckedChanged += new System.EventHandler(this.CheckBoxTop_CheckedChanged);
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel.AutoSize = true;
            this.flowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel.Controls.Add(this.panelStatusFields);
            this.flowLayoutPanel.Controls.Add(this.panelStatFilter);
            this.flowLayoutPanel.Controls.Add(this.panelCallsignId);
            this.flowLayoutPanel.Controls.Add(this.panelCwMacro);
            this.flowLayoutPanel.Controls.Add(this.statusStrip);
            this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel.Location = new System.Drawing.Point(0, 167);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(364, 301);
            this.flowLayoutPanel.TabIndex = 63;
            // 
            // panelStatusFields
            // 
            this.panelStatusFields.Controls.Add(this.checkBoxAutoLocator);
            this.panelStatusFields.Controls.Add(this.checkBoxAutoRafa);
            this.panelStatusFields.Controls.Add(this.checkBoxAutoRda);
            this.panelStatusFields.Controls.Add(this.labelUserField);
            this.panelStatusFields.Controls.Add(this.textBoxUserField);
            this.panelStatusFields.Controls.Add(this.labelLocator);
            this.panelStatusFields.Controls.Add(this.textBoxLocator);
            this.panelStatusFields.Controls.Add(this.labelRafa);
            this.panelStatusFields.Controls.Add(this.textBoxRafa);
            this.panelStatusFields.Controls.Add(this.labelRda);
            this.panelStatusFields.Controls.Add(this.textBoxRda);
            this.panelStatusFields.Location = new System.Drawing.Point(0, 0);
            this.panelStatusFields.Margin = new System.Windows.Forms.Padding(0);
            this.panelStatusFields.Name = "panelStatusFields";
            this.panelStatusFields.Size = new System.Drawing.Size(364, 102);
            this.panelStatusFields.TabIndex = 63;
            // 
            // checkBoxAutoLocator
            // 
            this.checkBoxAutoLocator.AutoSize = true;
            this.checkBoxAutoLocator.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.checkBoxAutoLocator.Location = new System.Drawing.Point(55, 53);
            this.checkBoxAutoLocator.Name = "checkBoxAutoLocator";
            this.checkBoxAutoLocator.Size = new System.Drawing.Size(47, 17);
            this.checkBoxAutoLocator.TabIndex = 27;
            this.checkBoxAutoLocator.TabStop = false;
            this.checkBoxAutoLocator.Text = "auto";
            this.checkBoxAutoLocator.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutoRafa
            // 
            this.checkBoxAutoRafa.AutoSize = true;
            this.checkBoxAutoRafa.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.checkBoxAutoRafa.Location = new System.Drawing.Point(225, 4);
            this.checkBoxAutoRafa.Name = "checkBoxAutoRafa";
            this.checkBoxAutoRafa.Size = new System.Drawing.Size(47, 17);
            this.checkBoxAutoRafa.TabIndex = 26;
            this.checkBoxAutoRafa.TabStop = false;
            this.checkBoxAutoRafa.Text = "auto";
            this.checkBoxAutoRafa.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutoRda
            // 
            this.checkBoxAutoRda.AutoSize = true;
            this.checkBoxAutoRda.Checked = true;
            this.checkBoxAutoRda.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoRda.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.checkBoxAutoRda.Location = new System.Drawing.Point(44, 4);
            this.checkBoxAutoRda.Name = "checkBoxAutoRda";
            this.checkBoxAutoRda.Size = new System.Drawing.Size(47, 17);
            this.checkBoxAutoRda.TabIndex = 25;
            this.checkBoxAutoRda.TabStop = false;
            this.checkBoxAutoRda.Text = "auto";
            this.checkBoxAutoRda.UseVisualStyleBackColor = true;
            // 
            // labelUserField
            // 
            this.labelUserField.AutoSize = true;
            this.labelUserField.BackColor = System.Drawing.Color.Transparent;
            this.labelUserField.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelUserField.Location = new System.Drawing.Point(188, 54);
            this.labelUserField.Name = "labelUserField";
            this.labelUserField.Size = new System.Drawing.Size(51, 13);
            this.labelUserField.TabIndex = 24;
            this.labelUserField.Text = "User field";
            // 
            // textBoxUserField
            // 
            this.textBoxUserField.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.textBoxUserField.ForeColor = System.Drawing.Color.Navy;
            this.textBoxUserField.Location = new System.Drawing.Point(189, 70);
            this.textBoxUserField.Name = "textBoxUserField";
            this.textBoxUserField.Size = new System.Drawing.Size(168, 23);
            this.textBoxUserField.TabIndex = 9;
            this.textBoxUserField.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelLocator
            // 
            this.labelLocator.AutoSize = true;
            this.labelLocator.BackColor = System.Drawing.Color.Transparent;
            this.labelLocator.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelLocator.Location = new System.Drawing.Point(11, 54);
            this.labelLocator.Name = "labelLocator";
            this.labelLocator.Size = new System.Drawing.Size(43, 13);
            this.labelLocator.TabIndex = 22;
            this.labelLocator.Text = "Locator";
            // 
            // textBoxLocator
            // 
            this.textBoxLocator.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxLocator.ForeColor = System.Drawing.Color.Navy;
            this.textBoxLocator.Location = new System.Drawing.Point(12, 70);
            this.textBoxLocator.Name = "textBoxLocator";
            this.textBoxLocator.Size = new System.Drawing.Size(171, 22);
            this.textBoxLocator.TabIndex = 8;
            this.textBoxLocator.Text = "KN96on";
            this.textBoxLocator.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxLocator.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxLocator_Validating);
            // 
            // labelRafa
            // 
            this.labelRafa.AutoSize = true;
            this.labelRafa.BackColor = System.Drawing.Color.Transparent;
            this.labelRafa.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelRafa.Location = new System.Drawing.Point(188, 5);
            this.labelRafa.Name = "labelRafa";
            this.labelRafa.Size = new System.Drawing.Size(35, 13);
            this.labelRafa.TabIndex = 20;
            this.labelRafa.Text = "RAFA";
            // 
            // textBoxRafa
            // 
            this.textBoxRafa.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.textBoxRafa.ForeColor = System.Drawing.Color.Navy;
            this.textBoxRafa.Location = new System.Drawing.Point(189, 21);
            this.textBoxRafa.Name = "textBoxRafa";
            this.textBoxRafa.Size = new System.Drawing.Size(168, 23);
            this.textBoxRafa.TabIndex = 7;
            this.textBoxRafa.Text = "H4RT";
            this.textBoxRafa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxRafa.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxRafa_Validating);
            // 
            // labelRda
            // 
            this.labelRda.AutoSize = true;
            this.labelRda.BackColor = System.Drawing.Color.Transparent;
            this.labelRda.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelRda.Location = new System.Drawing.Point(11, 5);
            this.labelRda.Name = "labelRda";
            this.labelRda.Size = new System.Drawing.Size(30, 13);
            this.labelRda.TabIndex = 18;
            this.labelRda.Text = "RDA";
            // 
            // textBoxRda
            // 
            this.textBoxRda.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.textBoxRda.ForeColor = System.Drawing.Color.Navy;
            this.textBoxRda.Location = new System.Drawing.Point(12, 21);
            this.textBoxRda.Name = "textBoxRda";
            this.textBoxRda.Size = new System.Drawing.Size(171, 23);
            this.textBoxRda.TabIndex = 7;
            this.textBoxRda.Text = "RA-25 RA-37 RA-27";
            this.textBoxRda.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxRda.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxRda_Validating);
            // 
            // panelStatFilter
            // 
            this.panelStatFilter.Controls.Add(this.labelStatMode);
            this.panelStatFilter.Controls.Add(this.labelStatBand);
            this.panelStatFilter.Controls.Add(this.labelStatCallsignsCaption);
            this.panelStatFilter.Controls.Add(this.labelStatQsoCaption);
            this.panelStatFilter.Controls.Add(this.labelStatQso);
            this.panelStatFilter.Controls.Add(this.labelStatCallsigns);
            this.panelStatFilter.Controls.Add(this.comboBoxStatFilterBand);
            this.panelStatFilter.Controls.Add(this.comboBoxStatFilterRda);
            this.panelStatFilter.Controls.Add(this.comboBoxStatFilterMode);
            this.panelStatFilter.Controls.Add(this.checkBoxAutoStatFilter);
            this.panelStatFilter.Controls.Add(this.labelStatFilter);
            this.panelStatFilter.Location = new System.Drawing.Point(0, 102);
            this.panelStatFilter.Margin = new System.Windows.Forms.Padding(0);
            this.panelStatFilter.Name = "panelStatFilter";
            this.panelStatFilter.Size = new System.Drawing.Size(363, 50);
            this.panelStatFilter.TabIndex = 28;
            // 
            // labelStatMode
            // 
            this.labelStatMode.AutoSize = true;
            this.labelStatMode.BackColor = System.Drawing.Color.Transparent;
            this.labelStatMode.ForeColor = System.Drawing.Color.DarkGreen;
            this.labelStatMode.Location = new System.Drawing.Point(84, 2);
            this.labelStatMode.Name = "labelStatMode";
            this.labelStatMode.Size = new System.Drawing.Size(34, 13);
            this.labelStatMode.TabIndex = 63;
            this.labelStatMode.Text = "Mode";
            // 
            // labelStatBand
            // 
            this.labelStatBand.AutoSize = true;
            this.labelStatBand.BackColor = System.Drawing.Color.Transparent;
            this.labelStatBand.ForeColor = System.Drawing.Color.DarkGreen;
            this.labelStatBand.Location = new System.Drawing.Point(153, 2);
            this.labelStatBand.Name = "labelStatBand";
            this.labelStatBand.Size = new System.Drawing.Size(29, 13);
            this.labelStatBand.TabIndex = 62;
            this.labelStatBand.Text = "MHz";
            // 
            // labelStatCallsignsCaption
            // 
            this.labelStatCallsignsCaption.AutoSize = true;
            this.labelStatCallsignsCaption.BackColor = System.Drawing.Color.Transparent;
            this.labelStatCallsignsCaption.ForeColor = System.Drawing.Color.DarkGreen;
            this.labelStatCallsignsCaption.Location = new System.Drawing.Point(309, 4);
            this.labelStatCallsignsCaption.Name = "labelStatCallsignsCaption";
            this.labelStatCallsignsCaption.Size = new System.Drawing.Size(29, 13);
            this.labelStatCallsignsCaption.TabIndex = 41;
            this.labelStatCallsignsCaption.Text = "Calls";
            // 
            // labelStatQsoCaption
            // 
            this.labelStatQsoCaption.AutoSize = true;
            this.labelStatQsoCaption.BackColor = System.Drawing.Color.Transparent;
            this.labelStatQsoCaption.ForeColor = System.Drawing.Color.DarkGreen;
            this.labelStatQsoCaption.Location = new System.Drawing.Point(247, 5);
            this.labelStatQsoCaption.Name = "labelStatQsoCaption";
            this.labelStatQsoCaption.Size = new System.Drawing.Size(30, 13);
            this.labelStatQsoCaption.TabIndex = 40;
            this.labelStatQsoCaption.Text = "QSO";
            // 
            // labelStatQso
            // 
            this.labelStatQso.BackColor = System.Drawing.SystemColors.Window;
            this.labelStatQso.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStatQso.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.labelStatQso.ForeColor = System.Drawing.Color.DarkGreen;
            this.labelStatQso.Location = new System.Drawing.Point(232, 18);
            this.labelStatQso.Name = "labelStatQso";
            this.labelStatQso.Size = new System.Drawing.Size(60, 23);
            this.labelStatQso.TabIndex = 61;
            this.labelStatQso.Text = "0";
            this.labelStatQso.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStatCallsigns
            // 
            this.labelStatCallsigns.BackColor = System.Drawing.SystemColors.Window;
            this.labelStatCallsigns.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStatCallsigns.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.labelStatCallsigns.ForeColor = System.Drawing.Color.DarkGreen;
            this.labelStatCallsigns.Location = new System.Drawing.Point(296, 18);
            this.labelStatCallsigns.Name = "labelStatCallsigns";
            this.labelStatCallsigns.Size = new System.Drawing.Size(60, 23);
            this.labelStatCallsigns.TabIndex = 61;
            this.labelStatCallsigns.Text = "0";
            this.labelStatCallsigns.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxStatFilterBand
            // 
            this.comboBoxStatFilterBand.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxStatFilterBand.ForeColor = System.Drawing.Color.DarkGreen;
            this.comboBoxStatFilterBand.FormattingEnabled = true;
            this.comboBoxStatFilterBand.Items.AddRange(new object[] {
            "All"});
            this.comboBoxStatFilterBand.Location = new System.Drawing.Point(156, 17);
            this.comboBoxStatFilterBand.Name = "comboBoxStatFilterBand";
            this.comboBoxStatFilterBand.Size = new System.Drawing.Size(70, 24);
            this.comboBoxStatFilterBand.TabIndex = 36;
            this.comboBoxStatFilterBand.TabStop = false;
            this.comboBoxStatFilterBand.Text = "All";
            this.comboBoxStatFilterBand.SelectedIndexChanged += new System.EventHandler(this.StatFilter_SelectedIndexChanged);
            // 
            // comboBoxStatFilterRda
            // 
            this.comboBoxStatFilterRda.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxStatFilterRda.ForeColor = System.Drawing.Color.DarkGreen;
            this.comboBoxStatFilterRda.FormattingEnabled = true;
            this.comboBoxStatFilterRda.Items.AddRange(new object[] {
            "All"});
            this.comboBoxStatFilterRda.Location = new System.Drawing.Point(12, 17);
            this.comboBoxStatFilterRda.Name = "comboBoxStatFilterRda";
            this.comboBoxStatFilterRda.Size = new System.Drawing.Size(67, 24);
            this.comboBoxStatFilterRda.TabIndex = 35;
            this.comboBoxStatFilterRda.TabStop = false;
            this.comboBoxStatFilterRda.Text = "All";
            this.comboBoxStatFilterRda.SelectedIndexChanged += new System.EventHandler(this.StatFilter_SelectedIndexChanged);
            // 
            // comboBoxStatFilterMode
            // 
            this.comboBoxStatFilterMode.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxStatFilterMode.ForeColor = System.Drawing.Color.DarkGreen;
            this.comboBoxStatFilterMode.FormattingEnabled = true;
            this.comboBoxStatFilterMode.Items.AddRange(new object[] {
            "All"});
            this.comboBoxStatFilterMode.Location = new System.Drawing.Point(83, 17);
            this.comboBoxStatFilterMode.Name = "comboBoxStatFilterMode";
            this.comboBoxStatFilterMode.Size = new System.Drawing.Size(67, 24);
            this.comboBoxStatFilterMode.TabIndex = 34;
            this.comboBoxStatFilterMode.TabStop = false;
            this.comboBoxStatFilterMode.Text = "All";
            this.comboBoxStatFilterMode.SelectedIndexChanged += new System.EventHandler(this.StatFilter_SelectedIndexChanged);
            // 
            // checkBoxAutoStatFilter
            // 
            this.checkBoxAutoStatFilter.AutoSize = true;
            this.checkBoxAutoStatFilter.Checked = true;
            this.checkBoxAutoStatFilter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoStatFilter.ForeColor = System.Drawing.Color.DarkGreen;
            this.checkBoxAutoStatFilter.Location = new System.Drawing.Point(43, 1);
            this.checkBoxAutoStatFilter.Name = "checkBoxAutoStatFilter";
            this.checkBoxAutoStatFilter.Size = new System.Drawing.Size(47, 17);
            this.checkBoxAutoStatFilter.TabIndex = 30;
            this.checkBoxAutoStatFilter.TabStop = false;
            this.checkBoxAutoStatFilter.Text = "auto";
            this.checkBoxAutoStatFilter.UseVisualStyleBackColor = true;
            // 
            // labelStatFilter
            // 
            this.labelStatFilter.AutoSize = true;
            this.labelStatFilter.BackColor = System.Drawing.Color.Transparent;
            this.labelStatFilter.ForeColor = System.Drawing.Color.DarkGreen;
            this.labelStatFilter.Location = new System.Drawing.Point(11, 2);
            this.labelStatFilter.Name = "labelStatFilter";
            this.labelStatFilter.Size = new System.Drawing.Size(31, 13);
            this.labelStatFilter.TabIndex = 29;
            this.labelStatFilter.Text = "Stats";
            // 
            // panelCallsignId
            // 
            this.panelCallsignId.Controls.Add(this.listBoxCallsignsDb);
            this.panelCallsignId.Controls.Add(this.listBoxCallsignsQso);
            this.panelCallsignId.Location = new System.Drawing.Point(0, 152);
            this.panelCallsignId.Margin = new System.Windows.Forms.Padding(0);
            this.panelCallsignId.Name = "panelCallsignId";
            this.panelCallsignId.Size = new System.Drawing.Size(363, 91);
            this.panelCallsignId.TabIndex = 63;
            // 
            // listBoxCallsignsDb
            // 
            this.listBoxCallsignsDb.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxCallsignsDb.FormattingEnabled = true;
            this.listBoxCallsignsDb.ItemHeight = 18;
            this.listBoxCallsignsDb.Location = new System.Drawing.Point(184, 7);
            this.listBoxCallsignsDb.Name = "listBoxCallsignsDb";
            this.listBoxCallsignsDb.Size = new System.Drawing.Size(170, 76);
            this.listBoxCallsignsDb.TabIndex = 1;
            this.listBoxCallsignsDb.SelectedIndexChanged += new System.EventHandler(this.ListBoxCallsigns_SelectedIndexChanged);
            // 
            // listBoxCallsignsQso
            // 
            this.listBoxCallsignsQso.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxCallsignsQso.FormattingEnabled = true;
            this.listBoxCallsignsQso.ItemHeight = 18;
            this.listBoxCallsignsQso.Location = new System.Drawing.Point(9, 7);
            this.listBoxCallsignsQso.Name = "listBoxCallsignsQso";
            this.listBoxCallsignsQso.Size = new System.Drawing.Size(174, 76);
            this.listBoxCallsignsQso.TabIndex = 0;
            this.listBoxCallsignsQso.SelectedIndexChanged += new System.EventHandler(this.ListBoxCallsigns_SelectedIndexChanged);
            // 
            // panelCwMacro
            // 
            this.panelCwMacro.Controls.Add(this.labelHotKeyF5);
            this.panelCwMacro.Controls.Add(this.labelHotKeyF1);
            this.panelCwMacro.Controls.Add(this.labelHotKeyF2);
            this.panelCwMacro.Controls.Add(this.numericUpDown1);
            this.panelCwMacro.Controls.Add(this.labelHotKeyF3);
            this.panelCwMacro.Controls.Add(this.labelHotKeyF9Bind);
            this.panelCwMacro.Controls.Add(this.labelHotKeyF4);
            this.panelCwMacro.Controls.Add(this.labelHotKeyF8Bind);
            this.panelCwMacro.Controls.Add(this.labelHotKeyF6);
            this.panelCwMacro.Controls.Add(this.labelHotKeyF7Bind);
            this.panelCwMacro.Controls.Add(this.labelHotKeyF7);
            this.panelCwMacro.Controls.Add(this.labelHotKeyF6Bind);
            this.panelCwMacro.Controls.Add(this.labelHotKeyF8);
            this.panelCwMacro.Controls.Add(this.labelHotKeyF5Bind);
            this.panelCwMacro.Controls.Add(this.labelHotKeyF9);
            this.panelCwMacro.Controls.Add(this.labelHotKeyF4Bind);
            this.panelCwMacro.Controls.Add(this.labelHotKeyF1Bind);
            this.panelCwMacro.Controls.Add(this.labelHotKeyF3Bind);
            this.panelCwMacro.Controls.Add(this.labelHotKeyF2Bind);
            this.panelCwMacro.Location = new System.Drawing.Point(0, 243);
            this.panelCwMacro.Margin = new System.Windows.Forms.Padding(0);
            this.panelCwMacro.Name = "panelCwMacro";
            this.panelCwMacro.Size = new System.Drawing.Size(364, 36);
            this.panelCwMacro.TabIndex = 62;
            // 
            // labelHotKeyF5
            // 
            this.labelHotKeyF5.AutoSize = true;
            this.labelHotKeyF5.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF5.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelHotKeyF5.Location = new System.Drawing.Point(154, 4);
            this.labelHotKeyF5.Name = "labelHotKeyF5";
            this.labelHotKeyF5.Size = new System.Drawing.Size(19, 13);
            this.labelHotKeyF5.TabIndex = 47;
            this.labelHotKeyF5.Text = "F5";
            // 
            // labelHotKeyF1
            // 
            this.labelHotKeyF1.AutoSize = true;
            this.labelHotKeyF1.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelHotKeyF1.Location = new System.Drawing.Point(6, 4);
            this.labelHotKeyF1.Name = "labelHotKeyF1";
            this.labelHotKeyF1.Size = new System.Drawing.Size(19, 13);
            this.labelHotKeyF1.TabIndex = 43;
            this.labelHotKeyF1.Text = "F1";
            // 
            // labelHotKeyF2
            // 
            this.labelHotKeyF2.AutoSize = true;
            this.labelHotKeyF2.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelHotKeyF2.Location = new System.Drawing.Point(43, 4);
            this.labelHotKeyF2.Name = "labelHotKeyF2";
            this.labelHotKeyF2.Size = new System.Drawing.Size(19, 13);
            this.labelHotKeyF2.TabIndex = 44;
            this.labelHotKeyF2.Text = "F2";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(317, 2);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(37, 20);
            this.numericUpDown1.TabIndex = 62;
            this.numericUpDown1.TabStop = false;
            // 
            // labelHotKeyF3
            // 
            this.labelHotKeyF3.AutoSize = true;
            this.labelHotKeyF3.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelHotKeyF3.Location = new System.Drawing.Point(80, 4);
            this.labelHotKeyF3.Name = "labelHotKeyF3";
            this.labelHotKeyF3.Size = new System.Drawing.Size(19, 13);
            this.labelHotKeyF3.TabIndex = 45;
            this.labelHotKeyF3.Text = "F3";
            // 
            // labelHotKeyF9Bind
            // 
            this.labelHotKeyF9Bind.AutoSize = true;
            this.labelHotKeyF9Bind.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF9Bind.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelHotKeyF9Bind.Location = new System.Drawing.Point(301, 19);
            this.labelHotKeyF9Bind.Name = "labelHotKeyF9Bind";
            this.labelHotKeyF9Bind.Size = new System.Drawing.Size(16, 13);
            this.labelHotKeyF9Bind.TabIndex = 60;
            this.labelHotKeyF9Bind.Text = "...";
            // 
            // labelHotKeyF4
            // 
            this.labelHotKeyF4.AutoSize = true;
            this.labelHotKeyF4.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF4.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelHotKeyF4.Location = new System.Drawing.Point(117, 4);
            this.labelHotKeyF4.Name = "labelHotKeyF4";
            this.labelHotKeyF4.Size = new System.Drawing.Size(19, 13);
            this.labelHotKeyF4.TabIndex = 46;
            this.labelHotKeyF4.Text = "F4";
            // 
            // labelHotKeyF8Bind
            // 
            this.labelHotKeyF8Bind.AutoSize = true;
            this.labelHotKeyF8Bind.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF8Bind.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelHotKeyF8Bind.Location = new System.Drawing.Point(253, 19);
            this.labelHotKeyF8Bind.Name = "labelHotKeyF8Bind";
            this.labelHotKeyF8Bind.Size = new System.Drawing.Size(35, 13);
            this.labelHotKeyF8Bind.TabIndex = 59;
            this.labelHotKeyF8Bind.Text = "RAFA";
            // 
            // labelHotKeyF6
            // 
            this.labelHotKeyF6.AutoSize = true;
            this.labelHotKeyF6.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF6.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelHotKeyF6.Location = new System.Drawing.Point(191, 4);
            this.labelHotKeyF6.Name = "labelHotKeyF6";
            this.labelHotKeyF6.Size = new System.Drawing.Size(19, 13);
            this.labelHotKeyF6.TabIndex = 48;
            this.labelHotKeyF6.Text = "F6";
            // 
            // labelHotKeyF7Bind
            // 
            this.labelHotKeyF7Bind.AutoSize = true;
            this.labelHotKeyF7Bind.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF7Bind.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelHotKeyF7Bind.Location = new System.Drawing.Point(210, 19);
            this.labelHotKeyF7Bind.Name = "labelHotKeyF7Bind";
            this.labelHotKeyF7Bind.Size = new System.Drawing.Size(30, 13);
            this.labelHotKeyF7Bind.TabIndex = 58;
            this.labelHotKeyF7Bind.Text = "RDA";
            // 
            // labelHotKeyF7
            // 
            this.labelHotKeyF7.AutoSize = true;
            this.labelHotKeyF7.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF7.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelHotKeyF7.Location = new System.Drawing.Point(228, 4);
            this.labelHotKeyF7.Name = "labelHotKeyF7";
            this.labelHotKeyF7.Size = new System.Drawing.Size(19, 13);
            this.labelHotKeyF7.TabIndex = 49;
            this.labelHotKeyF7.Text = "F7";
            // 
            // labelHotKeyF6Bind
            // 
            this.labelHotKeyF6Bind.AutoSize = true;
            this.labelHotKeyF6Bind.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF6Bind.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelHotKeyF6Bind.Location = new System.Drawing.Point(181, 19);
            this.labelHotKeyF6Bind.Name = "labelHotKeyF6Bind";
            this.labelHotKeyF6Bind.Size = new System.Drawing.Size(16, 13);
            this.labelHotKeyF6Bind.TabIndex = 57;
            this.labelHotKeyF6Bind.Text = "...";
            // 
            // labelHotKeyF8
            // 
            this.labelHotKeyF8.AutoSize = true;
            this.labelHotKeyF8.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF8.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelHotKeyF8.Location = new System.Drawing.Point(265, 4);
            this.labelHotKeyF8.Name = "labelHotKeyF8";
            this.labelHotKeyF8.Size = new System.Drawing.Size(19, 13);
            this.labelHotKeyF8.TabIndex = 50;
            this.labelHotKeyF8.Text = "F8";
            // 
            // labelHotKeyF5Bind
            // 
            this.labelHotKeyF5Bind.AutoSize = true;
            this.labelHotKeyF5Bind.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF5Bind.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelHotKeyF5Bind.Location = new System.Drawing.Point(143, 19);
            this.labelHotKeyF5Bind.Name = "labelHotKeyF5Bind";
            this.labelHotKeyF5Bind.Size = new System.Drawing.Size(25, 13);
            this.labelHotKeyF5Bind.TabIndex = 56;
            this.labelHotKeyF5Bind.Text = "HIS";
            // 
            // labelHotKeyF9
            // 
            this.labelHotKeyF9.AutoSize = true;
            this.labelHotKeyF9.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF9.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelHotKeyF9.Location = new System.Drawing.Point(290, 4);
            this.labelHotKeyF9.Name = "labelHotKeyF9";
            this.labelHotKeyF9.Size = new System.Drawing.Size(19, 13);
            this.labelHotKeyF9.TabIndex = 51;
            this.labelHotKeyF9.Text = "F9";
            // 
            // labelHotKeyF4Bind
            // 
            this.labelHotKeyF4Bind.AutoSize = true;
            this.labelHotKeyF4Bind.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF4Bind.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelHotKeyF4Bind.Location = new System.Drawing.Point(107, 19);
            this.labelHotKeyF4Bind.Name = "labelHotKeyF4Bind";
            this.labelHotKeyF4Bind.Size = new System.Drawing.Size(23, 13);
            this.labelHotKeyF4Bind.TabIndex = 55;
            this.labelHotKeyF4Bind.Text = "MY";
            // 
            // labelHotKeyF1Bind
            // 
            this.labelHotKeyF1Bind.AutoSize = true;
            this.labelHotKeyF1Bind.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF1Bind.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelHotKeyF1Bind.Location = new System.Drawing.Point(-1, 19);
            this.labelHotKeyF1Bind.Name = "labelHotKeyF1Bind";
            this.labelHotKeyF1Bind.Size = new System.Drawing.Size(22, 13);
            this.labelHotKeyF1Bind.TabIndex = 52;
            this.labelHotKeyF1Bind.Text = "CQ";
            // 
            // labelHotKeyF3Bind
            // 
            this.labelHotKeyF3Bind.AutoSize = true;
            this.labelHotKeyF3Bind.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF3Bind.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelHotKeyF3Bind.Location = new System.Drawing.Point(72, 19);
            this.labelHotKeyF3Bind.Name = "labelHotKeyF3Bind";
            this.labelHotKeyF3Bind.Size = new System.Drawing.Size(22, 13);
            this.labelHotKeyF3Bind.TabIndex = 54;
            this.labelHotKeyF3Bind.Text = "TU";
            // 
            // labelHotKeyF2Bind
            // 
            this.labelHotKeyF2Bind.AutoSize = true;
            this.labelHotKeyF2Bind.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF2Bind.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelHotKeyF2Bind.Location = new System.Drawing.Point(34, 19);
            this.labelHotKeyF2Bind.Name = "labelHotKeyF2Bind";
            this.labelHotKeyF2Bind.Size = new System.Drawing.Size(25, 13);
            this.labelHotKeyF2Bind.TabIndex = 53;
            this.labelHotKeyF2Bind.Text = "599";
            // 
            // statusStrip
            // 
            this.statusStrip.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.statusStrip.AutoSize = false;
            this.statusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 279);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(364, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 0;
            // 
            // connectionStatusLabel
            // 
            this.connectionStatusLabel.Name = "connectionStatusLabel";
            this.connectionStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // labelMode
            // 
            this.labelMode.AutoSize = true;
            this.labelMode.BackColor = System.Drawing.Color.Transparent;
            this.labelMode.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelMode.Location = new System.Drawing.Point(171, 78);
            this.labelMode.Name = "labelMode";
            this.labelMode.Size = new System.Drawing.Size(34, 13);
            this.labelMode.TabIndex = 16;
            this.labelMode.Text = "Mode";
            // 
            // comboBoxMode
            // 
            this.comboBoxMode.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxMode.FormattingEnabled = true;
            this.comboBoxMode.Location = new System.Drawing.Point(174, 93);
            this.comboBoxMode.Name = "comboBoxMode";
            this.comboBoxMode.Size = new System.Drawing.Size(67, 24);
            this.comboBoxMode.TabIndex = 4;
            this.comboBoxMode.Text = "CW";
            this.comboBoxMode.SelectedIndexChanged += new System.EventHandler(this.ComboBoxMode_SelectedIndexChanged);
            // 
            // labelFreq
            // 
            this.labelFreq.AutoSize = true;
            this.labelFreq.BackColor = System.Drawing.Color.Transparent;
            this.labelFreq.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelFreq.Location = new System.Drawing.Point(247, 78);
            this.labelFreq.Name = "labelFreq";
            this.labelFreq.Size = new System.Drawing.Size(26, 13);
            this.labelFreq.TabIndex = 14;
            this.labelFreq.Text = "kHz";
            // 
            // numericUpDownFreq
            // 
            this.numericUpDownFreq.DecimalPlaces = 1;
            this.numericUpDownFreq.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.numericUpDownFreq.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownFreq.Location = new System.Drawing.Point(248, 94);
            this.numericUpDownFreq.Maximum = new decimal(new int[] {
            29000,
            0,
            0,
            0});
            this.numericUpDownFreq.Minimum = new decimal(new int[] {
            1800,
            0,
            0,
            0});
            this.numericUpDownFreq.Name = "numericUpDownFreq";
            this.numericUpDownFreq.Size = new System.Drawing.Size(79, 23);
            this.numericUpDownFreq.TabIndex = 5;
            this.numericUpDownFreq.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownFreq.ThousandsSeparator = true;
            this.numericUpDownFreq.Value = new decimal(new int[] {
            140000,
            0,
            0,
            65536});
            this.numericUpDownFreq.ValueChanged += new System.EventHandler(this.NumericUpDownFreq_ValueChanged);
            // 
            // labelCallsign
            // 
            this.labelCallsign.AutoSize = true;
            this.labelCallsign.BackColor = System.Drawing.Color.Transparent;
            this.labelCallsign.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelCallsign.Location = new System.Drawing.Point(11, 78);
            this.labelCallsign.Name = "labelCallsign";
            this.labelCallsign.Size = new System.Drawing.Size(59, 13);
            this.labelCallsign.TabIndex = 12;
            this.labelCallsign.Text = "My callsign";
            // 
            // textBoxCallsign
            // 
            this.textBoxCallsign.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxCallsign.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.textBoxCallsign.Location = new System.Drawing.Point(12, 94);
            this.textBoxCallsign.Name = "textBoxCallsign";
            this.textBoxCallsign.Size = new System.Drawing.Size(156, 23);
            this.textBoxCallsign.TabIndex = 3;
            this.textBoxCallsign.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxCallsign.Validated += new System.EventHandler(this.TextBoxCallsign_Validated);
            // 
            // labelRstRcvd
            // 
            this.labelRstRcvd.AutoSize = true;
            this.labelRstRcvd.BackColor = System.Drawing.Color.Transparent;
            this.labelRstRcvd.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelRstRcvd.Location = new System.Drawing.Point(304, 36);
            this.labelRstRcvd.Name = "labelRstRcvd";
            this.labelRstRcvd.Size = new System.Drawing.Size(53, 13);
            this.labelRstRcvd.TabIndex = 10;
            this.labelRstRcvd.Text = "RST rcvd";
            // 
            // textBoxRstRcvd
            // 
            this.textBoxRstRcvd.Font = new System.Drawing.Font("Arial", 10F);
            this.textBoxRstRcvd.Location = new System.Drawing.Point(305, 52);
            this.textBoxRstRcvd.Name = "textBoxRstRcvd";
            this.textBoxRstRcvd.Size = new System.Drawing.Size(51, 23);
            this.textBoxRstRcvd.TabIndex = 2;
            this.textBoxRstRcvd.Text = "599";
            this.textBoxRstRcvd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelRstSent
            // 
            this.labelRstSent.AutoSize = true;
            this.labelRstSent.BackColor = System.Drawing.Color.Transparent;
            this.labelRstSent.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelRstSent.Location = new System.Drawing.Point(247, 36);
            this.labelRstSent.Name = "labelRstSent";
            this.labelRstSent.Size = new System.Drawing.Size(52, 13);
            this.labelRstSent.TabIndex = 8;
            this.labelRstSent.Text = "RST sent";
            // 
            // textBoxRstSent
            // 
            this.textBoxRstSent.Font = new System.Drawing.Font("Arial", 10F);
            this.textBoxRstSent.Location = new System.Drawing.Point(248, 52);
            this.textBoxRstSent.Name = "textBoxRstSent";
            this.textBoxRstSent.Size = new System.Drawing.Size(51, 23);
            this.textBoxRstSent.TabIndex = 1;
            this.textBoxRstSent.Text = "599";
            this.textBoxRstSent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxCorrespondent
            // 
            this.textBoxCorrespondent.BackColor = System.Drawing.SystemColors.Info;
            this.textBoxCorrespondent.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxCorrespondent.Location = new System.Drawing.Point(12, 46);
            this.textBoxCorrespondent.Name = "textBoxCorrespondent";
            this.textBoxCorrespondent.Size = new System.Drawing.Size(229, 29);
            this.textBoxCorrespondent.TabIndex = 0;
            this.textBoxCorrespondent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxCorrespondent.TextChanged += new System.EventHandler(this.TextBoxCorrespondent_TextChanged);
            // 
            // buttonPostFreq
            // 
            this.buttonPostFreq.BackColor = System.Drawing.SystemColors.Control;
            this.buttonPostFreq.Image = global::tnxlog.Properties.Resources.chat;
            this.buttonPostFreq.Location = new System.Drawing.Point(330, 93);
            this.buttonPostFreq.Name = "buttonPostFreq";
            this.buttonPostFreq.Size = new System.Drawing.Size(24, 24);
            this.buttonPostFreq.TabIndex = 5;
            this.buttonPostFreq.TabStop = false;
            this.buttonPostFreq.UseVisualStyleBackColor = false;
            this.buttonPostFreq.Click += new System.EventHandler(this.ButtonPostFreq_Click);
            // 
            // labelDateTime
            // 
            this.labelDateTime.BackColor = System.Drawing.Color.Transparent;
            this.labelDateTime.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelDateTime.Location = new System.Drawing.Point(12, 30);
            this.labelDateTime.Name = "labelDateTime";
            this.labelDateTime.Size = new System.Drawing.Size(229, 13);
            this.labelDateTime.TabIndex = 4;
            this.labelDateTime.Text = "15 May 2019   15:12z";
            this.labelDateTime.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dropDownButtonFile,
            this.toolStripLabelSettings,
            this.toolStripLabelLog});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(364, 25);
            this.toolStrip.TabIndex = 1;
            // 
            // dropDownButtonFile
            // 
            this.dropDownButtonFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.dropDownButtonFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFileClear,
            this.menuItemAdifExport});
            this.dropDownButtonFile.Image = ((System.Drawing.Image)(resources.GetObject("dropDownButtonFile.Image")));
            this.dropDownButtonFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.dropDownButtonFile.Name = "dropDownButtonFile";
            this.dropDownButtonFile.ShowDropDownArrow = false;
            this.dropDownButtonFile.Size = new System.Drawing.Size(29, 22);
            this.dropDownButtonFile.Text = "File";
            // 
            // menuItemFileClear
            // 
            this.menuItemFileClear.Name = "menuItemFileClear";
            this.menuItemFileClear.Size = new System.Drawing.Size(107, 22);
            this.menuItemFileClear.Text = "New";
            this.menuItemFileClear.Click += new System.EventHandler(this.MenuItemFileClear_Click);
            // 
            // menuItemAdifExport
            // 
            this.menuItemAdifExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAdifExportRda,
            this.menuItemAdifExportRafa,
            this.menuItemAdifExportAll});
            this.menuItemAdifExport.Name = "menuItemAdifExport";
            this.menuItemAdifExport.Size = new System.Drawing.Size(107, 22);
            this.menuItemAdifExport.Text = "Export";
            // 
            // menuItemAdifExportRda
            // 
            this.menuItemAdifExportRda.Name = "menuItemAdifExportRda";
            this.menuItemAdifExportRda.Size = new System.Drawing.Size(118, 22);
            this.menuItemAdifExportRda.Text = "By RDA";
            this.menuItemAdifExportRda.Click += new System.EventHandler(this.MenuItemAdifExportRda_Click);
            // 
            // menuItemAdifExportRafa
            // 
            this.menuItemAdifExportRafa.Name = "menuItemAdifExportRafa";
            this.menuItemAdifExportRafa.Size = new System.Drawing.Size(118, 22);
            this.menuItemAdifExportRafa.Text = "By RAFA";
            this.menuItemAdifExportRafa.Click += new System.EventHandler(this.MenuItemAdifExportRafa_Click);
            // 
            // menuItemAdifExportAll
            // 
            this.menuItemAdifExportAll.Name = "menuItemAdifExportAll";
            this.menuItemAdifExportAll.Size = new System.Drawing.Size(118, 22);
            this.menuItemAdifExportAll.Text = "All QSO";
            this.menuItemAdifExportAll.Click += new System.EventHandler(this.MenuItemAdifExportAll_Click);
            // 
            // toolStripLabelSettings
            // 
            this.toolStripLabelSettings.Name = "toolStripLabelSettings";
            this.toolStripLabelSettings.Size = new System.Drawing.Size(49, 22);
            this.toolStripLabelSettings.Text = "Settings";
            this.toolStripLabelSettings.Click += new System.EventHandler(this.ToolStripLabelSettings_Click);
            // 
            // toolStripLabelLog
            // 
            this.toolStripLabelLog.Name = "toolStripLabelLog";
            this.toolStripLabelLog.Size = new System.Drawing.Size(27, 22);
            this.toolStripLabelLog.Text = "Log";
            this.toolStripLabelLog.Click += new System.EventHandler(this.ToolStripLabelLog_Click);
            // 
            // labelDupe
            // 
            this.labelDupe.BackColor = System.Drawing.Color.Transparent;
            this.labelDupe.ForeColor = System.Drawing.Color.Red;
            this.labelDupe.Location = new System.Drawing.Point(194, 30);
            this.labelDupe.Name = "labelDupe";
            this.labelDupe.Size = new System.Drawing.Size(45, 13);
            this.labelDupe.TabIndex = 65;
            this.labelDupe.Text = "Dupe";
            this.labelDupe.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.labelDupe.Visible = false;
            // 
            // labelComments
            // 
            this.labelComments.AutoSize = true;
            this.labelComments.BackColor = System.Drawing.Color.Transparent;
            this.labelComments.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelComments.Location = new System.Drawing.Point(12, 120);
            this.labelComments.Name = "labelComments";
            this.labelComments.Size = new System.Drawing.Size(56, 13);
            this.labelComments.TabIndex = 67;
            this.labelComments.Text = "Comments";
            // 
            // textBoxComments
            // 
            this.textBoxComments.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxComments.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxComments.Location = new System.Drawing.Point(13, 136);
            this.textBoxComments.Name = "textBoxComments";
            this.textBoxComments.Size = new System.Drawing.Size(344, 22);
            this.textBoxComments.TabIndex = 6;
            this.textBoxComments.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(364, 471);
            this.Controls.Add(this.labelComments);
            this.Controls.Add(this.textBoxComments);
            this.Controls.Add(this.labelDupe);
            this.Controls.Add(this.checkBoxTop);
            this.Controls.Add(this.flowLayoutPanel);
            this.Controls.Add(this.labelMode);
            this.Controls.Add(this.comboBoxMode);
            this.Controls.Add(this.labelFreq);
            this.Controls.Add(this.numericUpDownFreq);
            this.Controls.Add(this.labelCallsign);
            this.Controls.Add(this.textBoxCallsign);
            this.Controls.Add(this.labelRstRcvd);
            this.Controls.Add(this.textBoxRstRcvd);
            this.Controls.Add(this.labelRstSent);
            this.Controls.Add(this.textBoxRstSent);
            this.Controls.Add(this.textBoxCorrespondent);
            this.Controls.Add(this.buttonPostFreq);
            this.Controls.Add(this.labelDateTime);
            this.Controls.Add(this.toolStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "RDA Log";
            this.Activated += new System.EventHandler(this.FormMain_Activated);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormMain_KeyPress);
            this.flowLayoutPanel.ResumeLayout(false);
            this.panelStatusFields.ResumeLayout(false);
            this.panelStatusFields.PerformLayout();
            this.panelStatFilter.ResumeLayout(false);
            this.panelStatFilter.PerformLayout();
            this.panelCallsignId.ResumeLayout(false);
            this.panelCwMacro.ResumeLayout(false);
            this.panelCwMacro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFreq)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripLabel toolStripLabelSettings;
        private System.Windows.Forms.ToolStripLabel toolStripLabelLog;
        private System.Windows.Forms.Label labelDateTime;
        private System.Windows.Forms.Button buttonPostFreq;
        private System.Windows.Forms.TextBoxCallsign textBoxCorrespondent;
        private System.Windows.Forms.TextBox textBoxRstSent;
        private System.Windows.Forms.Label labelRstSent;
        private System.Windows.Forms.Label labelRstRcvd;
        private System.Windows.Forms.TextBox textBoxRstRcvd;
        private System.Windows.Forms.Label labelCallsign;
        private System.Windows.Forms.TextBoxCallsign textBoxCallsign;
        private System.Windows.Forms.Label labelFreq;
        private System.Windows.Forms.NumericUpDown numericUpDownFreq;
        private System.Windows.Forms.ComboBox comboBoxMode;
        private System.Windows.Forms.Label labelMode;
        private System.Windows.Forms.Label labelRda;
        private System.Windows.Forms.TextBox textBoxRda;
        private System.Windows.Forms.Label labelRafa;
        private System.Windows.Forms.TextBox textBoxRafa;
        private System.Windows.Forms.Label labelUserField;
        private System.Windows.Forms.TextBox textBoxUserField;
        private System.Windows.Forms.Label labelLocator;
        private System.Windows.Forms.TextBox textBoxLocator;
        private System.Windows.Forms.CheckBox checkBoxAutoRda;
        private System.Windows.Forms.CheckBox checkBoxAutoRafa;
        private System.Windows.Forms.CheckBox checkBoxAutoLocator;
        private System.Windows.Forms.CheckBox checkBoxAutoStatFilter;
        private System.Windows.Forms.ComboBox comboBoxStatFilterMode;
        private System.Windows.Forms.ComboBox comboBoxStatFilterRda;
        private System.Windows.Forms.Label labelStatFilter;
        private System.Windows.Forms.ComboBox comboBoxStatFilterBand;
        private System.Windows.Forms.Label labelStatCallsigns;
        private System.Windows.Forms.Label labelStatQso;
        private System.Windows.Forms.Label labelStatQsoCaption;
        private System.Windows.Forms.Label labelStatCallsignsCaption;
        private System.Windows.Forms.Label labelHotKeyF1;
        private System.Windows.Forms.Label labelHotKeyF2;
        private System.Windows.Forms.Label labelHotKeyF3;
        private System.Windows.Forms.Label labelHotKeyF4;
        private System.Windows.Forms.Label labelHotKeyF5;
        private System.Windows.Forms.Label labelHotKeyF6;
        private System.Windows.Forms.Label labelHotKeyF7;
        private System.Windows.Forms.Label labelHotKeyF8;
        private System.Windows.Forms.Label labelHotKeyF9;
        private System.Windows.Forms.Label labelHotKeyF9Bind;
        private System.Windows.Forms.Label labelHotKeyF8Bind;
        private System.Windows.Forms.Label labelHotKeyF7Bind;
        private System.Windows.Forms.Label labelHotKeyF6Bind;
        private System.Windows.Forms.Label labelHotKeyF5Bind;
        private System.Windows.Forms.Label labelHotKeyF4Bind;
        private System.Windows.Forms.Label labelHotKeyF3Bind;
        private System.Windows.Forms.Label labelHotKeyF2Bind;
        private System.Windows.Forms.Label labelHotKeyF1Bind;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Panel panelStatusFields;
        private System.Windows.Forms.Panel panelStatFilter;
        private System.Windows.Forms.Panel panelCwMacro;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Panel panelCallsignId;
        private System.Windows.Forms.ToolStripDropDownButton dropDownButtonFile;
        private System.Windows.Forms.ToolStripMenuItem menuItemAdifExport;
        private System.Windows.Forms.ToolStripMenuItem menuItemAdifExportRda;
        private System.Windows.Forms.ToolStripMenuItem menuItemAdifExportRafa;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ListBox listBoxCallsignsDb;
        private System.Windows.Forms.ListBox listBoxCallsignsQso;
        private System.Windows.Forms.CheckBox checkBoxTop;
        private System.Windows.Forms.ToolStripMenuItem menuItemAdifExportAll;
        private System.Windows.Forms.ToolStripMenuItem menuItemFileClear;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Label labelStatMode;
        private System.Windows.Forms.Label labelStatBand;
        private System.Windows.Forms.ToolStripStatusLabel connectionStatusLabel;
        private System.Windows.Forms.Label labelDupe;
        private System.Windows.Forms.Label labelComments;
        private System.Windows.Forms.TextBoxCallsign textBoxComments;
    }
}

