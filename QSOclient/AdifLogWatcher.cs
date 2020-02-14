using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tnxlog
{
    public class NewAdifEntryEventArgs : EventArgs
    {
        public string adif;
    }
    public class AdifLogWatcher
    {
        private FileSystemWatcher fsWatcher;
        private string filePath;

        private long lastLength = 0;

        public EventHandler<NewAdifEntryEventArgs> OnNewAdifEntry;

        internal AdifLogWatcher() { }

        internal void start(string _filePath)
        {
            filePath = _filePath;
            readFile(true);
            fsWatcher = new FileSystemWatcher();
            fsWatcher.Filter = filePath.Substring(filePath.LastIndexOf('\\') + 1);
            fsWatcher.Path = filePath.Substring(0, filePath.Length - fsWatcher.Filter.Length);
            fsWatcher.NotifyFilter = NotifyFilters.LastWrite;
            fsWatcher.Changed += FsWatcher_Changed;
            fsWatcher.EnableRaisingEvents = true;
        }

        private async void FsWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            string[] newEntries = await readFile();
            foreach (string entry in newEntries)
                OnNewAdifEntry?.Invoke(this, new NewAdifEntryEventArgs { adif = entry });
        }

        private async Task<string[]> readFile(bool discardEntries = false)
        {
            int retryCount = 0;
            while (retryCount < 3)
            try
            {
                using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    if (lastLength != 0 && lastLength < stream.Length)
                        stream.Position = lastLength;
                    lastLength = stream.Length;
                    if (discardEntries)
                        return new string[] { };
                    else
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            string data = reader.ReadToEnd();
                            data = data.ToUpper().Replace("\r", "").Replace("\n", "");
                            if (data.Contains("<EOH>"))
                                data = data.Split(new string[] { "<EOH>" }, StringSplitOptions.RemoveEmptyEntries)[1];
                            string[] entries = data.Split(new string[] { "<EOR>" }, StringSplitOptions.RemoveEmptyEntries);
                            return entries;
                        }
                }
            }
            catch (IOException) {
                await Task.Delay(100);
                retryCount++;
            }
            return new string[] { };
        }

        internal void stop()
        {
            if (fsWatcher != null)
            {
                fsWatcher.EnableRaisingEvents = false;
                fsWatcher = null;
            }
        }
    }
}
