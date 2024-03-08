using HamRadio;
using SerialDevice;
using SerialPortTester;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XmlConfigNS;
//using ExpertElectronics.Tci.Interfaces;
//using ExpertElectronics.Tci;

namespace tnxlog
{
    [DataContract]
    public class TransceiverControllerConfig : ConfigSection
    {
        [DataMember]
        public int transceiverType = 0;
        [DataMember]
        public string tciHost = "localhost";
        [DataMember]
        public uint tciPort = 40001;
        [DataMember]
        public uint tciTrnsNo = 0;
        [DataMember]
        public string serialDeviceId;
        [DataMember]
        public int[] pinout = new int[] { 0, 1 };
        [DataMember]
        public bool[] invertPins = new bool[] { false, false };
        [DataMember]
        public uint morseDelay = 100;

        public TransceiverControllerConfig() : base() { }
        public TransceiverControllerConfig(XmlConfig _parent) : base(_parent) { }

        internal void updateFromForm(FormSettings formSettings)
        {
            serialDeviceId = formSettings.serialDeviceId;
            for (int co = 0; co < TransceiverController.PIN_FUNCTIONS.Count; co++)
            {
                pinout[co] = SerialDevice.SerialDevice.PINS.IndexOf(formSettings.transceiverPinSettings[co].pin);
                invertPins[co] = formSettings.transceiverPinSettings[co].invert;
            }
        }

    }


    public class TransceiverController : IDisposable
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public static readonly List<string> PIN_FUNCTIONS = new List<string>() { "PTT", "CW" };

        internal readonly TransceiverControllerConfig config;
        private SerialPort serialPort;
        //private ITciClient tciClient;
        //private ITransceiverController tciTranceiverController;

        public bool connected { get { return serialPort != null; } }
        private volatile bool _busy;

        public bool busy { get { return _busy; } }

        private readonly object _sync = new object();
        private volatile uint _morseDelay;
        public uint morseDelay
        {
            get
            {
                lock (_sync) return _morseDelay;
            }
            set
            {
                _morseDelay = value;
                config.morseDelay = value;
                config.write();
            }
        }

        private Stopwatch sw = new Stopwatch();
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public TransceiverController(TransceiverControllerConfig _config)
        {
            config = _config;
            morseDelay = config.morseDelay;
            Logger.Debug($"Stopwatch frequency: {Stopwatch.Frequency}");
        }

        public static async Task AccurateAsyncDelay(int delay, CancellationToken ct)
        {
            Action a = () => { new System.Threading.ManualResetEventSlim(false).Wait(delay); };
            await Task.Factory.StartNew(a, ct);
        }


        public void Dispose()
        {
            serialPort?.Close();
            //tciClient?.DisConnectAsync();
        }

        public async void connect()
        {
            if (config.transceiverType == 1)
            {
                //tciClient?.DisConnectAsync();
                //tciClient = null;
                List<SerialDeviceInfo> devices = SerialDevice.SerialDevice.listSerialDevices();
                SerialDeviceInfo device = devices.FirstOrDefault(item => item.deviceID == config.serialDeviceId);
                if (device != null)
                {
                    string portName = device.portName;
                    serialPort = new SerialPort(portName);
                    try
                    {
                        SerialPortFixer.Execute(portName);
                        serialPort.Open();
                        initializePort();
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex, "Error opening port " + portName + " " + ex.ToString());
                    }
                }
                else
                    serialPort = null;
            } else if (config.transceiverType == 2)
            {
                serialPort?.Close();
                /*
                try
                {
                    tciClient = TciClient.Create(config.tciHost, config.tciPort, _cancellationTokenSource.Token);
                    await tciClient.ConnectAsync();
                    tciTranceiverController = tciClient.TransceiverController;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, $"Error opening TCI connection to {config.tciHost}:{config.tciPort} {ex.ToString()}");
                }
                */
            }
        }

        public void initializePort()
        {
            foreach (string pinFunction in PIN_FUNCTIONS)
                setPin(pinFunction, true);
        }

        private void delay(uint delayMs)
        {
            sw.Restart();
            while (delayMs > sw.ElapsedMilliseconds);
            sw.Stop();
        }

        public void morseString(string line, CancellationToken ct)
        {
            if (config.transceiverType == 1) {
                if (!_busy)
                {
                    try
                    {
                        _busy = true;
                        int cwPinNo = getPinNumber("CW");
                        PropertyInfo cwProp = getPortProp(cwPinNo);
                        bool cwOn = getInvert(cwPinNo) ? true : false;
                        setPin("PTT", false);
                        delay(morseDelay);
                        foreach (char c in line)
                        {
                            if (ct.IsCancellationRequested)
                                throw new TaskCanceledException();
                            if (c != ' ')
                            {
                                if (!MorseCode.Alphabet.ContainsKey(c))
                                    continue;
                                char[] code = MorseCode.Alphabet[c];
                                foreach (char mc in code)
                                {
                                    cwProp.SetValue(serialPort, cwOn);
                                    delay(mc == MorseCode.Dot ? morseDelay : 4 * morseDelay);
                                    cwProp.SetValue(serialPort, !cwOn);
                                    delay(morseDelay);
                                }
                                delay(2 * morseDelay);
                            }
                            else
                                delay(4 * morseDelay);
                        }
                    }
                    catch (TaskCanceledException) { }
                    catch (Exception ex)
                    {
                        Logger.Error(ex, "Morse string output error!");
                    }
                    finally
                    {
                        _busy = false;
                        initializePort();
                    }
                } /*else if (config.transceiverType == 2)
                {
                    tciTranceiverController.CwMacroSpeed = morseDelay;
                    tciTranceiverController.SetMacros(config.tciTrnsNo, line);
                }
                */
            }
        }

        public void stop()
        {
            if (config.transceiverType == 2)
            {
                //tciTranceiverController.SetCwMacrosStop();
            }
        }

        private PropertyInfo getPortProp(int no)
        {
            if (no != -1 && config.pinout[no] != -1)
            {
                string pin = SerialDevice.SerialDevice.PINS[config.pinout[no]];
                string propName = SerialDevice.SerialDevice.PIN_PROPS[pin];
                return serialPort.GetType().GetProperty(propName);
            }
            else
                return null;
        }

        private int getPinNumber(string pinFunction)
        {
            return PIN_FUNCTIONS.FindIndex(item => item == pinFunction);
        }

        private bool getInvert(int no)
        {
            if (no != -1 && config.pinout[no] != -1)
                return config.invertPins[no];
            else
                return false;
        }

        public void setPin(string pinFunction, bool value)
        {
            if (serialPort != null)
            {
                int no = getPinNumber(pinFunction);
                PropertyInfo prop = getPortProp(no);
                if (prop != null)
                    prop.SetValue(serialPort, getInvert(no) ? !value : value);
            }
        }

        public void disconnect()
        {
            serialPort?.Close();
            serialPort = null;
        }
    }
}
