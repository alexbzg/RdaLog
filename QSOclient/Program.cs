using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tnxlog
{
    static class Program
    {
        private static Mutex _mutex;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 1 && args[0] == "INSTALLER") { Process.Start(Application.ExecutablePath); return; }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool mutexFl;
            _mutex = new Mutex(true, "TnxlogMutex", out mutexFl);
            if (mutexFl)
            {
                Tnxlog rdaLog = new Tnxlog();
                Application.Run(rdaLog.formMain);
            }
            else
                MessageBox.Show(Assembly.GetExecutingAssembly().GetName().Name + " is already running!", 
                    Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
