using SerialDevice;
using SerialPortTester;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tnxlog
{
    public partial class FormSettings : Form
    {
        private List<SerialDeviceInfo> serialPorts = SerialDevice.SerialDevice.listSerialDevices();
        private SerialPort serialPort;
        internal List<Tuple<TextBox, TextBox>> HotKeyBindings
        {
            get
            {
                return new List<Tuple<TextBox, TextBox>>()
                {
                    Tuple.Create(textBoxHotKeyF1Title, textBoxHotKeyF1Bindings),
                    Tuple.Create(textBoxHotKeyF2Title, textBoxHotKeyF2Bindings),
                    Tuple.Create(textBoxHotKeyF3Title, textBoxHotKeyF3Bindings),
                    Tuple.Create(textBoxHotKeyF4Title, textBoxHotKeyF4Bindings),
                    Tuple.Create(textBoxHotKeyF5Title, textBoxHotKeyF5Bindings),
                    Tuple.Create(textBoxHotKeyF6Title, textBoxHotKeyF6Bindings),
                    Tuple.Create(textBoxHotKeyF7Title, textBoxHotKeyF7Bindings),
                    Tuple.Create(textBoxHotKeyF8Title, textBoxHotKeyF8Bindings),
                    Tuple.Create(textBoxHotKeyF9Title, textBoxHotKeyF9Bindings)
                };
            }
        }

        internal Dictionary<int, RadioButton> updateIntervalRadioButtons;

        internal Dictionary<string, CheckBox> mainFormPanelCheckboxes;
        private string dataPath;
        public FormSettings(string _dataPath)
        {
            dataPath = _dataPath;
            InitializeComponent();
            mainFormPanelCheckboxes = new Dictionary<string, CheckBox>()
            {
                {"statusFields", checkBoxViewFields },
                {"statFilter", checkBoxViewStatFilter },
                {"cwMacros", checkBoxViewCwMacro },
                {"callsignId", checkBoxViewCallsignId }
            };
            updateIntervalRadioButtons = new Dictionary<int, RadioButton>()
            {
                {10 * 1000, radioButtonUpdInterval10s },
                {60 * 1000, radioButtonUpdInterval1m }
            };
            foreach (SerialDeviceInfo sp in serialPorts)
            {
                comboBoxPort.Items.Add(sp.caption);
                int w = TextRenderer.MeasureText(sp.caption, comboBoxPort.Font).Width;
                if (comboBoxPort.DropDownWidth < w)
                    comboBoxPort.DropDownWidth = w;
            }
            comboBoxPort.Items.Add("Disable");
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabPageDebug)
            {
                textBoxDebugLog.Text = File.ReadAllText(Path.Combine(dataPath, "debug.log"));
            }
        }

        private void TabPageSerial_Click(object sender, EventArgs e)
        {

        }

        private void ComboBoxPort_SelectedValueChanged(object sender, EventArgs e)
        {
            serialPort?.Close();
            if (comboBoxPort.SelectedIndex != -1)
            {
                string portName = serialPorts[comboBoxPort.SelectedIndex].portName;
                serialPort = new SerialPort(portName);
                try
                {
                    SerialPortFixer.Execute(portName);
                    serialPort.Open();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.TraceInformation("Error opening port " + portName + " " + ex.ToString());
                }
            }
            else
                serialPort = null;

        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (serialPort != null)
                serialPort.DtrEnable = checkBoxInvertRts.Checked;
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (serialPort != null)
                serialPort.RtsEnable = checkBoxInvertDtr.Checked;

        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
