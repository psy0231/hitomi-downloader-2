/* Copyright (C) 2018. Hitomi Parser Developer */

using hitomi.Parser;
using Hitomi_Copy;
using Hitomi_Copy_2.GalleryInfo;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;

namespace Hitomi_Copy_2
{
    public class HitomiCore
    {
        public delegate void CallBack(PicElement pe);
        
        static public void DownloadAndSetImageLink(PicElement pe, CallBack callback)
        {
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            wc.DownloadStringCompleted += wc_dasil;
            wc.DownloadStringAsync(new Uri(HitomiDef.HitomiGalleryAddress + pe.Article.Magic + ".js"),
                new Tuple<PicElement, CallBack>(pe, callback));
        }
        static private void wc_dasil(object sender, DownloadStringCompletedEventArgs e)
        {
            Tuple<PicElement, CallBack> tuple = (Tuple<PicElement, CallBack>)e.UserState;

            try
            {
                tuple.Item1.Article.ImagesLink = HitomiGalleryInfo.GetImageLink(e.Result);
                lock (tuple.Item2)
                {
                    tuple.Item2(tuple.Item1);
                }
            }
            catch
            {

            }

        }
    }
}
