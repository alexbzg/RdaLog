using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace XmlConfigNS
{

    public class XmlConfig
    {
        private string fname;

        public static T create<T>() where T : XmlConfig
        {
            return create<T>(Application.StartupPath + "\\config.xml");
        }


        public static T create<T>(string fname) where T : XmlConfig
        {
            T result = null;
            if (File.Exists(fname) )
            {
                XmlSerializer ser = new XmlSerializer(typeof(T));
                using (FileStream fs = File.OpenRead(fname))
                {
                    try
                    {
                        result = (T)ser.Deserialize(fs);
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(e.ToString());
                    }
                }
            }
            return result;
        }

        public void write()
        {
            using (StreamWriter sw = new StreamWriter(fname))
            {
                try
                {
                    XmlSerializer ser = new XmlSerializer(this.GetType());
                    ser.Serialize(sw, this);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }
        }
    }

    public class ConfigSection
    {
        [IgnoreDataMember]
        XmlConfig parent;

        public void write()
        {
            parent.write();
        }
    }
}
