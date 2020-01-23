using System;
using System.Collections.Generic;
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
        public int pinout = 0;
        [DataMember]
        public bool invertDtr = false;
        [DataMember]
        public bool invertRts = false;

        public TransceiverControllerConfig() : base() { }
        public TransceiverControllerConfig(XmlConfig _parent) : base(_parent) { }

    }
    public class TransceiverController
    {
    }
}
