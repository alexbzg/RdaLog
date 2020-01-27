namespace tnxlog
{
    partial class TransceiverPinSettings
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelFunction = new System.Windows.Forms.Label();
            this.comboBoxPin = new System.Windows.Forms.ComboBox();
            this.checkBoxInvert = new System.Windows.Forms.CheckBox();
            this.buttonTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelFunction
            // 
            this.labelFunction.AutoSize = true;
            this.labelFunction.Location = new System.Drawing.Point(3, 11);
            this.labelFunction.Name = "labelFunction";
            this.labelFunction.Size = new System.Drawing.Size(28, 13);
            this.labelFunction.TabIndex = 0;
            this.labelFunction.Text = "PTT";
            // 
            // comboBoxPin
            // 
            this.comboBoxPin.FormattingEnabled = true;
            this.comboBoxPin.Location = new System.Drawing.Point(37, 8);
            this.comboBoxPin.Name = "comboBoxPin";
            this.comboBoxPin.Size = new System.Drawing.Size(65, 21);
            this.comboBoxPin.TabIndex = 1;
            this.comboBoxPin.SelectedIndexChanged += new System.EventHandler(this.ComboBoxPin_SelectedIndexChanged);
            // 
            // checkBoxInvert
            // 
            this.checkBoxInvert.AutoSize = true;
            this.checkBoxInvert.Location = new System.Drawing.Point(108, 11);
            this.checkBoxInvert.Name = "checkBoxInvert";
            this.checkBoxInvert.Size = new System.Drawing.Size(53, 17);
            this.checkBoxInvert.TabIndex = 2;
            this.checkBoxInvert.Text = "Invert";
            this.checkBoxInvert.UseVisualStyleBackColor = true;
            // 
            // buttonTest
            // 
            this.buttonTest.Enabled = false;
            this.buttonTest.Location = new System.Drawing.Point(167, 6);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(75, 23);
            this.buttonTest.TabIndex = 3;
            this.buttonTest.Text = "Test";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonTest_MouseDown);
            this.buttonTest.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonTest_MouseUp);
            // 
            // TransceiverPinSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonTest);
            this.Controls.Add(this.checkBoxInvert);
            this.Controls.Add(this.comboBoxPin);
            this.Controls.Add(this.labelFunction);
            this.Name = "TransceiverPinSettings";
            this.Size = new System.Drawing.Size(356, 37);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelFunction;
        private System.Windows.Forms.ComboBox comboBoxPin;
        private System.Windows.Forms.CheckBox checkBoxInvert;
        private System.Windows.Forms.Button buttonTest;
    }
}
