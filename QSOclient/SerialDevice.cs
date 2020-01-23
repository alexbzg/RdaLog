using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SerialDevice
{
    public class SerialDeviceInfo
    {
        public string portName;
        public string caption;
        public string deviceID;
    }

    class SerialDevice
    {
        private static Regex portRE = new Regex(@"(?<=\()COM\d+(?=\))");


        public static List<SerialDeviceInfo> listSerialDevices()
        {
            List<SerialDeviceInfo> r = new List<SerialDeviceInfo>();
            using (var searcher = new ManagementObjectSearcher
                ("root\\CIMV2",
                    "SELECT * FROM Win32_PnPEntity WHERE ClassGuid=\"{4d36e978-e325-11ce-bfc1-08002be10318}\""))
            {
                var ports = searcher.Get().Cast<ManagementBaseObject>().ToList();
                foreach (var p in ports)
                {
                    string caption = p["Caption"].ToString();
                    if (caption.Contains("COM"))
                    {
                        Match m = portRE.Match(caption);
                        if (m.Success)
                            r.Add(new SerialDeviceInfo
                            {
                                caption = caption,
                                deviceID = p["DeviceID"].ToString(),
                                portName = m.Value
                            });
                    }
                }
            }
            return r;
        }

    }
}
