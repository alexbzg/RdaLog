﻿using System;
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
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

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

        public static string[] adifEntries(FileStream stream)
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                string data = reader.ReadToEnd();
                data = data.ToUpper().Replace("\r", "").Replace("\n", "");
                if (data.Contains("<EOH>"))
                {
                    string[] dataParts = data.Split(new string[] { "<EOH>" }, StringSplitOptions.RemoveEmptyEntries);
                    if (dataParts.Length > 1)
                        data = dataParts[1];
                    else
                        data = "";
                }
                string[] r = data.Split(new string[] { "<EOR>" }, StringSplitOptions.RemoveEmptyEntries);
                return r;
            }
        }

        private async Task<string[]> readFile(bool discardEntries = false)
        {
            int retryCount = 0;
            while (retryCount < 3)
            try
            {
                using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    long curLength = stream.Length;
                    if (curLength == 0)
                        return new string[] { }; 
                    if (lastLength != 0 && lastLength <= curLength)
                        stream.Position = lastLength;
                    lastLength = curLength;

                    if (discardEntries)
                        return new string[] { };
                    else
                        return adifEntries(stream);
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
