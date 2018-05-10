/* Copyright (C) 2018. Hitomi Parser Developer */

using System.Linq;

namespace Robust_Hitomi_Copy_Machine
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("------------------------------------------------------------------------");
            System.Console.WriteLine("   Robust Hitomi Copy Machine");
            System.Console.WriteLine("   Copyright (c) 2018. Hitomi Downloader Developer.");
            System.Console.WriteLine("------------------------------------------------------------------------");

            if (args.Length > 0) Argument.settingArgument(args);


        }
    }
}
