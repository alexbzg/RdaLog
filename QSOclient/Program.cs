using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RdaLog
{
    static class Program
    {
        private static Mutex _mutex;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool mutexFl;
            _mutex = new Mutex(true, "MyApplicationMutex", out mutexFl);
            if (mutexFl)
            {
                RdaLog rdaLog = new RdaLog();
                Application.Run(rdaLog.formMain);
            }
            else
                MessageBox.Show("RDA Log is already running!", "RDA Log", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
