using FfmpegIinterfaceNS;
using InvokeFormNS;
using SerialDevice;
using SerialPortTester;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tnxlog
{
    public partial class FormSettings : InvokeForm
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

        private List<LabelTextBox> qthFieldAdifContols = new List<LabelTextBox>();
        private LabelTextBox ltbComment;


        public string getQthFieldAdif(int field)
        {
            return qthFieldAdifContols[field].editText;
        }

        public void setQthFieldAdif(int field, string value)
        {
            qthFieldAdifContols[field].editText = value;
        }

        public string getCommentFieldAdif()
        {
            return ltbComment.editText;
        }

        public void setCommentFieldAdif(string value)
        {
            ltbComment.editText = value;
        }


        public void setQthFieldAdifLabel(int field, string value)
        {
            qthFieldAdifContols[field].labelText = TnxlogConfig.QthFieldTitle(field, value);
        }

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

        internal string tciHost
        {
            get { return textBoxTransceiverTciHost.Text.Trim(); }
            set { textBoxTransceiverTciHost.Text = value; }
        }

        internal uint tciPort
        {
            get { return Convert.ToUInt32(textBoxTransceiverTciPort.Text); }
            set { textBoxTransceiverTciPort.Text = value.ToString(); }
        }

        internal uint tciTrnsNo
        {
            get { return Convert.ToUInt16(textBoxTransceiverTciTrnsNo.Text); }
            set { textBoxTransceiverTciTrnsNo.Text = value.ToString(); }
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

        internal int cwTransceiverType { get { return comboBoxTransceiverType.SelectedIndex; } set { comboBoxTransceiverType.SelectedIndex = value; } }

        internal string soundRecordFolder { get { return textBoxSoundRecordFolder.Text; } set { textBoxSoundRecordFolder.Text = value; } }
        private string _soundRecordDevice = "";
        public string soundRecordDevice {
            get { return _soundRecordDevice; }
            set {
                _soundRecordDevice = value;
                if (comboBoxSoundRecordDevice.Items.Contains(value))
                {
                    comboBoxSoundRecordDevice.SelectedItem = value;
                }
            }
        }
        private FfmpegInterface soundRecordTestInterface;

        private string dataPath;

        public FormSettings(string _dataPath)
        {
            dataPath = _dataPath;
            InitializeComponent();

            mainFormPanelCheckboxes = new Dictionary<string, CheckBox>()
            {
                {"qth1_2", checkBoxViewQth1_2 },
                {"qth3Loc", checkBoxViewQth3Loc },
                {"qsoComments", checkBoxViewQsoComments },
                {"statFilter", checkBoxViewStatFilter },
                {"cwMacros", checkBoxViewCwMacro },
                {"callsignId", checkBoxViewCallsignId },
                {"soundRecord", checkBoxViewSoundRecord }
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
                panelTransceiverSerial.Controls.Add(tpsControl);
                tpsControl.Location = new Point(1, 25 + (pinCount++) * (tpsControl.Height + 2));
                tpsControl.pinChanged += transceiverPinChanged;
                tpsControl.invertChanged += transceiverPinInvertChanged;
                tpsControl.testMouseDown += testPinMouseDown;
                tpsControl.testMouseUp += testPinMouseUp;
            }

            for (int field = 0; field < TnxlogConfig.QthFieldCount; field++)
            {
                LabelTextBox ltb = new LabelTextBox();
                qthFieldAdifContols.Add(ltb);
                groupBoxAdifFields.Controls.Add(ltb);
                ltb.Location = new Point(1, 14 + field * (ltb.Height + 2));
            }
            ltbComment = new LabelTextBox();
            qthFieldAdifContols.Add(ltbComment);
            groupBoxAdifFields.Controls.Add(ltbComment);
            ltbComment.Location = new Point(1, 14 + TnxlogConfig.QthFieldCount * (ltbComment.Height + 2));
            ltbComment.labelText = "Comment";

            enumerateSoundRecordDevices();

        }

        private void enumerateSoundRecordDevices()
        {
            FfmpegInterface ffmpeg = new FfmpegInterface(Tnxlog.FfmpegPath, "-list_devices true -f dshow -i dummy");
            List<string> devices = new List<string>();
            ffmpeg.DataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                if (e.Data != null && e.Data.EndsWith("(audio)"))
                {
                    string devName = e.Data.Split(new char[] { '"' }, 3)[1];
                    byte[] bytes = Encoding.Default.GetBytes(devName);
                    devices.Add(Encoding.UTF8.GetString(bytes));
                }
            });
            ffmpeg.Exited += new EventHandler((sender, e) => {
                DoInvoke(() => {
                    comboBoxSoundRecordDevice.Items.AddRange(devices.ToArray());
                    if (comboBoxSoundRecordDevice.Items.Contains(_soundRecordDevice))
                    {
                        comboBoxSoundRecordDevice.SelectedItem = _soundRecordDevice;
                    }
                    else if (comboBoxSoundRecordDevice.Items.Count > 0)
                    {
                        comboBoxSoundRecordDevice.SelectedIndex = 0;
                    }
                });
            });
            ffmpeg.Start();
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

        private void buttonClip_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBoxDebugLog.Text);
            MessageBox.Show("Текст скопирован в буфер обмена.\nThe text was copied to clipboard.", Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void comboBoxTransceiverType_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelTransceiverSerial.Visible = comboBoxTransceiverType.SelectedIndex == 1;
            panelTransceiverTCI.Visible = comboBoxTransceiverType.SelectedIndex == 2;
        }

        private void textBoxTransceiverTciHost_Validating(object sender, CancelEventArgs e)
        {
        }

        private void textBoxTransceiverTciPort_Validating(object sender, CancelEventArgs e)
        {

        }

        private void FormSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                if (cwTransceiverType == 2)
                {
                    if ((tciHost != "localhost") && (!IPAddress.TryParse(tciHost, out _)))
                    {
                        MessageBox.Show("Enter a valid IP address (Refer EESDR -> Options -> TCI Tab).\nВведите корректный IP-адрес (см. EESDR -> Options -> TCI Tab).");
                        e.Cancel = true;
                        return;
                    }
                    if (tciPort == 0)
                    {
                        MessageBox.Show("Enter a valid port number (Refer EESDR -> Options -> TCI Tab).\nВведите корректный номер порта (см. EESDR -> Options -> TCI Tab).");
                        e.Cancel = true;
                        return;
                    }

                }
            }
        }

        private void ButtonSoundInputRefresh_Click(object sender, EventArgs e)
        {
            comboBoxSoundRecordDevice.Items.Clear();
            enumerateSoundRecordDevices();
        }

        private void ComboBoxSoundRecordDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            _soundRecordDevice = (string)comboBoxSoundRecordDevice.SelectedItem;
        }

        private void ButtonSoundRecordFolderBrowse_Click(object sender, EventArgs e)
        {
            if (textBoxSoundRecordFolder.Text.Length > 0)
                folderBrowserDialogSoundRecord.SelectedPath = textBoxSoundRecordFolder.Text;
            if (folderBrowserDialogSoundRecord.ShowDialog() == DialogResult.OK)
                textBoxSoundRecordFolder.Text = folderBrowserDialogSoundRecord.SelectedPath;
        }

        private string soundRecordTestFile()
        {
            return Path.Combine(soundRecordFolder, "test.mp3");
        }

        private void CheckBoxTestSoundRecord_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTestSoundRecord.Checked)
            {
                soundRecordTestInterface = FfmpegInterface.AudioRecorder(Tnxlog.FfmpegPath, soundRecordDevice, Tnxlog.FfmpegRecordArgs, soundRecordTestFile());
                soundRecordTestInterface.setTimer(30000);
                soundRecordTestInterface.Exited += soundRecordTestExited;
                soundRecordTestInterface.Start();
            } else
            {
                soundRecordTestInterface?.Stop();
            }
        }

        private void soundRecordTestExited(object sender, EventArgs e)
        {
            DoInvoke(() =>
            {
                checkBoxTestSoundRecord.Checked = false;
            });
            var process = new Process();
            process.StartInfo = new ProcessStartInfo(soundRecordTestFile())
            {
                UseShellExecute = true
            };
            process.Start();
        }
    }
}
