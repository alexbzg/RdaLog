using HamRadio;
using SerialDevice;
using SerialPortTester;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XmlConfigNS;

namespace tnxlog
{
    [DataContract]
    public class TransceiverControllerConfig : ConfigSection
    {
        [DataMember]
        public string serialDeviceId;
        [DataMember]
        public int[] pinout = new int[] { 0, 1 };
        [DataMember]
        public bool[] invertPins = new bool[] { false, false };

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

        public bool connected { get { return serialPort != null; } }
        private volatile bool _busy;

        public bool busy { get { return _busy; } }

        public TransceiverController(TransceiverControllerConfig _config)
        {
            config = _config;
        }

        public static async Task AccurateAsyncDelay(int delay, CancellationToken ct)
        {
            //await Task.Delay(delay);

            Action a = () => { new System.Threading.ManualResetEventSlim(false).Wait(delay); };
            await Task.Factory.StartNew(a, ct);
        }


        public void Dispose()
        {
            serialPort.Close();
        }

        public void connect()
        {
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
                    Logger.Debug("Port Init");
                    initializePort();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.TraceInformation("Error opening port " + portName + " " + ex.ToString());
                }
            }
            else
                serialPort = null;
        }

        public void initializePort()
        {
            foreach (string pinFunction in PIN_FUNCTIONS)
                setPin(pinFunction, true);
        }

        public void morseString(string line, int speed, CancellationToken ct)
        {
            if (!_busy)
            {
                try {
                    _busy = true;
                    int cwPinNo = getPinNumber("CW");
                    PropertyInfo cwProp = getPortProp(cwPinNo);
                    bool cwOn = getInvert(cwPinNo) ? true : false;
                    setPin("PTT", false);
                    Thread.Sleep(speed);
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
                                Thread.Sleep(mc == MorseCode.Dot ? speed : 4 * speed);
                                cwProp.SetValue(serialPort, !cwOn);
                                Thread.Sleep(speed);
                            }
                            Thread.Sleep(2 * speed);
                        }
                        else
                            Thread.Sleep(4 * speed);
                    }
                }
                catch (TaskCanceledException) {}
                catch (Exception ex)
                {
                    Logger.Error(ex, "Morse string output error!");
                }
                finally
                {
                    _busy = false;
                    initializePort();
                }

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
