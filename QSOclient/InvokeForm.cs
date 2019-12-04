using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InvokeFormNS
{
    public class InvokeForm : Form
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public InvokeForm()
        {
        }

        public void DoInvoke(Action a)
        {
            if (InvokeRequired)
            {
                try
                {
                    Invoke(a);
                }
                catch (Exception e)
                {
                    logger.Error(e, "Invoke exception");
                }
            }
            else
                a();

        }
    }
}