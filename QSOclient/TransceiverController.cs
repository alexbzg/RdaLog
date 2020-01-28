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

    public class TransceiverController
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public static readonly List<string> PIN_FUNCTIONS = new List<string>() { "PTT", "CW" };

        internal readonly TransceiverControllerConfig config;
        private SerialPort serialPort; 

        public bool connected { get { return serialPort != null; } }

        public TransceiverController(TransceiverControllerConfig _config)
        {
            config = _config;
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

        public void setPin(string pinFunction, bool value)
        {
            if (serialPort != null)
            {
                int no = PIN_FUNCTIONS.FindIndex(item => item == pinFunction);
                if (no != -1 && config.pinout[no] != -1)
                {
                    string pin = SerialDevice.SerialDevice.PINS[config.pinout[no]];
                    string propName = SerialDevice.SerialDevice.PIN_PROPS[pin];
                    PropertyInfo prop = serialPort.GetType().GetProperty(propName);
                    bool cValue = config.invertPins[no] ? !value : value;
                    Logger.Debug($"{propName}: {cValue}");
                    prop.SetValue(serialPort, cValue);
                }
            }
        }

        public void disconnect()
        {
            serialPort?.Close();
            serialPort = null;
        }
    }
}
