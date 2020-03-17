using System;
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
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.labelDupe = new System.Windows.Forms.Label();
            this.checkBoxTop = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panelQsoComments = new System.Windows.Forms.Panel();
            this.labelComments = new System.Windows.Forms.Label();
            this.textBoxComments = new System.Windows.Forms.TextBoxCallsign();
            this.panelQth1_2 = new System.Windows.Forms.Panel();
            this.checkBoxAutoQth2 = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoQth1 = new System.Windows.Forms.CheckBox();
            this.labelQth2 = new System.Windows.Forms.Label();
            this.textBoxQth2 = new System.Windows.Forms.TextBox();
            this.labelQth1 = new System.Windows.Forms.Label();
            this.textBoxQth1 = new System.Windows.Forms.TextBox();
            this.panelQth3Loc = new System.Windows.Forms.Panel();
            this.checkBoxAutoQth3 = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoLocator = new System.Windows.Forms.CheckBox();
            this.textBoxLocator = new System.Windows.Forms.TextBox();
            this.labelLocator = new System.Windows.Forms.Label();
            this.textBoxQth3 = new System.Windows.Forms.TextBox();
            this.labelQth3 = new System.Windows.Forms.Label();
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
            this.labelMorseSpeed = new System.Windows.Forms.Label();
            this.labelCwMacroF5 = new System.Windows.Forms.Label();
            this.labelCwMacroF1 = new System.Windows.Forms.Label();
            this.labelCwMacroF2 = new System.Windows.Forms.Label();
            this.numericUpDownMorseSpeed = new System.Windows.Forms.NumericUpDown();
            this.labelCwMacroF3 = new System.Windows.Forms.Label();
            this.labelCwMacroF9Title = new System.Windows.Forms.Label();
            this.labelCwMacroF4 = new System.Windows.Forms.Label();
            this.labelCwMacroF8Title = new System.Windows.Forms.Label();
            this.labelCwMacroF6 = new System.Windows.Forms.Label();
            this.labelCwMacroF7Title = new System.Windows.Forms.Label();
            this.labelCwMacroF7 = new System.Windows.Forms.Label();
            this.labelCwMacroF6Title = new System.Windows.Forms.Label();
            this.labelCwMacroF8 = new System.Windows.Forms.Label();
            this.labelCwMacroF5Title = new System.Windows.Forms.Label();
            this.labelCwMacroF9 = new System.Windows.Forms.Label();
            this.labelCwMacroF4Title = new System.Windows.Forms.Label();
            this.labelCwMacroF1Title = new System.Windows.Forms.Label();
            this.labelCwMacroF3Title = new System.Windows.Forms.Label();
            this.labelCwMacroF2Title = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.loginLabel = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.menuItemAdifExportLoc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAdifExportAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripLabelSettings = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabelLog = new System.Windows.Forms.ToolStripLabel();
            this.labelEsm = new System.Windows.Forms.Label();
            this.flowLayoutPanel.SuspendLayout();
            this.panelQsoComments.SuspendLayout();
            this.panelQth1_2.SuspendLayout();
            this.panelQth3Loc.SuspendLayout();
            this.panelStatFilter.SuspendLayout();
            this.panelCallsignId.SuspendLayout();
            this.panelCwMacro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMorseSpeed)).BeginInit();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFreq)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
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
            this.flowLayoutPanel.Controls.Add(this.panelQsoComments);
            this.flowLayoutPanel.Controls.Add(this.panelQth1_2);
            this.flowLayoutPanel.Controls.Add(this.panelQth3Loc);
            this.flowLayoutPanel.Controls.Add(this.panelStatFilter);
            this.flowLayoutPanel.Controls.Add(this.panelCallsignId);
            this.flowLayoutPanel.Controls.Add(this.panelCwMacro);
            this.flowLayoutPanel.Controls.Add(this.statusStrip);
            this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel.Location = new System.Drawing.Point(0, 123);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(364, 351);
            this.flowLayoutPanel.TabIndex = 63;
            // 
            // panelQsoComments
            // 
            this.panelQsoComments.Controls.Add(this.labelComments);
            this.panelQsoComments.Controls.Add(this.textBoxComments);
            this.panelQsoComments.Location = new System.Drawing.Point(0, 0);
            this.panelQsoComments.Margin = new System.Windows.Forms.Padding(0);
            this.panelQsoComments.Name = "panelQsoComments";
            this.panelQsoComments.Size = new System.Drawing.Size(364, 50);
            this.panelQsoComments.TabIndex = 29;
            // 
            // labelComments
            // 
            this.labelComments.AutoSize = true;
            this.labelComments.BackColor = System.Drawing.Color.Transparent;
            this.labelComments.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelComments.Location = new System.Drawing.Point(12, 5);
            this.labelComments.Name = "labelComments";
            this.labelComments.Size = new System.Drawing.Size(56, 13);
            this.labelComments.TabIndex = 67;
            this.labelComments.Text = "Comments";
            // 
            // textBoxComments
            // 
            this.textBoxComments.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxComments.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxComments.Location = new System.Drawing.Point(13, 21);
            this.textBoxComments.Name = "textBoxComments";
            this.textBoxComments.Size = new System.Drawing.Size(344, 22);
            this.textBoxComments.TabIndex = 6;
            this.textBoxComments.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panelQth1_2
            // 
            this.panelQth1_2.Controls.Add(this.checkBoxAutoQth2);
            this.panelQth1_2.Controls.Add(this.checkBoxAutoQth1);
            this.panelQth1_2.Controls.Add(this.labelQth2);
            this.panelQth1_2.Controls.Add(this.textBoxQth2);
            this.panelQth1_2.Controls.Add(this.labelQth1);
            this.panelQth1_2.Controls.Add(this.textBoxQth1);
            this.panelQth1_2.Location = new System.Drawing.Point(0, 50);
            this.panelQth1_2.Margin = new System.Windows.Forms.Padding(0);
            this.panelQth1_2.Name = "panelQth1_2";
            this.panelQth1_2.Size = new System.Drawing.Size(364, 48);
            this.panelQth1_2.TabIndex = 63;
            // 
            // checkBoxAutoQth2
            // 
            this.checkBoxAutoQth2.AutoSize = true;
            this.checkBoxAutoQth2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.checkBoxAutoQth2.Location = new System.Drawing.Point(310, 4);
            this.checkBoxAutoQth2.Name = "checkBoxAutoQth2";
            this.checkBoxAutoQth2.Size = new System.Drawing.Size(47, 17);
            this.checkBoxAutoQth2.TabIndex = 26;
            this.checkBoxAutoQth2.TabStop = false;
            this.checkBoxAutoQth2.Text = "auto";
            this.checkBoxAutoQth2.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutoQth1
            // 
            this.checkBoxAutoQth1.AutoSize = true;
            this.checkBoxAutoQth1.Checked = true;
            this.checkBoxAutoQth1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoQth1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.checkBoxAutoQth1.Location = new System.Drawing.Point(136, 4);
            this.checkBoxAutoQth1.Name = "checkBoxAutoQth1";
            this.checkBoxAutoQth1.Size = new System.Drawing.Size(47, 17);
            this.checkBoxAutoQth1.TabIndex = 25;
            this.checkBoxAutoQth1.TabStop = false;
            this.checkBoxAutoQth1.Text = "auto";
            this.checkBoxAutoQth1.UseVisualStyleBackColor = true;
            // 
            // labelQth2
            // 
            this.labelQth2.AutoSize = true;
            this.labelQth2.BackColor = System.Drawing.Color.Transparent;
            this.labelQth2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelQth2.Location = new System.Drawing.Point(188, 5);
            this.labelQth2.Name = "labelQth2";
            this.labelQth2.Size = new System.Drawing.Size(35, 13);
            this.labelQth2.TabIndex = 20;
            this.labelQth2.Text = "RAFA";
            // 
            // textBoxQth2
            // 
            this.textBoxQth2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.textBoxQth2.ForeColor = System.Drawing.Color.Navy;
            this.textBoxQth2.Location = new System.Drawing.Point(189, 21);
            this.textBoxQth2.Name = "textBoxQth2";
            this.textBoxQth2.Size = new System.Drawing.Size(168, 23);
            this.textBoxQth2.TabIndex = 7;
            this.textBoxQth2.Text = "H4RT";
            this.textBoxQth2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelQth1
            // 
            this.labelQth1.AutoSize = true;
            this.labelQth1.BackColor = System.Drawing.Color.Transparent;
            this.labelQth1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelQth1.Location = new System.Drawing.Point(11, 5);
            this.labelQth1.Name = "labelQth1";
            this.labelQth1.Size = new System.Drawing.Size(30, 13);
            this.labelQth1.TabIndex = 18;
            this.labelQth1.Text = "RDA";
            // 
            // textBoxQth1
            // 
            this.textBoxQth1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.textBoxQth1.ForeColor = System.Drawing.Color.Navy;
            this.textBoxQth1.Location = new System.Drawing.Point(12, 21);
            this.textBoxQth1.Name = "textBoxQth1";
            this.textBoxQth1.Size = new System.Drawing.Size(171, 23);
            this.textBoxQth1.TabIndex = 7;
            this.textBoxQth1.Text = "RA-25 RA-37 RA-27";
            this.textBoxQth1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panelQth3Loc
            // 
            this.panelQth3Loc.Controls.Add(this.checkBoxAutoQth3);
            this.panelQth3Loc.Controls.Add(this.checkBoxAutoLocator);
            this.panelQth3Loc.Controls.Add(this.textBoxLocator);
            this.panelQth3Loc.Controls.Add(this.labelLocator);
            this.panelQth3Loc.Controls.Add(this.textBoxQth3);
            this.panelQth3Loc.Controls.Add(this.labelQth3);
            this.panelQth3Loc.Location = new System.Drawing.Point(0, 98);
            this.panelQth3Loc.Margin = new System.Windows.Forms.Padding(0);
            this.panelQth3Loc.Name = "panelQth3Loc";
            this.panelQth3Loc.Size = new System.Drawing.Size(364, 50);
            this.panelQth3Loc.TabIndex = 28;
            // 
            // checkBoxAutoQth3
            // 
            this.checkBoxAutoQth3.AutoSize = true;
            this.checkBoxAutoQth3.Checked = true;
            this.checkBoxAutoQth3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoQth3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.checkBoxAutoQth3.Location = new System.Drawing.Point(136, 6);
            this.checkBoxAutoQth3.Name = "checkBoxAutoQth3";
            this.checkBoxAutoQth3.Size = new System.Drawing.Size(47, 17);
            this.checkBoxAutoQth3.TabIndex = 27;
            this.checkBoxAutoQth3.TabStop = false;
            this.checkBoxAutoQth3.Text = "auto";
            this.checkBoxAutoQth3.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutoLocator
            // 
            this.checkBoxAutoLocator.AutoSize = true;
            this.checkBoxAutoLocator.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.checkBoxAutoLocator.Location = new System.Drawing.Point(309, 6);
            this.checkBoxAutoLocator.Name = "checkBoxAutoLocator";
            this.checkBoxAutoLocator.Size = new System.Drawing.Size(47, 17);
            this.checkBoxAutoLocator.TabIndex = 27;
            this.checkBoxAutoLocator.TabStop = false;
            this.checkBoxAutoLocator.Text = "auto";
            this.checkBoxAutoLocator.UseVisualStyleBackColor = true;
            this.checkBoxAutoLocator.CheckedChanged += new System.EventHandler(this.CheckBoxAutoLocator_CheckedChanged);
            // 
            // textBoxLocator
            // 
            this.textBoxLocator.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxLocator.ForeColor = System.Drawing.Color.Navy;
            this.textBoxLocator.Location = new System.Drawing.Point(186, 23);
            this.textBoxLocator.Name = "textBoxLocator";
            this.textBoxLocator.Size = new System.Drawing.Size(171, 22);
            this.textBoxLocator.TabIndex = 8;
            this.textBoxLocator.Text = "KN96on";
            this.textBoxLocator.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxLocator.TextChanged += new System.EventHandler(this.TextBoxLocator_TextChanged);
            this.textBoxLocator.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxLocator_Validating);
            this.textBoxLocator.Validated += new System.EventHandler(this.TextBoxLocator_Validated);
            // 
            // labelLocator
            // 
            this.labelLocator.AutoSize = true;
            this.labelLocator.BackColor = System.Drawing.Color.Transparent;
            this.labelLocator.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelLocator.Location = new System.Drawing.Point(188, 7);
            this.labelLocator.Name = "labelLocator";
            this.labelLocator.Size = new System.Drawing.Size(43, 13);
            this.labelLocator.TabIndex = 22;
            this.labelLocator.Text = "Locator";
            // 
            // textBoxQth3
            // 
            this.textBoxQth3.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.textBoxQth3.ForeColor = System.Drawing.Color.Navy;
            this.textBoxQth3.Location = new System.Drawing.Point(15, 23);
            this.textBoxQth3.Name = "textBoxQth3";
            this.textBoxQth3.Size = new System.Drawing.Size(168, 23);
            this.textBoxQth3.TabIndex = 9;
            this.textBoxQth3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelQth3
            // 
            this.labelQth3.AutoSize = true;
            this.labelQth3.BackColor = System.Drawing.Color.Transparent;
            this.labelQth3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelQth3.Location = new System.Drawing.Point(12, 7);
            this.labelQth3.Name = "labelQth3";
            this.labelQth3.Size = new System.Drawing.Size(51, 13);
            this.labelQth3.TabIndex = 24;
            this.labelQth3.Text = "User field";
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
            this.panelStatFilter.Location = new System.Drawing.Point(0, 148);
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
            this.checkBoxAutoStatFilter.CheckedChanged += new System.EventHandler(this.CheckBoxAutoStatFilter_CheckedChanged);
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
            this.panelCallsignId.Location = new System.Drawing.Point(0, 198);
            this.panelCallsignId.Margin = new System.Windows.Forms.Padding(0);
            this.panelCallsignId.Name = "panelCallsignId";
            this.panelCallsignId.Size = new System.Drawing.Size(363, 91);
            this.panelCallsignId.TabIndex = 63;
            // 
            // listBoxCallsignsDb
            // 
            this.listBoxCallsignsDb.ColumnWidth = 85;
            this.listBoxCallsignsDb.Cursor = System.Windows.Forms.Cursors.Default;
            this.listBoxCallsignsDb.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxCallsignsDb.FormattingEnabled = true;
            this.listBoxCallsignsDb.ItemHeight = 16;
            this.listBoxCallsignsDb.Location = new System.Drawing.Point(184, 7);
            this.listBoxCallsignsDb.Name = "listBoxCallsignsDb";
            this.listBoxCallsignsDb.Size = new System.Drawing.Size(170, 84);
            this.listBoxCallsignsDb.TabIndex = 1;
            this.listBoxCallsignsDb.SelectedIndexChanged += new System.EventHandler(this.ListBoxCallsigns_SelectedIndexChanged);
            // 
            // listBoxCallsignsQso
            // 
            this.listBoxCallsignsQso.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxCallsignsQso.FormattingEnabled = true;
            this.listBoxCallsignsQso.ItemHeight = 16;
            this.listBoxCallsignsQso.Location = new System.Drawing.Point(9, 7);
            this.listBoxCallsignsQso.Name = "listBoxCallsignsQso";
            this.listBoxCallsignsQso.Size = new System.Drawing.Size(174, 84);
            this.listBoxCallsignsQso.TabIndex = 0;
            this.listBoxCallsignsQso.SelectedIndexChanged += new System.EventHandler(this.ListBoxCallsigns_SelectedIndexChanged);
            // 
            // panelCwMacro
            // 
            this.panelCwMacro.Controls.Add(this.labelMorseSpeed);
            this.panelCwMacro.Controls.Add(this.labelCwMacroF5);
            this.panelCwMacro.Controls.Add(this.labelCwMacroF1);
            this.panelCwMacro.Controls.Add(this.labelCwMacroF2);
            this.panelCwMacro.Controls.Add(this.numericUpDownMorseSpeed);
            this.panelCwMacro.Controls.Add(this.labelCwMacroF3);
            this.panelCwMacro.Controls.Add(this.labelCwMacroF9Title);
            this.panelCwMacro.Controls.Add(this.labelCwMacroF4);
            this.panelCwMacro.Controls.Add(this.labelCwMacroF8Title);
            this.panelCwMacro.Controls.Add(this.labelCwMacroF6);
            this.panelCwMacro.Controls.Add(this.labelCwMacroF7Title);
            this.panelCwMacro.Controls.Add(this.labelCwMacroF7);
            this.panelCwMacro.Controls.Add(this.labelCwMacroF6Title);
            this.panelCwMacro.Controls.Add(this.labelCwMacroF8);
            this.panelCwMacro.Controls.Add(this.labelCwMacroF5Title);
            this.panelCwMacro.Controls.Add(this.labelCwMacroF9);
            this.panelCwMacro.Controls.Add(this.labelCwMacroF4Title);
            this.panelCwMacro.Controls.Add(this.labelCwMacroF1Title);
            this.panelCwMacro.Controls.Add(this.labelCwMacroF3Title);
            this.panelCwMacro.Controls.Add(this.labelCwMacroF2Title);
            this.panelCwMacro.Location = new System.Drawing.Point(0, 289);
            this.panelCwMacro.Margin = new System.Windows.Forms.Padding(0);
            this.panelCwMacro.Name = "panelCwMacro";
            this.panelCwMacro.Size = new System.Drawing.Size(364, 40);
            this.panelCwMacro.TabIndex = 62;
            // 
            // labelMorseSpeed
            // 
            this.labelMorseSpeed.AutoSize = true;
            this.labelMorseSpeed.BackColor = System.Drawing.Color.Transparent;
            this.labelMorseSpeed.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelMorseSpeed.Location = new System.Drawing.Point(322, 4);
            this.labelMorseSpeed.Name = "labelMorseSpeed";
            this.labelMorseSpeed.Size = new System.Drawing.Size(34, 13);
            this.labelMorseSpeed.TabIndex = 63;
            this.labelMorseSpeed.Text = "WPM";
            // 
            // labelCwMacroF5
            // 
            this.labelCwMacroF5.AutoSize = true;
            this.labelCwMacroF5.BackColor = System.Drawing.Color.Transparent;
            this.labelCwMacroF5.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelCwMacroF5.Location = new System.Drawing.Point(141, 4);
            this.labelCwMacroF5.Name = "labelCwMacroF5";
            this.labelCwMacroF5.Size = new System.Drawing.Size(19, 13);
            this.labelCwMacroF5.TabIndex = 47;
            this.labelCwMacroF5.Text = "F5";
            // 
            // labelCwMacroF1
            // 
            this.labelCwMacroF1.AutoSize = true;
            this.labelCwMacroF1.BackColor = System.Drawing.Color.Transparent;
            this.labelCwMacroF1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelCwMacroF1.Location = new System.Drawing.Point(6, 4);
            this.labelCwMacroF1.Name = "labelCwMacroF1";
            this.labelCwMacroF1.Size = new System.Drawing.Size(19, 13);
            this.labelCwMacroF1.TabIndex = 43;
            this.labelCwMacroF1.Text = "F1";
            // 
            // labelCwMacroF2
            // 
            this.labelCwMacroF2.AutoSize = true;
            this.labelCwMacroF2.BackColor = System.Drawing.Color.Transparent;
            this.labelCwMacroF2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelCwMacroF2.Location = new System.Drawing.Point(38, 4);
            this.labelCwMacroF2.Name = "labelCwMacroF2";
            this.labelCwMacroF2.Size = new System.Drawing.Size(19, 13);
            this.labelCwMacroF2.TabIndex = 44;
            this.labelCwMacroF2.Text = "F2";
            // 
            // numericUpDownMorseSpeed
            // 
            this.numericUpDownMorseSpeed.Location = new System.Drawing.Point(317, 18);
            this.numericUpDownMorseSpeed.Name = "numericUpDownMorseSpeed";
            this.numericUpDownMorseSpeed.Size = new System.Drawing.Size(43, 20);
            this.numericUpDownMorseSpeed.TabIndex = 62;
            this.numericUpDownMorseSpeed.TabStop = false;
            this.numericUpDownMorseSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownMorseSpeed.ValueChanged += new System.EventHandler(this.NumericUpDownMorseSpeed_ValueChanged);
            // 
            // labelCwMacroF3
            // 
            this.labelCwMacroF3.AutoSize = true;
            this.labelCwMacroF3.BackColor = System.Drawing.Color.Transparent;
            this.labelCwMacroF3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelCwMacroF3.Location = new System.Drawing.Point(73, 4);
            this.labelCwMacroF3.Name = "labelCwMacroF3";
            this.labelCwMacroF3.Size = new System.Drawing.Size(19, 13);
            this.labelCwMacroF3.TabIndex = 45;
            this.labelCwMacroF3.Text = "F3";
            // 
            // labelCwMacroF9Title
            // 
            this.labelCwMacroF9Title.BackColor = System.Drawing.Color.Transparent;
            this.labelCwMacroF9Title.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelCwMacroF9Title.Location = new System.Drawing.Point(273, 20);
            this.labelCwMacroF9Title.Name = "labelCwMacroF9Title";
            this.labelCwMacroF9Title.Size = new System.Drawing.Size(30, 16);
            this.labelCwMacroF9Title.TabIndex = 60;
            this.labelCwMacroF9Title.Text = "...";
            this.labelCwMacroF9Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCwMacroF4
            // 
            this.labelCwMacroF4.AutoSize = true;
            this.labelCwMacroF4.BackColor = System.Drawing.Color.Transparent;
            this.labelCwMacroF4.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelCwMacroF4.Location = new System.Drawing.Point(107, 4);
            this.labelCwMacroF4.Name = "labelCwMacroF4";
            this.labelCwMacroF4.Size = new System.Drawing.Size(19, 13);
            this.labelCwMacroF4.TabIndex = 46;
            this.labelCwMacroF4.Text = "F4";
            // 
            // labelCwMacroF8Title
            // 
            this.labelCwMacroF8Title.BackColor = System.Drawing.Color.Transparent;
            this.labelCwMacroF8Title.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelCwMacroF8Title.Location = new System.Drawing.Point(239, 19);
            this.labelCwMacroF8Title.Name = "labelCwMacroF8Title";
            this.labelCwMacroF8Title.Size = new System.Drawing.Size(30, 16);
            this.labelCwMacroF8Title.TabIndex = 59;
            this.labelCwMacroF8Title.Text = "RAFA";
            this.labelCwMacroF8Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCwMacroF6
            // 
            this.labelCwMacroF6.AutoSize = true;
            this.labelCwMacroF6.BackColor = System.Drawing.Color.Transparent;
            this.labelCwMacroF6.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelCwMacroF6.Location = new System.Drawing.Point(177, 4);
            this.labelCwMacroF6.Name = "labelCwMacroF6";
            this.labelCwMacroF6.Size = new System.Drawing.Size(19, 13);
            this.labelCwMacroF6.TabIndex = 48;
            this.labelCwMacroF6.Text = "F6";
            // 
            // labelCwMacroF7Title
            // 
            this.labelCwMacroF7Title.BackColor = System.Drawing.Color.Transparent;
            this.labelCwMacroF7Title.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelCwMacroF7Title.Location = new System.Drawing.Point(205, 19);
            this.labelCwMacroF7Title.Name = "labelCwMacroF7Title";
            this.labelCwMacroF7Title.Size = new System.Drawing.Size(30, 16);
            this.labelCwMacroF7Title.TabIndex = 58;
            this.labelCwMacroF7Title.Text = "RDA";
            this.labelCwMacroF7Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCwMacroF7
            // 
            this.labelCwMacroF7.AutoSize = true;
            this.labelCwMacroF7.BackColor = System.Drawing.Color.Transparent;
            this.labelCwMacroF7.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelCwMacroF7.Location = new System.Drawing.Point(212, 4);
            this.labelCwMacroF7.Name = "labelCwMacroF7";
            this.labelCwMacroF7.Size = new System.Drawing.Size(19, 13);
            this.labelCwMacroF7.TabIndex = 49;
            this.labelCwMacroF7.Text = "F7";
            // 
            // labelCwMacroF6Title
            // 
            this.labelCwMacroF6Title.BackColor = System.Drawing.Color.Transparent;
            this.labelCwMacroF6Title.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelCwMacroF6Title.Location = new System.Drawing.Point(170, 19);
            this.labelCwMacroF6Title.Name = "labelCwMacroF6Title";
            this.labelCwMacroF6Title.Size = new System.Drawing.Size(30, 16);
            this.labelCwMacroF6Title.TabIndex = 57;
            this.labelCwMacroF6Title.Text = "...";
            this.labelCwMacroF6Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCwMacroF8
            // 
            this.labelCwMacroF8.AutoSize = true;
            this.labelCwMacroF8.BackColor = System.Drawing.Color.Transparent;
            this.labelCwMacroF8.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelCwMacroF8.Location = new System.Drawing.Point(246, 4);
            this.labelCwMacroF8.Name = "labelCwMacroF8";
            this.labelCwMacroF8.Size = new System.Drawing.Size(19, 13);
            this.labelCwMacroF8.TabIndex = 50;
            this.labelCwMacroF8.Text = "F8";
            // 
            // labelCwMacroF5Title
            // 
            this.labelCwMacroF5Title.BackColor = System.Drawing.Color.Transparent;
            this.labelCwMacroF5Title.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelCwMacroF5Title.Location = new System.Drawing.Point(135, 19);
            this.labelCwMacroF5Title.Name = "labelCwMacroF5Title";
            this.labelCwMacroF5Title.Size = new System.Drawing.Size(30, 16);
            this.labelCwMacroF5Title.TabIndex = 56;
            this.labelCwMacroF5Title.Text = "HIS";
            this.labelCwMacroF5Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCwMacroF9
            // 
            this.labelCwMacroF9.AutoSize = true;
            this.labelCwMacroF9.BackColor = System.Drawing.Color.Transparent;
            this.labelCwMacroF9.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelCwMacroF9.Location = new System.Drawing.Point(282, 4);
            this.labelCwMacroF9.Name = "labelCwMacroF9";
            this.labelCwMacroF9.Size = new System.Drawing.Size(19, 13);
            this.labelCwMacroF9.TabIndex = 51;
            this.labelCwMacroF9.Text = "F9";
            // 
            // labelCwMacroF4Title
            // 
            this.labelCwMacroF4Title.BackColor = System.Drawing.Color.Transparent;
            this.labelCwMacroF4Title.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelCwMacroF4Title.Location = new System.Drawing.Point(101, 19);
            this.labelCwMacroF4Title.Name = "labelCwMacroF4Title";
            this.labelCwMacroF4Title.Size = new System.Drawing.Size(30, 16);
            this.labelCwMacroF4Title.TabIndex = 55;
            this.labelCwMacroF4Title.Text = "MY";
            this.labelCwMacroF4Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCwMacroF1Title
            // 
            this.labelCwMacroF1Title.BackColor = System.Drawing.Color.Transparent;
            this.labelCwMacroF1Title.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelCwMacroF1Title.Location = new System.Drawing.Point(-1, 19);
            this.labelCwMacroF1Title.Name = "labelCwMacroF1Title";
            this.labelCwMacroF1Title.Size = new System.Drawing.Size(30, 16);
            this.labelCwMacroF1Title.TabIndex = 52;
            this.labelCwMacroF1Title.Text = "CQ";
            this.labelCwMacroF1Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCwMacroF3Title
            // 
            this.labelCwMacroF3Title.BackColor = System.Drawing.Color.Transparent;
            this.labelCwMacroF3Title.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelCwMacroF3Title.Location = new System.Drawing.Point(66, 19);
            this.labelCwMacroF3Title.Name = "labelCwMacroF3Title";
            this.labelCwMacroF3Title.Size = new System.Drawing.Size(30, 16);
            this.labelCwMacroF3Title.TabIndex = 54;
            this.labelCwMacroF3Title.Text = "TU";
            this.labelCwMacroF3Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCwMacroF2Title
            // 
            this.labelCwMacroF2Title.BackColor = System.Drawing.Color.Transparent;
            this.labelCwMacroF2Title.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelCwMacroF2Title.Location = new System.Drawing.Point(32, 19);
            this.labelCwMacroF2Title.Name = "labelCwMacroF2Title";
            this.labelCwMacroF2Title.Size = new System.Drawing.Size(30, 16);
            this.labelCwMacroF2Title.TabIndex = 53;
            this.labelCwMacroF2Title.Text = "599";
            this.labelCwMacroF2Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusStrip
            // 
            this.statusStrip.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.statusStrip.AutoSize = false;
            this.statusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip.GripMargin = new System.Windows.Forms.Padding(0);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginLabel,
            this.connectionStatusLabel});
            this.statusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip.Location = new System.Drawing.Point(0, 329);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(364, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 0;
            // 
            // loginLabel
            // 
            this.loginLabel.Name = "loginLabel";
            this.loginLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // connectionStatusLabel
            // 
            this.connectionStatusLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.connectionStatusLabel.BackColor = System.Drawing.Color.Red;
            this.connectionStatusLabel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.connectionStatusLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.connectionStatusLabel.ForeColor = System.Drawing.Color.White;
            this.connectionStatusLabel.Name = "connectionStatusLabel";
            this.connectionStatusLabel.Size = new System.Drawing.Size(79, 17);
            this.connectionStatusLabel.Text = " TNXQSO.com ";
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
            29000000,
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
            this.numericUpDownFreq.TextChanged += new System.EventHandler(this.NumericUpDownFreq_TextChanged);
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
            this.buttonPostFreq.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonPostFreq.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonPostFreq.Location = new System.Drawing.Point(330, 93);
            this.buttonPostFreq.Name = "buttonPostFreq";
            this.buttonPostFreq.Size = new System.Drawing.Size(24, 24);
            this.buttonPostFreq.TabIndex = 5;
            this.buttonPostFreq.TabStop = false;
            this.buttonPostFreq.Text = "➤";
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
            this.menuItemAdifExportLoc,
            this.menuItemAdifExportAll});
            this.menuItemAdifExport.Name = "menuItemAdifExport";
            this.menuItemAdifExport.Size = new System.Drawing.Size(107, 22);
            this.menuItemAdifExport.Text = "Export";
            // 
            // menuItemAdifExportRda
            // 
            this.menuItemAdifExportRda.Name = "menuItemAdifExportRda";
            this.menuItemAdifExportRda.Size = new System.Drawing.Size(127, 22);
            this.menuItemAdifExportRda.Text = "By RDA";
            this.menuItemAdifExportRda.Click += new System.EventHandler(this.MenuItemAdifExportRda_Click);
            // 
            // menuItemAdifExportRafa
            // 
            this.menuItemAdifExportRafa.Name = "menuItemAdifExportRafa";
            this.menuItemAdifExportRafa.Size = new System.Drawing.Size(127, 22);
            this.menuItemAdifExportRafa.Text = "By RAFA";
            this.menuItemAdifExportRafa.Click += new System.EventHandler(this.MenuItemAdifExportRafa_Click);
            // 
            // menuItemAdifExportLoc
            // 
            this.menuItemAdifExportLoc.Name = "menuItemAdifExportLoc";
            this.menuItemAdifExportLoc.Size = new System.Drawing.Size(127, 22);
            this.menuItemAdifExportLoc.Text = "By locator";
            this.menuItemAdifExportLoc.Click += new System.EventHandler(this.MenuItemAdifExportLoc_Click);
            // 
            // menuItemAdifExportAll
            // 
            this.menuItemAdifExportAll.Name = "menuItemAdifExportAll";
            this.menuItemAdifExportAll.Size = new System.Drawing.Size(127, 22);
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
            // labelEsm
            // 
            this.labelEsm.BackColor = System.Drawing.Color.Transparent;
            this.labelEsm.ForeColor = System.Drawing.Color.DarkGreen;
            this.labelEsm.Location = new System.Drawing.Point(11, 30);
            this.labelEsm.Name = "labelEsm";
            this.labelEsm.Size = new System.Drawing.Size(45, 13);
            this.labelEsm.TabIndex = 66;
            this.labelEsm.Text = "ESM";
            this.labelEsm.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.labelEsm.Visible = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(364, 474);
            this.Controls.Add(this.labelEsm);
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
            this.Activated += new System.EventHandler(this.FormMain_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.flowLayoutPanel.ResumeLayout(false);
            this.panelQsoComments.ResumeLayout(false);
            this.panelQsoComments.PerformLayout();
            this.panelQth1_2.ResumeLayout(false);
            this.panelQth1_2.PerformLayout();
            this.panelQth3Loc.ResumeLayout(false);
            this.panelQth3Loc.PerformLayout();
            this.panelStatFilter.ResumeLayout(false);
            this.panelStatFilter.PerformLayout();
            this.panelCallsignId.ResumeLayout(false);
            this.panelCwMacro.ResumeLayout(false);
            this.panelCwMacro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMorseSpeed)).EndInit();
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
        private System.Windows.Forms.Label labelQth1;
        private System.Windows.Forms.TextBox textBoxQth1;
        private System.Windows.Forms.Label labelQth2;
        private System.Windows.Forms.TextBox textBoxQth2;
        private System.Windows.Forms.Label labelQth3;
        private System.Windows.Forms.TextBox textBoxQth3;
        private System.Windows.Forms.Label labelLocator;
        private System.Windows.Forms.TextBox textBoxLocator;
        private System.Windows.Forms.CheckBox checkBoxAutoQth1;
        private System.Windows.Forms.CheckBox checkBoxAutoQth2;
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
        private System.Windows.Forms.Label labelCwMacroF1;
        private System.Windows.Forms.Label labelCwMacroF2;
        private System.Windows.Forms.Label labelCwMacroF3;
        private System.Windows.Forms.Label labelCwMacroF4;
        private System.Windows.Forms.Label labelCwMacroF5;
        private System.Windows.Forms.Label labelCwMacroF6;
        private System.Windows.Forms.Label labelCwMacroF7;
        private System.Windows.Forms.Label labelCwMacroF8;
        private System.Windows.Forms.Label labelCwMacroF9;
        private System.Windows.Forms.Label labelCwMacroF9Title;
        private System.Windows.Forms.Label labelCwMacroF8Title;
        private System.Windows.Forms.Label labelCwMacroF7Title;
        private System.Windows.Forms.Label labelCwMacroF6Title;
        private System.Windows.Forms.Label labelCwMacroF5Title;
        private System.Windows.Forms.Label labelCwMacroF4Title;
        private System.Windows.Forms.Label labelCwMacroF3Title;
        private System.Windows.Forms.Label labelCwMacroF2Title;
        private System.Windows.Forms.Label labelCwMacroF1Title;
        private System.Windows.Forms.NumericUpDown numericUpDownMorseSpeed;
        private System.Windows.Forms.Panel panelQth1_2;
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
        private System.Windows.Forms.ToolStripStatusLabel loginLabel;
        private System.Windows.Forms.Label labelDupe;
        private System.Windows.Forms.Label labelComments;
        private System.Windows.Forms.TextBoxCallsign textBoxComments;
        private System.Windows.Forms.ToolStripStatusLabel connectionStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem menuItemAdifExportLoc;
        private System.Windows.Forms.Label labelMorseSpeed;
        private System.Windows.Forms.Panel panelQth3Loc;
        private System.Windows.Forms.Panel panelQsoComments;
        private System.Windows.Forms.Label labelEsm;
        private System.Windows.Forms.CheckBox checkBoxAutoQth3;
    }
}

