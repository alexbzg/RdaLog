﻿using HamRadio;
using LiteDB;
using NLog;
using ProtoBuf;
using SerializationNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using XmlConfigNS;

namespace tnxlog
{
    public class QthFieldChangeEventArgs: EventArgs
    {
        public int field;
        public string value;
    }

    public class Tnxlog
    {
        public const string FfmpegPath = "ffmpeg.exe";
        public const string FfmpegRecordArgs = "-y -c:a libmp3lame -ar 44100 -b:a 36k -ac 1";
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private const int QsoBakCount = 3;


        private FormMain _formMain;
        public FormMain formMain { get { return _formMain; } }
        private FormLog formLog;
        internal TnxlogConfig config;
        public HttpService httpService;
        public TransceiverController transceiverController;
        public AdifLogWatcher adifLogWatcher = new AdifLogWatcher();
        internal string dataPath;
        private NLog.Targets.FileTarget logfile;
        private string qsoFilePath;
        internal QsoDB qsoDB;
        internal QSOFactory qsoFactory;
        public BindingList<QSO> qsoList;
        public EventHandler<QthFieldChangeEventArgs> qthFieldChange;
        public EventHandler locChange;
        public EventHandler<QthFieldChangeEventArgs> qthFieldTitleChange;
        private string[] qthFields = new string[TnxlogConfig.QthFieldCount];
        public void setQthField(int field, string value)
        {
            if (qthFields[field] != value)
            {
                qthFields[field] = value;
                config.setQthFieldValue(field, value);
                qthFieldChange?.Invoke(this, new QthFieldChangeEventArgs()
                {
                    field = field,
                    value = value
                });
            }
        }
        public string getQthFieldValue(int field)
        {
            return qthFields[field];
        }

        private string _loc;
        public string loc
        {
            get { return _loc; }
            set
            {
                if (_loc != value)
                {
                    _loc = value;
                    config.loc = value;
                    locChange?.Invoke(this, new EventArgs());
                }
            }
        }

        private string[] qthFieldTitles = new string[TnxlogConfig.QthFieldCount];
        public void setQthFieldTitle(int field, string value)
        {
            if (qthFieldTitles[field] != value)
            {
                qthFieldTitles[field] = value;
                config.setQthFieldTitle(field, value);
                qthFieldTitleChange?.Invoke(this, new QthFieldChangeEventArgs()
                {
                    field = field,
                    value = value
                });
            }
        }

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
            bool initSuccess = false;
            bool initFail = false;
            while (!initSuccess)
            {
                try
                {
                    qsoDB = new QsoDB(Path.Combine(dataPath, "qso.db"));
                    config = XmlConfig.create<TnxlogConfig>(Path.Combine(dataPath, "config.xml"), forceCreate: initFail);
                    for (int field = 0; field < TnxlogConfig.QthFieldCount; field++)
                        qthFields[field] = config.getQthFieldValue(field);
                    _loc = config.loc;
                    httpService = new HttpService(config.httpService, this);
                    transceiverController = new TransceiverController(config.transceiverController);
                    adifLogWatcher.OnNewAdifEntry += newAdifLogEntry;

                    if (qsoDB.qso.Count() == 0 && File.Exists(qsoFilePath))
                    {
                        List<QSO> qsoList = QSOFactory.ReadList<List<QSO>>(qsoFilePath);
                        qsoList.Reverse();
                        foreach (QSO qso in qsoList)
                            qso._id = new ObjectId();
                        if (qsoList != null && qsoList.Count > 0)
                            qsoDB.qso.InsertBulk(qsoList);
                    }

                    qsoFactory = new QSOFactory(this);
                    qsoList = new BindingList<QSO>(qsoDB.qso.FindAll().OrderByDescending(qso => qso._id).ToList());
                    qsoFactory.no = qsoList.Count == 0 ? 1 : qsoList.First().no + 1;
                    qsoList.ListChanged += QsoList_ListChanged;

                    _formMain = new FormMain(config.formMain, this);
                    if (config.autoLogin)
                        Task.Run(async () => await httpService.login(true));

                    QSO[] qsosPending = qsoDB.qso.Find(item => item.serverState == ServerState.Pending).ToArray();
                    if (qsosPending.Length > 0)
                    {
                        Task.Run(async () => await httpService.postQso(qsosPending));
                    }

                    initServices();
                    initSuccess = true;
                }
                catch (Exception e)
                {
                    Logger.Error(e, "Tnxlog initialiazion exception");
                    initFail = true;
                }
            }
        }

        private async void newAdifLogEntry(object sender, NewAdifEntryEventArgs e)
        {
            QSO qso = qsoFactory.fromADIF(e.adif, loc: loc);
            if (qsoDB.qso.Exists(Query.And(
                Query.EQ("ts", qso.ts),
                Query.EQ("cs", qso.cs),
                Query.EQ("myCS", qso.myCS),
                Query.EQ("freq", qso.freq),
                Query.EQ("mode", qso.mode))))
                return;
            _formMain.DoInvoke(() => { qsoList.Insert(0, qso); });
            await httpService.postQso(qso);
        }

        private void initServices()
        {
            if (config.transceiverController.transceiverType != 0)
                transceiverController.connect();
            if (config.watchAdifLog)
                adifLogWatcher.start(config.watchAdifLogPath);
            else
                adifLogWatcher.stop();
        }

        public async Task deleteQso(QSO qso)
        {
            qsoList.Remove(qso);
            qsoDB.qso.Delete(qso._id);
            await httpService.deleteQso(qso);
        }

        private void QsoList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded)
                qsoDB.qso.Insert(qsoList[e.NewIndex]);
            else if (e.ListChangedType == ListChangedType.ItemChanged)
                qsoDB.qso.Update(qsoList[e.NewIndex]);
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

        public async Task newQso(string callsign, string myCallsign, decimal freq, string mode, string rstRcvd, string rstSnt, string comments, string sound)
        {
            QSO qso = qsoFactory.create(callsign, myCallsign, freq, mode, rstRcvd, rstSnt, comments, null, sound);
            qsoList.Insert(0, qso);
            await httpService.postQso(qso);
        }

        public async Task postFreq(decimal freq)
        {
            await httpService.postFreq(QSO.formatFreq(freq));
        }


        public void clearQso()
        {
            qsoList.Clear();
            qsoDB.qso.DeleteAll();
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

            formSettings.cwTransceiverType = config.transceiverController.transceiverType;
            formSettings.serialDeviceId = config.transceiverController.serialDeviceId;
            for (int co = 0; co < TransceiverController.PIN_FUNCTIONS.Count; co++)
            {
                formSettings.transceiverPinSettings[co].pin = config.transceiverController.pinout[co] == -1 ? "" : SerialDevice.SerialDevice.PINS[config.transceiverController.pinout[co]];
                formSettings.transceiverPinSettings[co].invert = config.transceiverController.invertPins[co];
            }
            formSettings.tciHost = config.transceiverController.tciHost;
            formSettings.tciPort = config.transceiverController.tciPort;
            formSettings.tciTrnsNo = config.transceiverController.tciTrnsNo;
            formSettings.autoCqRxPause = config.autoCqRxPause;
            formSettings.esmMacro = config.esmMacro;

            for (int co = 0; co < formSettings.CwMacros.Count; co++)
            {
                formSettings.CwMacros[co].Item1.Text = config.cwMacros[co][0];
                formSettings.CwMacros[co].Item2.Text = config.cwMacros[co][1];
            }

            formSettings.watchAdifLog = config.watchAdifLog;
            formSettings.watchAdifLogPath = config.watchAdifLogPath;

            for (int field = 0; field < TnxlogConfig.QthFieldCount; field++)
            {
                formSettings.setQthFieldAdifLabel(field, config.qthFieldTitles[field]);
                formSettings.setQthFieldAdif(field, config.qthAdifFields[field]);
            }
            formSettings.setCommentFieldAdif(config.commentAdifField);

            formSettings.soundRecordDevice = config.soundRecordDevice;
            formSettings.soundRecordFolder = config.soundRecordFolder;

            if (formSettings.ShowDialog(this.formMain) == System.Windows.Forms.DialogResult.OK)
            {
                config.httpService.callsign = formSettings.textBoxLogin.Text;
                config.httpService.password = formSettings.textBoxPassword.Text;
                config.httpService.updateIterval = formSettings.updateIntervalRadioButtons.Where(x => x.Value.Checked).FirstOrDefault().Key;

                config.autoLogin = formSettings.checkBoxAutoLogin.Checked;
                if (config.autoLogin && !httpService.connected)
                    Task.Run(async () => await httpService.login(true));

                config.setMainFormPanelsVisible(formSettings.mainFormPanelCheckboxes.Select(item => { return new KeyValuePair<string, bool>(item.Key, item.Value.Checked); }));

                updateTransceiverControllerConfig(config.transceiverController, formSettings);
                config.autoCqRxPause = formSettings.autoCqRxPause;
                config.esmMacro = formSettings.esmMacro;

                for (int co = 0; co < formSettings.CwMacros.Count; co++)
                {
                    config.cwMacros[co][0] = formSettings.CwMacros[co].Item1.Text.ToUpper();
                    config.cwMacros[co][1] = formSettings.CwMacros[co].Item2.Text.ToUpper();
                }

                config.watchAdifLog = formSettings.watchAdifLog;
                config.watchAdifLogPath = formSettings.watchAdifLogPath;

                for (int field = 0; field < TnxlogConfig.QthFieldCount; field++)
                    config.qthAdifFields[field] = formSettings.getQthFieldAdif(field).Trim().ToUpper();
                config.commentAdifField = formSettings.getCommentFieldAdif().Trim().ToUpper();
                formMain.adifQthMenu();

                config.soundRecordDevice = formSettings.soundRecordDevice;
                config.soundRecordFolder = formSettings.soundRecordFolder;

                config.write();
            }
            formSettings.Dispose();
            initServices();
            formMain.updateCwMacrosTitles();
        }

        private void updateTransceiverControllerConfig(TransceiverControllerConfig tcConfig, FormSettings formSettings)
        {
            tcConfig.transceiverType = formSettings.cwTransceiverType;
            tcConfig.serialDeviceId = formSettings.serialDeviceId;
            for (int co = 0; co < TransceiverController.PIN_FUNCTIONS.Count; co++)
            {
                tcConfig.pinout[co] = SerialDevice.SerialDevice.PINS.IndexOf(formSettings.transceiverPinSettings[co].pin);
                tcConfig.invertPins[co] = formSettings.transceiverPinSettings[co].invert;
            }
            tcConfig.tciHost = formSettings.tciHost;
            tcConfig.tciPort = formSettings.tciPort;
            tcConfig.tciTrnsNo = formSettings.tciTrnsNo;
        }

    }
}
