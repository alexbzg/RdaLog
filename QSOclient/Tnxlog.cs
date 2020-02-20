﻿using HamRadio;
using NLog;
using SerializationNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using XmlConfigNS;

namespace tnxlog
{
    public class StatusFieldChangeEventArgs: EventArgs
    {
        public string field;
        public string value;
    }

    public class Tnxlog
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private FormMain _formMain;
        public FormMain formMain { get { return _formMain; } }
        private FormLog formLog;
        private TnxlogConfig config;
        public HttpService httpService;
        public TransceiverController transceiverController;
        public AdifLogWatcher adifLogWatcher = new AdifLogWatcher();
        internal string dataPath;
        private NLog.Targets.FileTarget logfile;
        private string qsoFilePath;
        internal BindingList<QSO> qsoList;
        private QSOFactory qsoFactory;
        public EventHandler<StatusFieldChangeEventArgs> statusFieldChange;
        private Dictionary<string, string> _statusFields = new Dictionary<string, string>();
        public void setStatusFieldValue(string field, string value)
        {
            if (_statusFields[field] != value)
            {
                _statusFields[field] = value;
                config.setStatusFieldValue(field, value);
                statusFieldChange?.Invoke(this, new StatusFieldChangeEventArgs()
                {
                    field = field,
                    value = value
                });
            }
        }
        public string getStatusFieldValue(string field)
        {
            return _statusFields[field];
        }

        public string userField { get { return config.userField; } }



        public Tnxlog()
        {
#if DEBUG || LOG
            var loggingConfig = new NLog.Config.LoggingConfiguration();
#endif
            dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "tnxlog");
#if DEBUG
            dataPath = Path.Combine(dataPath, "debug");
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");
            loggingConfig.AddRule(LogLevel.Debug, LogLevel.Fatal, logconsole);
#endif
            if (!Directory.Exists(dataPath))
                Directory.CreateDirectory(dataPath);
#if LOG
            logfile = new NLog.Targets.FileTarget("logfile") {
                FileName = Path.Combine(dataPath, "debug.log"),
                Layout = "${longdate} ${level} ${message}  ${exception:format=toString,Data:maxInnerExceptionLevel=10} ${stacktrace:format=DetailedFlat:topFrames=10}",
                ArchiveEvery = NLog.Targets.FileArchivePeriod.Sunday,
                Encoding = Encoding.UTF8,
                MaxArchiveFiles = 1 };
            loggingConfig.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);
#endif
#if DEBUG || LOG
            NLog.LogManager.Configuration = loggingConfig;
#endif
#if LOG
            Logger.Info("Start");
#endif
            qsoFilePath = Path.Combine(dataPath, "qso.dat");
            config = XmlConfig.create<TnxlogConfig>(Path.Combine(dataPath, "config.xml"));
            foreach (string field in TnxlogConfig.StatusFields)
                _statusFields[field] = config.getStatusFieldValue(field);
            httpService = new HttpService(config.httpService, this);
            transceiverController = new TransceiverController(config.transceiverController);
            adifLogWatcher.OnNewAdifEntry += newAdifLogEntry;   
            qsoFactory = new QSOFactory(this);
            qsoList = ProtoBufSerialization.Read<BindingList<QSO>>(qsoFilePath);
            if (qsoList == null)
            {
                qsoList = new BindingList<QSO>();
                qsoFactory.no = 1;
            }
            else if (qsoList.Count > 0)
                qsoFactory.no = qsoList.First().no + 1;
            qsoList.ListChanged += QsoList_ListChanged;
            _formMain = new FormMain(config.formMain, this);
            if (config.autoLogin)
                Task.Run(async () => await httpService.login(true));
            qsoList.Where(qso => qso.serverTs == 0).Select(async qso => await httpService.postQso(qso));
            initServices();
        }

        private async void newAdifLogEntry(object sender, NewAdifEntryEventArgs e)
        {
            QSO qso = qsoFactory.fromADIF(e.adif);
            qsoList.Insert(0, qso);
            writeQsoList();
            await httpService.postQso(qso);
        }

        private void initServices()
        {
            if (config.enableCwMacros)
                transceiverController.connect();
            if (config.watchAdifLog)
                adifLogWatcher.start(config.watchAdifLogPath);
            else
                adifLogWatcher.stop();
        }

        public async Task deleteQso(QSO qso)
        {
            qsoList.Remove(qso);
            await httpService.deleteQso(qso);
        }

        private void QsoList_ListChanged(object sender, ListChangedEventArgs e)
        {
            writeQsoList();
        }

        public void showFormLog()
        {
            if (formLog == null)
            {
                formLog = new FormLog(config.formLog, this);
                formLog.FormClosed += FormLog_FormClosed;
                formLog.Show();
            }
            else
                formLog.Focus();
        }

        private void FormLog_FormClosed(object sender, FormClosedEventArgs e)
        {
            formLog.Dispose();
            formLog = null;
        }

        public async Task newQso(string callsign, string myCallsign, decimal freq, string mode, string rstRcvd, string rstSnt, string comments)
        {
            QSO qso = qsoFactory.create(callsign, myCallsign, freq, mode, rstRcvd, rstSnt, comments);
            qsoList.Insert(0, qso);
            writeQsoList();
            await httpService.postQso(qso);
        }

        public async Task postFreq(decimal freq)
        {
            await httpService.postFreq(QSO.formatFreq(freq));
        }

        public void writeQsoList()
        {
            ProtoBufSerialization.Write<BindingList<QSO>>(qsoFilePath, qsoList);
        }

        public void clearQso()
        {
            qsoList.Clear();
            writeQsoList();
            qsoFactory.no = 1;
        }

        public void showSettings()
        {
            transceiverController.disconnect();
            FormSettings formSettings = new FormSettings(dataPath);

            formSettings.textBoxLogin.Text = config.httpService.callsign;
            formSettings.textBoxPassword.Text = config.httpService.password;
            if (config.httpService.updateIterval != 0)
                formSettings.updateIntervalRadioButtons[config.httpService.updateIterval].Checked = true;
            formSettings.buttonLogin.Click += async delegate (object sender, EventArgs e)
            {
                config.httpService.callsign = formSettings.textBoxLogin.Text;
                config.httpService.password = formSettings.textBoxPassword.Text;
                System.Net.HttpStatusCode? loginStatusCode = await httpService.login();
                if (loginStatusCode == System.Net.HttpStatusCode.OK)
                    MessageBox.Show("Logged in successfully.", Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            formSettings.checkBoxAutoLogin.Checked = config.autoLogin;

            foreach (KeyValuePair<string,CheckBox> item in formSettings.mainFormPanelCheckboxes)
                item.Value.Checked = config.getMainFormPanelVisible(item.Key);

            formSettings.enableCwMacros = config.enableCwMacros;
            formSettings.serialDeviceId = config.transceiverController.serialDeviceId;
            for (int co = 0; co < TransceiverController.PIN_FUNCTIONS.Count; co++)
            {
                formSettings.transceiverPinSettings[co].pin = SerialDevice.SerialDevice.PINS[config.transceiverController.pinout[co]];
                formSettings.transceiverPinSettings[co].invert = config.transceiverController.invertPins[co];
            }
            formSettings.autoCqRxPause = config.autoCqRxPause;

            for (int co = 0; co < formSettings.CwMacros.Count; co++)
            {
                formSettings.CwMacros[co].Item1.Text = config.cwMacros[co][0];
                formSettings.CwMacros[co].Item2.Text = config.cwMacros[co][1];
            }

            formSettings.watchAdifLog = config.watchAdifLog;
            formSettings.watchAdifLogPath = config.watchAdifLogPath;

            if (formSettings.ShowDialog(this.formMain) == System.Windows.Forms.DialogResult.OK)
            {
                config.httpService.callsign = formSettings.textBoxLogin.Text;
                config.httpService.password = formSettings.textBoxPassword.Text;
                config.httpService.updateIterval = formSettings.updateIntervalRadioButtons.Where(x => x.Value.Checked).FirstOrDefault().Key;

                config.autoLogin = formSettings.checkBoxAutoLogin.Checked;
                if (config.autoLogin && !httpService.connected)
                    Task.Run(async () => await httpService.login(true));

                foreach (KeyValuePair<string, CheckBox> item in formSettings.mainFormPanelCheckboxes)
                     config.setMainFormPanelVisible(item.Key, item.Value.Checked);

                config.enableCwMacros = formSettings.enableCwMacros;
                updateTransceiverControllerConfig(config.transceiverController, formSettings);
                config.autoCqRxPause = formSettings.autoCqRxPause;

                for (int co = 0; co < formSettings.CwMacros.Count; co++)
                {
                    config.cwMacros[co][0] = formSettings.CwMacros[co].Item1.Text;
                    config.cwMacros[co][1] = formSettings.CwMacros[co].Item2.Text;
                }

                config.watchAdifLog = formSettings.watchAdifLog;
                config.watchAdifLogPath = formSettings.watchAdifLogPath;

                config.write();
            }
            formSettings.Dispose();
            initServices();
            formMain.updateCwMacrosTitles();
        }

        private void updateTransceiverControllerConfig(TransceiverControllerConfig tcConfig, FormSettings formSettings)
        {
            tcConfig.serialDeviceId = formSettings.serialDeviceId;
            for (int co = 0; co < TransceiverController.PIN_FUNCTIONS.Count; co++)
            {
                tcConfig.pinout[co] = SerialDevice.SerialDevice.PINS.IndexOf(formSettings.transceiverPinSettings[co].pin);
                tcConfig.invertPins[co] = formSettings.transceiverPinSettings[co].invert;
            }
        }

    }
}
