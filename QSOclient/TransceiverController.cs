using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
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

    }

    public class TransceiverController
    {
        public static readonly List<string> PIN_FUNCTIONS = new List<string>() { "PTT", "CW" };
    }
}
