namespace RdaLog
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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripLabelFile = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabelSettings = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabelLog = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.labelDateTime = new System.Windows.Forms.Label();
            this.buttonPostFreq = new System.Windows.Forms.Button();
            this.textBoxCorrespondent = new System.Windows.Forms.TextBox();
            this.textBoxRstSent = new System.Windows.Forms.TextBox();
            this.labelRstSent = new System.Windows.Forms.Label();
            this.labelRstRcvd = new System.Windows.Forms.Label();
            this.textBoxRstRcvd = new System.Windows.Forms.TextBox();
            this.labelCallsign = new System.Windows.Forms.Label();
            this.textBoxCallsign = new System.Windows.Forms.TextBox();
            this.labelFreq = new System.Windows.Forms.Label();
            this.textBoxFreq = new System.Windows.Forms.TextBox();
            this.comboBoxMode = new System.Windows.Forms.ComboBox();
            this.labelMode = new System.Windows.Forms.Label();
            this.labelRda = new System.Windows.Forms.Label();
            this.textBoxRda = new System.Windows.Forms.TextBox();
            this.labelRafa = new System.Windows.Forms.Label();
            this.textBoxRafa = new System.Windows.Forms.TextBox();
            this.labelUserField = new System.Windows.Forms.Label();
            this.textBoxUserField = new System.Windows.Forms.TextBox();
            this.labelLocator = new System.Windows.Forms.Label();
            this.textBoxLocator = new System.Windows.Forms.TextBox();
            this.checkBoxAutoRda = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoRafa = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoLocator = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoStatFilter = new System.Windows.Forms.CheckBox();
            this.comboBoxStatFilterMode = new System.Windows.Forms.ComboBox();
            this.comboBoxStatFilterRda = new System.Windows.Forms.ComboBox();
            this.labelStatFilter = new System.Windows.Forms.Label();
            this.comboBoxStatFilterBand = new System.Windows.Forms.ComboBox();
            this.labelStatCallsigns = new System.Windows.Forms.Label();
            this.labelStatQso = new System.Windows.Forms.Label();
            this.labelStatQsoCaption = new System.Windows.Forms.Label();
            this.labelStatCallsignsCaption = new System.Windows.Forms.Label();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.labelHotKeyF1 = new System.Windows.Forms.Label();
            this.labelHotKeyF2 = new System.Windows.Forms.Label();
            this.labelHotKeyF3 = new System.Windows.Forms.Label();
            this.labelHotKeyF4 = new System.Windows.Forms.Label();
            this.labelHotKeyF5 = new System.Windows.Forms.Label();
            this.labelHotKeyF6 = new System.Windows.Forms.Label();
            this.labelHotKeyF7 = new System.Windows.Forms.Label();
            this.labelHotKeyF8 = new System.Windows.Forms.Label();
            this.labelHotKeyF9 = new System.Windows.Forms.Label();
            this.labelHotKeyF9Bind = new System.Windows.Forms.Label();
            this.labelHotKeyF8Bind = new System.Windows.Forms.Label();
            this.labelHotKeyF7Bind = new System.Windows.Forms.Label();
            this.labelHotKeyF6Bind = new System.Windows.Forms.Label();
            this.labelHotKeyF5Bind = new System.Windows.Forms.Label();
            this.labelHotKeyF4Bind = new System.Windows.Forms.Label();
            this.labelHotKeyF3Bind = new System.Windows.Forms.Label();
            this.labelHotKeyF2Bind = new System.Windows.Forms.Label();
            this.labelHotKeyF1Bind = new System.Windows.Forms.Label();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 439);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(364, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 0;
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelFile,
            this.toolStripLabelSettings,
            this.toolStripLabelLog,
            this.toolStripLabel3});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(364, 25);
            this.toolStrip.TabIndex = 1;
            // 
            // toolStripLabelFile
            // 
            this.toolStripLabelFile.Name = "toolStripLabelFile";
            this.toolStripLabelFile.Size = new System.Drawing.Size(25, 22);
            this.toolStripLabelFile.Text = "File";
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
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(28, 22);
            this.toolStripLabel3.Text = "Info";
            // 
            // labelDateTime
            // 
            this.labelDateTime.AutoSize = true;
            this.labelDateTime.BackColor = System.Drawing.Color.Transparent;
            this.labelDateTime.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelDateTime.Location = new System.Drawing.Point(12, 30);
            this.labelDateTime.Name = "labelDateTime";
            this.labelDateTime.Size = new System.Drawing.Size(110, 13);
            this.labelDateTime.TabIndex = 4;
            this.labelDateTime.Text = "15 May 2019   15:12z";
            // 
            // buttonPostFreq
            // 
            this.buttonPostFreq.Location = new System.Drawing.Point(333, 100);
            this.buttonPostFreq.Name = "buttonPostFreq";
            this.buttonPostFreq.Size = new System.Drawing.Size(23, 23);
            this.buttonPostFreq.TabIndex = 5;
            this.buttonPostFreq.UseVisualStyleBackColor = true;
            // 
            // textBoxCorrespondent
            // 
            this.textBoxCorrespondent.BackColor = System.Drawing.SystemColors.Info;
            this.textBoxCorrespondent.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxCorrespondent.Location = new System.Drawing.Point(12, 46);
            this.textBoxCorrespondent.Name = "textBoxCorrespondent";
            this.textBoxCorrespondent.Size = new System.Drawing.Size(229, 29);
            this.textBoxCorrespondent.TabIndex = 6;
            this.textBoxCorrespondent.Text = "OK/UZ6LWZ/P";
            this.textBoxCorrespondent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxRstSent
            // 
            this.textBoxRstSent.Font = new System.Drawing.Font("Arial", 10F);
            this.textBoxRstSent.Location = new System.Drawing.Point(248, 52);
            this.textBoxRstSent.Name = "textBoxRstSent";
            this.textBoxRstSent.Size = new System.Drawing.Size(51, 23);
            this.textBoxRstSent.TabIndex = 7;
            this.textBoxRstSent.Text = "599";
            this.textBoxRstSent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.textBoxRstRcvd.TabIndex = 9;
            this.textBoxRstRcvd.Text = "599";
            this.textBoxRstRcvd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelCallsign
            // 
            this.labelCallsign.AutoSize = true;
            this.labelCallsign.BackColor = System.Drawing.Color.Transparent;
            this.labelCallsign.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelCallsign.Location = new System.Drawing.Point(11, 84);
            this.labelCallsign.Name = "labelCallsign";
            this.labelCallsign.Size = new System.Drawing.Size(59, 13);
            this.labelCallsign.TabIndex = 12;
            this.labelCallsign.Text = "My callsign";
            // 
            // textBoxCallsign
            // 
            this.textBoxCallsign.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.textBoxCallsign.Location = new System.Drawing.Point(12, 100);
            this.textBoxCallsign.Name = "textBoxCallsign";
            this.textBoxCallsign.Size = new System.Drawing.Size(156, 23);
            this.textBoxCallsign.TabIndex = 11;
            this.textBoxCallsign.Text = "R7AB/P";
            this.textBoxCallsign.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelFreq
            // 
            this.labelFreq.AutoSize = true;
            this.labelFreq.BackColor = System.Drawing.Color.Transparent;
            this.labelFreq.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelFreq.Location = new System.Drawing.Point(247, 84);
            this.labelFreq.Name = "labelFreq";
            this.labelFreq.Size = new System.Drawing.Size(29, 13);
            this.labelFreq.TabIndex = 14;
            this.labelFreq.Text = "MHz";
            // 
            // textBoxFreq
            // 
            this.textBoxFreq.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.textBoxFreq.Location = new System.Drawing.Point(248, 100);
            this.textBoxFreq.Name = "textBoxFreq";
            this.textBoxFreq.Size = new System.Drawing.Size(79, 23);
            this.textBoxFreq.TabIndex = 13;
            this.textBoxFreq.Text = "14 177.3";
            this.textBoxFreq.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboBoxMode
            // 
            this.comboBoxMode.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxMode.FormattingEnabled = true;
            this.comboBoxMode.Location = new System.Drawing.Point(174, 99);
            this.comboBoxMode.Name = "comboBoxMode";
            this.comboBoxMode.Size = new System.Drawing.Size(67, 24);
            this.comboBoxMode.TabIndex = 15;
            this.comboBoxMode.Text = "RTTY";
            // 
            // labelMode
            // 
            this.labelMode.AutoSize = true;
            this.labelMode.BackColor = System.Drawing.Color.Transparent;
            this.labelMode.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelMode.Location = new System.Drawing.Point(171, 84);
            this.labelMode.Name = "labelMode";
            this.labelMode.Size = new System.Drawing.Size(34, 13);
            this.labelMode.TabIndex = 16;
            this.labelMode.Text = "Mode";
            // 
            // labelRda
            // 
            this.labelRda.AutoSize = true;
            this.labelRda.BackColor = System.Drawing.Color.Transparent;
            this.labelRda.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelRda.Location = new System.Drawing.Point(11, 137);
            this.labelRda.Name = "labelRda";
            this.labelRda.Size = new System.Drawing.Size(30, 13);
            this.labelRda.TabIndex = 18;
            this.labelRda.Text = "RDA";
            // 
            // textBoxRda
            // 
            this.textBoxRda.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.textBoxRda.ForeColor = System.Drawing.Color.Navy;
            this.textBoxRda.Location = new System.Drawing.Point(12, 153);
            this.textBoxRda.Name = "textBoxRda";
            this.textBoxRda.Size = new System.Drawing.Size(171, 23);
            this.textBoxRda.TabIndex = 17;
            this.textBoxRda.Text = "RA-25 RA-37 RA-27";
            this.textBoxRda.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelRafa
            // 
            this.labelRafa.AutoSize = true;
            this.labelRafa.BackColor = System.Drawing.Color.Transparent;
            this.labelRafa.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelRafa.Location = new System.Drawing.Point(188, 137);
            this.labelRafa.Name = "labelRafa";
            this.labelRafa.Size = new System.Drawing.Size(35, 13);
            this.labelRafa.TabIndex = 20;
            this.labelRafa.Text = "RAFA";
            // 
            // textBoxRafa
            // 
            this.textBoxRafa.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.textBoxRafa.ForeColor = System.Drawing.Color.Navy;
            this.textBoxRafa.Location = new System.Drawing.Point(189, 153);
            this.textBoxRafa.Name = "textBoxRafa";
            this.textBoxRafa.Size = new System.Drawing.Size(168, 23);
            this.textBoxRafa.TabIndex = 19;
            this.textBoxRafa.Text = "H4RT";
            this.textBoxRafa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelUserField
            // 
            this.labelUserField.AutoSize = true;
            this.labelUserField.BackColor = System.Drawing.Color.Transparent;
            this.labelUserField.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelUserField.Location = new System.Drawing.Point(188, 186);
            this.labelUserField.Name = "labelUserField";
            this.labelUserField.Size = new System.Drawing.Size(51, 13);
            this.labelUserField.TabIndex = 24;
            this.labelUserField.Text = "User field";
            // 
            // textBoxUserField
            // 
            this.textBoxUserField.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.textBoxUserField.ForeColor = System.Drawing.Color.Navy;
            this.textBoxUserField.Location = new System.Drawing.Point(189, 202);
            this.textBoxUserField.Name = "textBoxUserField";
            this.textBoxUserField.Size = new System.Drawing.Size(168, 23);
            this.textBoxUserField.TabIndex = 23;
            this.textBoxUserField.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelLocator
            // 
            this.labelLocator.AutoSize = true;
            this.labelLocator.BackColor = System.Drawing.Color.Transparent;
            this.labelLocator.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelLocator.Location = new System.Drawing.Point(11, 186);
            this.labelLocator.Name = "labelLocator";
            this.labelLocator.Size = new System.Drawing.Size(43, 13);
            this.labelLocator.TabIndex = 22;
            this.labelLocator.Text = "Locator";
            // 
            // textBoxLocator
            // 
            this.textBoxLocator.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxLocator.ForeColor = System.Drawing.Color.Navy;
            this.textBoxLocator.Location = new System.Drawing.Point(12, 202);
            this.textBoxLocator.Name = "textBoxLocator";
            this.textBoxLocator.Size = new System.Drawing.Size(171, 22);
            this.textBoxLocator.TabIndex = 21;
            this.textBoxLocator.Text = "KN96on";
            this.textBoxLocator.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkBoxAutoRda
            // 
            this.checkBoxAutoRda.AutoSize = true;
            this.checkBoxAutoRda.Checked = true;
            this.checkBoxAutoRda.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoRda.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.checkBoxAutoRda.Location = new System.Drawing.Point(44, 136);
            this.checkBoxAutoRda.Name = "checkBoxAutoRda";
            this.checkBoxAutoRda.Size = new System.Drawing.Size(47, 17);
            this.checkBoxAutoRda.TabIndex = 25;
            this.checkBoxAutoRda.Text = "auto";
            this.checkBoxAutoRda.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutoRafa
            // 
            this.checkBoxAutoRafa.AutoSize = true;
            this.checkBoxAutoRafa.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.checkBoxAutoRafa.Location = new System.Drawing.Point(225, 136);
            this.checkBoxAutoRafa.Name = "checkBoxAutoRafa";
            this.checkBoxAutoRafa.Size = new System.Drawing.Size(47, 17);
            this.checkBoxAutoRafa.TabIndex = 26;
            this.checkBoxAutoRafa.Text = "auto";
            this.checkBoxAutoRafa.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutoLocator
            // 
            this.checkBoxAutoLocator.AutoSize = true;
            this.checkBoxAutoLocator.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.checkBoxAutoLocator.Location = new System.Drawing.Point(55, 185);
            this.checkBoxAutoLocator.Name = "checkBoxAutoLocator";
            this.checkBoxAutoLocator.Size = new System.Drawing.Size(47, 17);
            this.checkBoxAutoLocator.TabIndex = 27;
            this.checkBoxAutoLocator.Text = "auto";
            this.checkBoxAutoLocator.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutoStatFilter
            // 
            this.checkBoxAutoStatFilter.AutoSize = true;
            this.checkBoxAutoStatFilter.Checked = true;
            this.checkBoxAutoStatFilter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoStatFilter.ForeColor = System.Drawing.Color.DarkGreen;
            this.checkBoxAutoStatFilter.Location = new System.Drawing.Point(83, 237);
            this.checkBoxAutoStatFilter.Name = "checkBoxAutoStatFilter";
            this.checkBoxAutoStatFilter.Size = new System.Drawing.Size(47, 17);
            this.checkBoxAutoStatFilter.TabIndex = 30;
            this.checkBoxAutoStatFilter.Text = "auto";
            this.checkBoxAutoStatFilter.UseVisualStyleBackColor = true;
            // 
            // comboBoxStatFilterMode
            // 
            this.comboBoxStatFilterMode.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxStatFilterMode.ForeColor = System.Drawing.Color.DarkGreen;
            this.comboBoxStatFilterMode.FormattingEnabled = true;
            this.comboBoxStatFilterMode.Location = new System.Drawing.Point(83, 253);
            this.comboBoxStatFilterMode.Name = "comboBoxStatFilterMode";
            this.comboBoxStatFilterMode.Size = new System.Drawing.Size(67, 24);
            this.comboBoxStatFilterMode.TabIndex = 34;
            this.comboBoxStatFilterMode.Text = "RTTY";
            // 
            // comboBoxStatFilterRda
            // 
            this.comboBoxStatFilterRda.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxStatFilterRda.ForeColor = System.Drawing.Color.DarkGreen;
            this.comboBoxStatFilterRda.FormattingEnabled = true;
            this.comboBoxStatFilterRda.Location = new System.Drawing.Point(12, 253);
            this.comboBoxStatFilterRda.Name = "comboBoxStatFilterRda";
            this.comboBoxStatFilterRda.Size = new System.Drawing.Size(67, 24);
            this.comboBoxStatFilterRda.TabIndex = 35;
            this.comboBoxStatFilterRda.Text = "RA-31";
            // 
            // labelStatFilter
            // 
            this.labelStatFilter.AutoSize = true;
            this.labelStatFilter.BackColor = System.Drawing.Color.Transparent;
            this.labelStatFilter.ForeColor = System.Drawing.Color.DarkGreen;
            this.labelStatFilter.Location = new System.Drawing.Point(11, 238);
            this.labelStatFilter.Name = "labelStatFilter";
            this.labelStatFilter.Size = new System.Drawing.Size(66, 13);
            this.labelStatFilter.TabIndex = 29;
            this.labelStatFilter.Text = "Statistic filter";
            // 
            // comboBoxStatFilterBand
            // 
            this.comboBoxStatFilterBand.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxStatFilterBand.ForeColor = System.Drawing.Color.DarkGreen;
            this.comboBoxStatFilterBand.FormattingEnabled = true;
            this.comboBoxStatFilterBand.Location = new System.Drawing.Point(156, 253);
            this.comboBoxStatFilterBand.Name = "comboBoxStatFilterBand";
            this.comboBoxStatFilterBand.Size = new System.Drawing.Size(70, 24);
            this.comboBoxStatFilterBand.TabIndex = 36;
            this.comboBoxStatFilterBand.Text = "14 MHz";
            // 
            // labelStatCallsigns
            // 
            this.labelStatCallsigns.BackColor = System.Drawing.SystemColors.Window;
            this.labelStatCallsigns.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStatCallsigns.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.labelStatCallsigns.ForeColor = System.Drawing.Color.DarkGreen;
            this.labelStatCallsigns.Location = new System.Drawing.Point(296, 254);
            this.labelStatCallsigns.Name = "labelStatCallsigns";
            this.labelStatCallsigns.Size = new System.Drawing.Size(60, 23);
            this.labelStatCallsigns.TabIndex = 61;
            this.labelStatCallsigns.Text = "75";
            this.labelStatCallsigns.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStatQso
            // 
            this.labelStatQso.BackColor = System.Drawing.SystemColors.Window;
            this.labelStatQso.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStatQso.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.labelStatQso.ForeColor = System.Drawing.Color.DarkGreen;
            this.labelStatQso.Location = new System.Drawing.Point(232, 254);
            this.labelStatQso.Name = "labelStatQso";
            this.labelStatQso.Size = new System.Drawing.Size(60, 23);
            this.labelStatQso.TabIndex = 61;
            this.labelStatQso.Text = "125";
            this.labelStatQso.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStatQsoCaption
            // 
            this.labelStatQsoCaption.AutoSize = true;
            this.labelStatQsoCaption.BackColor = System.Drawing.Color.Transparent;
            this.labelStatQsoCaption.ForeColor = System.Drawing.Color.DarkGreen;
            this.labelStatQsoCaption.Location = new System.Drawing.Point(247, 241);
            this.labelStatQsoCaption.Name = "labelStatQsoCaption";
            this.labelStatQsoCaption.Size = new System.Drawing.Size(30, 13);
            this.labelStatQsoCaption.TabIndex = 40;
            this.labelStatQsoCaption.Text = "QSO";
            // 
            // labelStatCallsignsCaption
            // 
            this.labelStatCallsignsCaption.AutoSize = true;
            this.labelStatCallsignsCaption.BackColor = System.Drawing.Color.Transparent;
            this.labelStatCallsignsCaption.ForeColor = System.Drawing.Color.DarkGreen;
            this.labelStatCallsignsCaption.Location = new System.Drawing.Point(309, 240);
            this.labelStatCallsignsCaption.Name = "labelStatCallsignsCaption";
            this.labelStatCallsignsCaption.Size = new System.Drawing.Size(29, 13);
            this.labelStatCallsignsCaption.TabIndex = 41;
            this.labelStatCallsignsCaption.Text = "Calls";
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Location = new System.Drawing.Point(12, 290);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(344, 100);
            this.tableLayoutPanel.TabIndex = 42;
            // 
            // labelHotKeyF1
            // 
            this.labelHotKeyF1.AutoSize = true;
            this.labelHotKeyF1.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelHotKeyF1.Location = new System.Drawing.Point(9, 401);
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
            this.labelHotKeyF2.Location = new System.Drawing.Point(50, 401);
            this.labelHotKeyF2.Name = "labelHotKeyF2";
            this.labelHotKeyF2.Size = new System.Drawing.Size(19, 13);
            this.labelHotKeyF2.TabIndex = 44;
            this.labelHotKeyF2.Text = "F2";
            // 
            // labelHotKeyF3
            // 
            this.labelHotKeyF3.AutoSize = true;
            this.labelHotKeyF3.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelHotKeyF3.Location = new System.Drawing.Point(91, 401);
            this.labelHotKeyF3.Name = "labelHotKeyF3";
            this.labelHotKeyF3.Size = new System.Drawing.Size(19, 13);
            this.labelHotKeyF3.TabIndex = 45;
            this.labelHotKeyF3.Text = "F3";
            // 
            // labelHotKeyF4
            // 
            this.labelHotKeyF4.AutoSize = true;
            this.labelHotKeyF4.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF4.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelHotKeyF4.Location = new System.Drawing.Point(132, 401);
            this.labelHotKeyF4.Name = "labelHotKeyF4";
            this.labelHotKeyF4.Size = new System.Drawing.Size(19, 13);
            this.labelHotKeyF4.TabIndex = 46;
            this.labelHotKeyF4.Text = "F4";
            // 
            // labelHotKeyF5
            // 
            this.labelHotKeyF5.AutoSize = true;
            this.labelHotKeyF5.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF5.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelHotKeyF5.Location = new System.Drawing.Point(173, 401);
            this.labelHotKeyF5.Name = "labelHotKeyF5";
            this.labelHotKeyF5.Size = new System.Drawing.Size(19, 13);
            this.labelHotKeyF5.TabIndex = 47;
            this.labelHotKeyF5.Text = "F5";
            // 
            // labelHotKeyF6
            // 
            this.labelHotKeyF6.AutoSize = true;
            this.labelHotKeyF6.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF6.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelHotKeyF6.Location = new System.Drawing.Point(214, 401);
            this.labelHotKeyF6.Name = "labelHotKeyF6";
            this.labelHotKeyF6.Size = new System.Drawing.Size(19, 13);
            this.labelHotKeyF6.TabIndex = 48;
            this.labelHotKeyF6.Text = "F6";
            // 
            // labelHotKeyF7
            // 
            this.labelHotKeyF7.AutoSize = true;
            this.labelHotKeyF7.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF7.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelHotKeyF7.Location = new System.Drawing.Point(255, 401);
            this.labelHotKeyF7.Name = "labelHotKeyF7";
            this.labelHotKeyF7.Size = new System.Drawing.Size(19, 13);
            this.labelHotKeyF7.TabIndex = 49;
            this.labelHotKeyF7.Text = "F7";
            // 
            // labelHotKeyF8
            // 
            this.labelHotKeyF8.AutoSize = true;
            this.labelHotKeyF8.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF8.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelHotKeyF8.Location = new System.Drawing.Point(296, 401);
            this.labelHotKeyF8.Name = "labelHotKeyF8";
            this.labelHotKeyF8.Size = new System.Drawing.Size(19, 13);
            this.labelHotKeyF8.TabIndex = 50;
            this.labelHotKeyF8.Text = "F8";
            // 
            // labelHotKeyF9
            // 
            this.labelHotKeyF9.AutoSize = true;
            this.labelHotKeyF9.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF9.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelHotKeyF9.Location = new System.Drawing.Point(337, 401);
            this.labelHotKeyF9.Name = "labelHotKeyF9";
            this.labelHotKeyF9.Size = new System.Drawing.Size(19, 13);
            this.labelHotKeyF9.TabIndex = 51;
            this.labelHotKeyF9.Text = "F9";
            // 
            // labelHotKeyF9Bind
            // 
            this.labelHotKeyF9Bind.AutoSize = true;
            this.labelHotKeyF9Bind.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF9Bind.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelHotKeyF9Bind.Location = new System.Drawing.Point(337, 416);
            this.labelHotKeyF9Bind.Name = "labelHotKeyF9Bind";
            this.labelHotKeyF9Bind.Size = new System.Drawing.Size(16, 13);
            this.labelHotKeyF9Bind.TabIndex = 60;
            this.labelHotKeyF9Bind.Text = "...";
            // 
            // labelHotKeyF8Bind
            // 
            this.labelHotKeyF8Bind.AutoSize = true;
            this.labelHotKeyF8Bind.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF8Bind.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelHotKeyF8Bind.Location = new System.Drawing.Point(288, 416);
            this.labelHotKeyF8Bind.Name = "labelHotKeyF8Bind";
            this.labelHotKeyF8Bind.Size = new System.Drawing.Size(35, 13);
            this.labelHotKeyF8Bind.TabIndex = 59;
            this.labelHotKeyF8Bind.Text = "RAFA";
            // 
            // labelHotKeyF7Bind
            // 
            this.labelHotKeyF7Bind.AutoSize = true;
            this.labelHotKeyF7Bind.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF7Bind.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelHotKeyF7Bind.Location = new System.Drawing.Point(249, 416);
            this.labelHotKeyF7Bind.Name = "labelHotKeyF7Bind";
            this.labelHotKeyF7Bind.Size = new System.Drawing.Size(30, 13);
            this.labelHotKeyF7Bind.TabIndex = 58;
            this.labelHotKeyF7Bind.Text = "RDA";
            // 
            // labelHotKeyF6Bind
            // 
            this.labelHotKeyF6Bind.AutoSize = true;
            this.labelHotKeyF6Bind.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF6Bind.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelHotKeyF6Bind.Location = new System.Drawing.Point(214, 416);
            this.labelHotKeyF6Bind.Name = "labelHotKeyF6Bind";
            this.labelHotKeyF6Bind.Size = new System.Drawing.Size(16, 13);
            this.labelHotKeyF6Bind.TabIndex = 57;
            this.labelHotKeyF6Bind.Text = "...";
            // 
            // labelHotKeyF5Bind
            // 
            this.labelHotKeyF5Bind.AutoSize = true;
            this.labelHotKeyF5Bind.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF5Bind.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelHotKeyF5Bind.Location = new System.Drawing.Point(170, 416);
            this.labelHotKeyF5Bind.Name = "labelHotKeyF5Bind";
            this.labelHotKeyF5Bind.Size = new System.Drawing.Size(25, 13);
            this.labelHotKeyF5Bind.TabIndex = 56;
            this.labelHotKeyF5Bind.Text = "HIS";
            // 
            // labelHotKeyF4Bind
            // 
            this.labelHotKeyF4Bind.AutoSize = true;
            this.labelHotKeyF4Bind.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF4Bind.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelHotKeyF4Bind.Location = new System.Drawing.Point(130, 416);
            this.labelHotKeyF4Bind.Name = "labelHotKeyF4Bind";
            this.labelHotKeyF4Bind.Size = new System.Drawing.Size(23, 13);
            this.labelHotKeyF4Bind.TabIndex = 55;
            this.labelHotKeyF4Bind.Text = "MY";
            // 
            // labelHotKeyF3Bind
            // 
            this.labelHotKeyF3Bind.AutoSize = true;
            this.labelHotKeyF3Bind.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF3Bind.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelHotKeyF3Bind.Location = new System.Drawing.Point(89, 416);
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
            this.labelHotKeyF2Bind.Location = new System.Drawing.Point(47, 416);
            this.labelHotKeyF2Bind.Name = "labelHotKeyF2Bind";
            this.labelHotKeyF2Bind.Size = new System.Drawing.Size(25, 13);
            this.labelHotKeyF2Bind.TabIndex = 53;
            this.labelHotKeyF2Bind.Text = "599";
            // 
            // labelHotKeyF1Bind
            // 
            this.labelHotKeyF1Bind.AutoSize = true;
            this.labelHotKeyF1Bind.BackColor = System.Drawing.Color.Transparent;
            this.labelHotKeyF1Bind.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelHotKeyF1Bind.Location = new System.Drawing.Point(9, 416);
            this.labelHotKeyF1Bind.Name = "labelHotKeyF1Bind";
            this.labelHotKeyF1Bind.Size = new System.Drawing.Size(22, 13);
            this.labelHotKeyF1Bind.TabIndex = 52;
            this.labelHotKeyF1Bind.Text = "CQ";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 461);
            this.Controls.Add(this.labelHotKeyF9Bind);
            this.Controls.Add(this.labelHotKeyF8Bind);
            this.Controls.Add(this.labelHotKeyF7Bind);
            this.Controls.Add(this.labelHotKeyF6Bind);
            this.Controls.Add(this.labelHotKeyF5Bind);
            this.Controls.Add(this.labelHotKeyF4Bind);
            this.Controls.Add(this.labelHotKeyF3Bind);
            this.Controls.Add(this.labelHotKeyF2Bind);
            this.Controls.Add(this.labelHotKeyF1Bind);
            this.Controls.Add(this.labelHotKeyF9);
            this.Controls.Add(this.labelHotKeyF8);
            this.Controls.Add(this.labelHotKeyF7);
            this.Controls.Add(this.labelHotKeyF6);
            this.Controls.Add(this.labelHotKeyF5);
            this.Controls.Add(this.labelHotKeyF4);
            this.Controls.Add(this.labelHotKeyF3);
            this.Controls.Add(this.labelHotKeyF2);
            this.Controls.Add(this.labelHotKeyF1);
            this.Controls.Add(this.tableLayoutPanel);
            this.Controls.Add(this.labelStatCallsignsCaption);
            this.Controls.Add(this.labelStatQsoCaption);
            this.Controls.Add(this.labelStatQso);
            this.Controls.Add(this.labelStatCallsigns);
            this.Controls.Add(this.comboBoxStatFilterBand);
            this.Controls.Add(this.comboBoxStatFilterRda);
            this.Controls.Add(this.comboBoxStatFilterMode);
            this.Controls.Add(this.checkBoxAutoStatFilter);
            this.Controls.Add(this.labelStatFilter);
            this.Controls.Add(this.checkBoxAutoLocator);
            this.Controls.Add(this.checkBoxAutoRafa);
            this.Controls.Add(this.checkBoxAutoRda);
            this.Controls.Add(this.labelUserField);
            this.Controls.Add(this.textBoxUserField);
            this.Controls.Add(this.labelLocator);
            this.Controls.Add(this.textBoxLocator);
            this.Controls.Add(this.labelRafa);
            this.Controls.Add(this.textBoxRafa);
            this.Controls.Add(this.labelRda);
            this.Controls.Add(this.textBoxRda);
            this.Controls.Add(this.labelMode);
            this.Controls.Add(this.comboBoxMode);
            this.Controls.Add(this.labelFreq);
            this.Controls.Add(this.textBoxFreq);
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
            this.Controls.Add(this.statusStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormMain";
            this.Text = "TNXQSO log - R7AB_08dec2019";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripLabel toolStripLabelFile;
        private System.Windows.Forms.ToolStripLabel toolStripLabelSettings;
        private System.Windows.Forms.ToolStripLabel toolStripLabelLog;
        private System.Windows.Forms.Label labelDateTime;
        private System.Windows.Forms.Button buttonPostFreq;
        private System.Windows.Forms.TextBox textBoxCorrespondent;
        private System.Windows.Forms.TextBox textBoxRstSent;
        private System.Windows.Forms.Label labelRstSent;
        private System.Windows.Forms.Label labelRstRcvd;
        private System.Windows.Forms.TextBox textBoxRstRcvd;
        private System.Windows.Forms.Label labelCallsign;
        private System.Windows.Forms.TextBox textBoxCallsign;
        private System.Windows.Forms.Label labelFreq;
        private System.Windows.Forms.TextBox textBoxFreq;
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
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
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
    }
}

