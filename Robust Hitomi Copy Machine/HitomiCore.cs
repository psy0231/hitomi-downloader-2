﻿
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;


namespace Robust_Hitomi_Copy_Machine
{
    public class HitomiCore
    {
        static public void DownloadAndSetImageLink(string magic)
        {
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            string json = wc.DownloadString(new Uri(HitomiDef.HitomiGalleryAddress + magic + ".js"));
            
            Directory.CreateDirectory(MakeDownloadDirectory(magic));
            var list = HitomiGalleryInfo.GetImageLink(json);
            foreach (var i in list)
                DriverManager.Instance.Add(Path.Combine(MakeDownloadDirectory(magic), i), HitomiDef.GetDownloadImageAddress(magic, i));
        }
        
        private static string MakeDownloadDirectory(string id)
        {
            return "C:\\Hitomi\\" + id + "\\";
        }
    }
}
