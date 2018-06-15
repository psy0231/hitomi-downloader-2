/* Copyright (C) 2018. Hitomi Parser Developer */

using System.Threading;

namespace Robust_Hitomi_Copy_Machine
{
    class Program
    {
        static bool complete_flag = false;

        static void Main(string[] args)
        {
            System.Console.WriteLine("------------------------------------------------------------------------");
            System.Console.WriteLine("   Robust Hitomi Copy Machine");
            System.Console.WriteLine("   Copyright (c) 2018. Hitomi Downloader Developer.");
            System.Console.WriteLine("------------------------------------------------------------------------");

            DriverManager.Instance.callback = callback;

            while (true)
            {
                System.Console.Write(">> ");
                int magic;
                if (int.TryParse(System.Console.ReadLine(), out magic))
                    HitomiCore.DownloadAndSetImageLink(magic.ToString());

                while (complete_flag == false)
                    Thread.Sleep(1000);
            }
        }

        static void callback()
        {
            complete_flag = true;
        }
    }
}
