/* Copyright (C) 2018. Hitomi Parser Developers */

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Hitomi_Copy
{
    class HitomiQueue
    {
        int capacity = 64;
        int mtx = 0;
        List<Tuple<string, string, object>> queue = new List<Tuple<string, string, object>>();

        public delegate void CallBack(string uri, string filename, object obj);
        CallBack callback;
        public delegate void DownloadSizeCallBack(string uri, UInt64 size);

        object int_lock = new object();
        object notify_lock = new object();

        public HitomiQueue(CallBack notify)
        {
            callback = notify;
        }
        
        public void DownloadRemoteImageFile(string uri, string fileName, object obj)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.Proxy = null;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if ((response.StatusCode == HttpStatusCode.OK ||
                    response.StatusCode == HttpStatusCode.Moved ||
                    response.StatusCode == HttpStatusCode.Redirect) &&
                    response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase))
                {
                    using (Stream inputStream = response.GetResponseStream())
                    using (Stream outputStream = File.OpenWrite(fileName))
                    {
                        byte[] buffer = new byte[65536];
                        int bytesRead;
                        do
                        {
                            bytesRead = inputStream.Read(buffer, 0, buffer.Length);
                            outputStream.Write(buffer, 0, bytesRead);
                        } while (bytesRead != 0);
                    }
                }

                lock (callback)
                {
                    callback(uri, fileName, obj);
                }

                lock (queue)
                {
                    int at = 0;
                    for (; at < queue.Count; at++)
                        if (queue[at].Item1 == uri && queue[at].Item2 == fileName)
                            break;
                    queue.RemoveAt(at);
                }

                lock (int_lock) mtx--;
                lock (notify_lock) Notify();
            }
            catch
            {
                // ㅗ
                Task.Run(() => DownloadRemoteImageFile(uri, fileName, obj));
            }
        }

        public Thread StartTheThread(string uri, string filename, object obj)
        {
            var t = new Thread(() => DownloadRemoteImageFile(uri, filename, obj));
            t.IsBackground = true;
            t.Start();
            return t;
        }

        public void Add(string uri, string filename, object obj)
        {
            queue.Add(new Tuple<string, string, object>(uri, filename,obj));
            ServicePointManager.DefaultConnectionLimit = 128;
            if (Wait())
                lock (notify_lock) Notify();
            //StartTheThread(uri, filename, obj);
        }

        private void Notify()
        {
            int i = mtx;
            if (queue.Count > i)
            {
                string s1 = queue[i].Item1;
                string s2 = queue[i].Item2;
                object s3 = queue[i].Item3;
                //StartTheThread(s1, s2, s3);
                Task.Run(() => DownloadRemoteImageFile(s1, s2, s3));
                lock (int_lock) mtx++;
            }
        }

        private bool Wait()
        {
            if (mtx == capacity)
                return false;
            return true;
        }
    }
}
