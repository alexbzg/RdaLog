using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace tnxlog
{
    public partial class TransceiverPinSettings : UserControl
    {
        internal string function { get { return labelFunction.Text; } }
        internal string pin { get { return comboBoxPin.SelectedItem?.ToString(); } set { comboBoxPin.SelectedItem = value; } }
        internal bool invert { get { return checkBoxInvert.Checked; } set { checkBoxInvert.Checked = value; } }

        internal bool testEnabled { set { buttonTest.Enabled = value; } }

        internal EventHandler pinChanged;
        internal EventHandler testMouseDown;
        internal EventHandler testMouseUp;

        public TransceiverPinSettings(string _function)
        {
            InitializeComponent();
            labelFunction.Text = _function;
            foreach (string pin in SerialDevice.SerialDevice.PINS)
                comboBoxPin.Items.Add(pin);
        }

        private void ComboBoxPin_SelectedIndexChanged(object sender, EventArgs e)
        {
            pinChanged?.Invoke(this, new EventArgs());
        }

        private void ButtonTest_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                testMouseDown?.Invoke(this, new EventArgs());
        }

        private void ButtonTest_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                testMouseUp?.Invoke(this, new EventArgs());
        }
    }
}
