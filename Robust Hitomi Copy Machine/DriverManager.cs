/* Copyright (C) 2018. Hitomi Parser Developer */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Robust_Hitomi_Copy_Machine
{
    public class DriverManager
    {
        private static readonly Lazy<DriverManager> instance = new Lazy<DriverManager>(() => new DriverManager());
        public static DriverManager Instance => instance.Value;

        public int capacity = 12;
        public int mutex = 0;

        public List<Tuple<string, string>> queue = new List<Tuple<string, string>>();

        object int_lock = new object();
        object notify_lock = new object();

        public delegate void ClearCallback();
        public ClearCallback callback;

        private static Process CreateProcess(string path, string url)
        {
            ProcessStartInfo processInfo;
            Process process;
            processInfo = new ProcessStartInfo("driver.exe", $"\"{path}\" \"{url}\"");
            processInfo.CreateNoWindow = false;
            processInfo.UseShellExecute = false;
            process = Process.Start(processInfo);
            Console.WriteLine($"driver.exe \"{path}\" \"{url}\"");
            return process;
        }

        private void AddArticle(string fileName, string uri)
        {
        RETRY:
            Process process = CreateProcess(fileName, uri);
            process.WaitForExit();
            int exitCode = process.ExitCode;
            process.Close();
            Console.WriteLine($"process terminated {fileName} {uri}");

            if (exitCode == 6974)
            {
                Console.WriteLine($" retrying... {fileName} {uri}");
                Thread.Sleep(1000);
                goto RETRY;
            }

            lock (queue)
            {
                int at = 0;
                for (; at < queue.Count; at++) if (queue[at].Item1 == fileName && queue[at].Item2 == uri) break;
                if (at != queue.Count) queue.RemoveAt(at);
            }

            lock (int_lock) mutex--;
            lock (notify_lock) Notify();
        }

        public void Add(string filename, string uri)
        {
            queue.Add(new Tuple<string, string>(filename, uri));
            if (Wait())
                lock (notify_lock) Notify();
        }

        private void Notify()
        {
            int i = mutex;
            if (queue.Count > i)
            {
                string path = queue[i].Item1;
                string uri = queue[i].Item2;
                Task.Run(() => AddArticle(path, uri));
                lock (int_lock) mutex++;
            }
            else if (queue.Count == 0)
                callback();
        }

        private bool Wait()
        {
            if (mutex == capacity)
                return false;
            return true;
        }
    }
}
