/* Copyright (C) 2018. Hitomi Parser Developer */

using System;
using System.IO;
using System.Net;
using System.Threading;

namespace driver
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("driver.exe [path] [url]");
                return;
            }
            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(args[1]);
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/66.0.3359.139 Safari/537.36";

            request.Timeout = Timeout.Infinite;
            request.KeepAlive = true;
            
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if ((response.StatusCode == HttpStatusCode.OK ||
                    response.StatusCode == HttpStatusCode.Moved ||
                    response.StatusCode == HttpStatusCode.Redirect) &&
                    response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase))
                {
                    using (Stream inputStream = response.GetResponseStream())
                    using (Stream outputStream = File.OpenWrite(args[0]))
                    {
                        byte[] buffer = new byte[131072];
                        int bytesRead;
                        do
                        {
                            bytesRead = inputStream.Read(buffer, 0, buffer.Length);
                            outputStream.Write(buffer, 0, bytesRead);
                        } while (bytesRead != 0);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error occured! {e.Message}");
                Console.ReadKey();
                Environment.Exit(6974);
            }
        }
    }
}
