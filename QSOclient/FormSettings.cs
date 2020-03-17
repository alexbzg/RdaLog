using SerialDevice;
using SerialPortTester;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        private List<SerialDeviceInfo> serialDevices = SerialDevice.SerialDevice.listSerialDevices();
        internal List<Tuple<TextBox, TextBox>> CwMacros
        {
            get
            {
                return new List<Tuple<TextBox, TextBox>>()
                {
                    Tuple.Create(textBoxCwMacroF1Title, textBoxCwMacroF1Bindings),
                    Tuple.Create(textBoxCwMacroF2Title, textBoxCwMacroF2Bindings),
                    Tuple.Create(textBoxCwMacroF3Title, textBoxCwMacroF3Bindings),
                    Tuple.Create(textBoxCwMacroF4Title, textBoxCwMacroF4Bindings),
                    Tuple.Create(textBoxCwMacroF5Title, textBoxCwMacroF5Bindings),
                    Tuple.Create(textBoxCwMacroF6Title, textBoxCwMacroF6Bindings),
                    Tuple.Create(textBoxCwMacroF7Title, textBoxCwMacroF7Bindings),
                    Tuple.Create(textBoxCwMacroF8Title, textBoxCwMacroF8Bindings),
                    Tuple.Create(textBoxCwMacroF9Title, textBoxCwMacroF9Bindings)
                };
            }
        }

        internal Dictionary<int, RadioButton> updateIntervalRadioButtons;

        internal Dictionary<string, CheckBox> mainFormPanelCheckboxes;

        internal List<TransceiverPinSettings> transceiverPinSettings = new List<TransceiverPinSettings>();

        private TransceiverController transceiverController = new TransceiverController(new TransceiverControllerConfig());

        internal string serialDeviceId {
            get { return comboBoxPort.SelectedIndex == -1 ? null : serialDevices[comboBoxPort.SelectedIndex].deviceID; }
            set {
                SerialDeviceInfo device = serialDevices.FirstOrDefault(item => item.deviceID == value);
                if (device != null)
                    comboBoxPort.SelectedItem = device.caption;
                else
                    comboBoxPort.SelectedIndex = -1;
            }
        }

        internal bool watchAdifLog
        {
            get { return checkBoxWatchAdifLog.Checked; }
            set { checkBoxWatchAdifLog.Checked = value; }
        }

        internal string watchAdifLogPath
        {
            get { return textBoxWatchAdifLogPath.Text; }
            set { textBoxWatchAdifLogPath.Text = value; }
        }

        internal string esmMacro
        {
            get { return textBoxEsmMacro.Text; }
            set { textBoxEsmMacro.Text = value; }
        }

        internal int autoCqRxPause
        {
            get { return Decimal.ToInt32(numericUpDownAutoCqPause.Value); }
            set { numericUpDownAutoCqPause.Value = value; }
        }

        internal bool enableCwMacros { get { return checkBoxEnableCwMacros.Checked; } set { checkBoxEnableCwMacros.Checked = value; } }

        private string dataPath;
        public FormSettings(string _dataPath)
        {
            dataPath = _dataPath;
            InitializeComponent();

            mainFormPanelCheckboxes = new Dictionary<string, CheckBox>()
            {
                {"statusFields", checkBoxViewQth1_2 },
                {"statusFieldsLocUsr", checkBoxViewQth3Loc },
                {"qsoComments", checkBoxViewQsoComments },
                {"statFilter", checkBoxViewStatFilter },
                {"cwMacros", checkBoxViewCwMacro },
                {"callsignId", checkBoxViewCallsignId }
            };

            updateIntervalRadioButtons = new Dictionary<int, RadioButton>()
            {
                {10 * 1000, radioButtonUpdInterval10s },
                {60 * 1000, radioButtonUpdInterval1m }
            };

            foreach (SerialDeviceInfo sp in serialDevices)
            {
                comboBoxPort.Items.Add(sp.caption);
                int w = TextRenderer.MeasureText(sp.caption, comboBoxPort.Font).Width;
                if (comboBoxPort.DropDownWidth < w)
                    comboBoxPort.DropDownWidth = w;
            }

            int pinCount = 0;
            foreach (string pinFunction in TransceiverController.PIN_FUNCTIONS)
            {
                TransceiverPinSettings tpsControl = new TransceiverPinSettings(pinFunction);
                transceiverPinSettings.Add(tpsControl);
                tabPageCwMacros.Controls.Add(tpsControl);
                tpsControl.Location = new Point(1, 48 + (pinCount++) * (tpsControl.Height + 2));
                tpsControl.pinChanged += transceiverPinChanged;
                tpsControl.invertChanged += transceiverPinInvertChanged;
                tpsControl.testMouseDown += testPinMouseDown;
                tpsControl.testMouseUp += testPinMouseUp;
            }
#if !DEBUG
            tabControl.TabPages.Remove(tabPageDigi);
#endif
        }

        private void testPinMouseUp(object sender, EventArgs e)
        {
            TransceiverPinSettings tpsSender = (TransceiverPinSettings)sender;
            transceiverController.setPin(tpsSender.function, true);
        }

        private void testPinMouseDown(object sender, EventArgs e)
        {
            TransceiverPinSettings tpsSender = (TransceiverPinSettings)sender;
            transceiverController.setPin(tpsSender.function, false);
        }

        private void transceiverPinInvertChanged(object sender, EventArgs e)
        {
            updateTransceiverController();
        }

        private void transceiverPinChanged(object sender, EventArgs e)
        {
            TransceiverPinSettings tpsSender = (TransceiverPinSettings)sender;
            string freePin = null;
            foreach (string pin in SerialDevice.SerialDevice.PINS)
            {
                bool busy = false;
                foreach (TransceiverPinSettings tpsControl in transceiverPinSettings)
                    if (tpsControl.pin == pin)
                    {
                        busy = true;
                        break;
                    }
                if (!busy)
                {
                    freePin = pin;
                    break;
                }
            }
            if (freePin != null)
                foreach (TransceiverPinSettings tpsControl in transceiverPinSettings)
                    if (tpsControl != tpsSender && tpsControl.pin == tpsSender.pin)
                    {
                        tpsControl.pin = freePin;
                        break;
                    }
            updateTransceiverController();
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
            foreach (TransceiverPinSettings tpsControl in transceiverPinSettings)
                tpsControl.testEnabled = comboBoxPort.SelectedIndex != -1;
        }


        private void checkBoxEnableCwMacros_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxPort.Enabled = checkBoxEnableCwMacros.Checked;
            foreach (TransceiverPinSettings tps in transceiverPinSettings)
                tps.Enabled = checkBoxEnableCwMacros.Checked;
        }

        private void ComboBoxPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            transceiverController.disconnect();
            if (comboBoxPort.SelectedIndex != -1)
            {
                transceiverController.config.updateFromForm(this);
                transceiverController.connect();
            }
        }

        private void updateTransceiverController()
        {
            transceiverController.config.updateFromForm(this);
            transceiverController.initializePort();
        }

        private void FormSettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            transceiverController.disconnect();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void CheckBoxWatchAdifLog_CheckedChanged(object sender, EventArgs e)
        {
            textBoxWatchAdifLogPath.Enabled = watchAdifLog;
            buttonAdifLogBrowse.Enabled = watchAdifLog;
        }

        private void ButtonAdifLogBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialogAdifLog.ShowDialog() == DialogResult.OK)
                textBoxWatchAdifLogPath.Text = openFileDialogAdifLog.FileName;
        }

        private void TextBoxWatchAdifLogPath_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
