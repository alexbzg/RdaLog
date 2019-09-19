using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XmlConfigNS;
using InvokeFormNS;
using System.Runtime.Serialization;

namespace StorableForm
{
    public class StorableForm : InvokeForm
    {
        public StorableFormConfig config;
        public virtual void writeConfig() {
            config.write();
        }
        public bool loaded = false;

        public void storeFormState()
        {
            Rectangle bounds = this.WindowState != FormWindowState.Normal ? this.RestoreBounds : this.DesktopBounds;
            config.formLocation = bounds.Location;
            config.formSize = bounds.Size;
        }

        public void restoreFormState()
        {
              if (config != null && config.formLocation != null && !config.formLocation.IsEmpty)
                  this.DesktopBounds =
                          new Rectangle(config.formLocation, config.formSize);
        }

        public StorableForm(StorableFormConfig _config)
        {
            config = _config;
            Load += FormWStorableState_Load;
            ResizeEnd += FormWStorableState_MoveResize;
            Move += FormWStorableState_MoveResize;
        }

        public StorableForm(string fname) : this(XmlConfig.create<StorableFormConfig>(fname)) { }
        public StorableForm() : this(XmlConfig.create<StorableFormConfig>()) { }

        private void FormWStorableState_MoveResize(object sender, EventArgs e)
        {
            if (loaded)
            {
                storeFormState();
                writeConfig();
            }
        }


        private void FormWStorableState_Load(object sender, EventArgs e)
        {
            restoreFormState();
            loaded = true;

        }
    }


    [DataContractAttribute]
    public class StorableFormConfig : XmlConfig
    {
        public System.Drawing.Point formLocation;
        public System.Drawing.Size formSize;
    }

}
