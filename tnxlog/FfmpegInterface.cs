using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FfmpegIinterfaceNS
{
    public class FfmpegInterface
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public class ExitEventArgs: EventArgs
        {
            public int code = 0;
        }

        public EventHandler<ExitEventArgs> Exited;
        public DataReceivedEventHandler DataReceived;
        private Process Process;
        private System.Threading.Timer timer;

        public static FfmpegInterface AudioRecorder(string ffmpegPath, string inputDevice, string additionalArgs, string outputFile)
        {
            string args = $" -f dshow -i audio=\"{inputDevice}\" {additionalArgs} {outputFile}";
            return new FfmpegInterface(ffmpegPath, args);
        }
        public FfmpegInterface(string ffmpegPath, string args)
        {
            Process = new Process();
            Logger.Debug($"{ffmpegPath} {args}");
            Process.StartInfo = new ProcessStartInfo(ffmpegPath)
            {
                Arguments = args,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            Process.EnableRaisingEvents = true;
            Process.OutputDataReceived += _DataReceived;
            Process.ErrorDataReceived += _DataReceived;
            Process.Exited += _Exited;
        }

        public void setTimer(int dueTime)
        {
            timer = new System.Threading.Timer(delegate { Stop(); }, null, dueTime, 0);
        }

        private void _Exited(object sender, EventArgs e)
        {
            Exited?.Invoke(this, new ExitEventArgs() { code = Process.ExitCode });
            Process = null;
        }

        public void Start()
        {
            if (Process != null) {
                Process.Start();
                Process.BeginOutputReadLine();
                Process.BeginErrorReadLine();
            }
        }

        public void Stop()
        {
            if (Process != null)
            {
                Process.StandardInput.Write('q');
            }
        }

        private void _DataReceived(object sender, DataReceivedEventArgs e)
        {
#if DEBUG
            Logger.Info(e.Data);
#endif            
            DataReceived?.Invoke(this, e);
        }

    }
}
