﻿using System;
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

        public XmlConfig(string _fname)
        {
            fname = _fname;
        }

        public XmlConfig()
        {
            fname = Application.StartupPath + "\\config.xml";
        }

        public static T create<T>() where T : XmlConfig, new()
        {
            return create<T>(Application.StartupPath + "\\config.xml");
        }


        public static T create<T>(string fname) where T : XmlConfig, new()
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
            if (result == null)
            {
                result = new T();
                result.fname = fname;
            }
            result.initialize();
            return result;
        }

        public virtual void initialize() { }
        public virtual void write()
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

    public class ConfigSection : XmlConfig
    {
        [IgnoreDataMember, XmlIgnore]
        public XmlConfig parent;

        public ConfigSection(XmlConfig _parent)
        {
            parent = _parent;
        }

        public ConfigSection() : base() { }

        public override void write()
        {
            if (parent == null)
                base.write();
            else
                parent.write();
        }
    }
}
