﻿using HamRadio;
using SerializationNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XmlConfigNS;

namespace RdaLog
{
    public class StatusFieldChangeEventArgs: EventArgs
    {
        public string field;
        public string value;
    }

    public class RdaLog
    {
        private FormMain _formMain;
        public FormMain formMain { get { return _formMain; } }
        private FormLog formLog;
        private RdaLogConfig config;
        private HttpService httpService;
        private readonly string qsoFilePath = Application.StartupPath + "\\qso.dat";
        internal BindingList<QSO> qsoList;
        private int qsoNo = 0;
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



        public RdaLog()
        {
            config = XmlConfig.create<RdaLogConfig>();
            foreach (string field in RdaLogConfig.StatusFields)
                _statusFields[field] = config.getStatusFieldValue(field);
            httpService = new HttpService(config.httpService, this);
            qsoFactory = new QSOFactory(this);
            qsoList = ProtoBufSerialization.Read<BindingList<QSO>>(qsoFilePath);
            if (qsoList == null)
                qsoList = new BindingList<QSO>();
            else if (qsoList.Count > 0)
            {
                QSO last = qsoList.Last();
                if (!string.IsNullOrEmpty(last.rda))
                    qsoFactory.no = last.no + 1;
            }
            _formMain = new FormMain(config.formMain, this);
            if (config.autoLogin)
                Task.Run(() => { httpService.login(); });
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

        public async Task newQso(string callsign, string myCallsign, decimal freq, string mode, string rstRcvd, string rstSnt)
        {
            QSO qso = qsoFactory.create(callsign, myCallsign, freq, mode, rstRcvd, rstSnt);
            qsoList.Insert(0, qso);
            ProtoBufSerialization.Write<BindingList<QSO>>(qsoFilePath, qsoList);
            await httpService.postQso(qso);
        }

        public async Task postFreq(decimal freq)
        {
            await httpService.postFreq(freq);
        }

        public void showSettings()
        {
            FormSettings formSettings = new FormSettings();

            formSettings.textBoxLogin.Text = config.httpService.callsign;
            formSettings.textBoxPassword.Text = config.httpService.password;
            formSettings.buttonLogin.Click += ButtonLogin_Click;

            formSettings.checkBoxAutoLogin.Checked = config.autoLogin;

            foreach (KeyValuePair<string,CheckBox> item in formSettings.mainFormPanelCheckboxes)
                item.Value.Checked = config.getMainFormPanelVisible(item.Key);

            for (int co = 0; co < formSettings.HotKeyBindings.Count; co++)
            {
                formSettings.HotKeyBindings[co].Item1.Text = config.hotKeys[co][0];
                formSettings.HotKeyBindings[co].Item2.Text = config.hotKeys[co][1];
            }

            if (formSettings.ShowDialog(this.formMain) == System.Windows.Forms.DialogResult.OK)
            {
                config.httpService.callsign = formSettings.textBoxLogin.Text;
                config.httpService.password = formSettings.textBoxPassword.Text;

                config.autoLogin = formSettings.checkBoxAutoLogin.Checked;

                foreach (KeyValuePair<string, CheckBox> item in formSettings.mainFormPanelCheckboxes)
                     config.setMainFormPanelVisible(item.Key, item.Value.Checked);

                config.enableMacros = formSettings.checkBoxEnableCwMacro.Checked;

                for (int co = 0; co < formSettings.HotKeyBindings.Count; co++)
                {
                    config.hotKeys[co][0] = formSettings.HotKeyBindings[co].Item1.Text;
                    config.hotKeys[co][1] = formSettings.HotKeyBindings[co].Item2.Text;
                }

                config.write();
            }
        }

        private async void ButtonLogin_Click(object sender, EventArgs e)
        {
            await httpService.login();
        }
    }
}
