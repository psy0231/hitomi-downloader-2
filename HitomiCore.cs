/* Copyright (C) 2018. Hitomi Parser Developer */

using hitomi.Parser;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;

namespace Hitomi_Copy
{
    public class HitomiCore
    {
        public delegate void CallBack(PicElement pe);

        /*
         *   1. DownloadAndSetImageLink
         *   2. DownloadArticle
         */
        static public void DownloadAndSetImageLink(PicElement pe, CallBack callback)
        {
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            wc.DownloadStringCompleted += wc_dasil;
            wc.DownloadStringAsync(new Uri(HitomiDef.HitomiGalleryAddress + pe.Article.Magic + ".html"), 
                new Tuple<PicElement, CallBack> (pe, callback));
        }
        static private void wc_dasil(object sender, DownloadStringCompletedEventArgs e)
        {
            Tuple<PicElement, CallBack> tuple = (Tuple<PicElement, CallBack>)e.UserState;
            tuple.Item1.Article.ImagesLink = HitomiParser.ParsePage(e.Result);

            lock (tuple.Item2)
            {
                tuple.Item2(tuple.Item1);
            }
        }

        public delegate void CallBack2(HitomiArticle v, PicElement pe, int i);
        static public void DownloadArticle(HitomiArticle ha, string folder, CallBack2 callback, PicElement pe, int v1, int v2)
        {
            Directory.CreateDirectory(folder);
            for (int i = v1; i < v2; i++)
            {
                WebClient wc = new WebClient();
                wc.Headers["Accept-Encoding"] = "application/x-gzip";
                wc.Encoding = Encoding.UTF8;
                wc.DownloadFileCompleted += wc_da;
                if (ha.TagsKor == false)
                    wc.DownloadFileAsync(new Uri(HitomiDef.GetDownloadImageAddress(ha.Magic, ha.ImagesLink[i])),
                        Path.Combine(folder, ha.ImagesLink[i]),
                        new Tuple<HitomiArticle, int, CallBack2, PicElement>(ha, i, callback, pe));
            }
        }
        static private void wc_da(object sender, AsyncCompletedEventArgs e)
        {
            var tuple = (Tuple<HitomiArticle, int, CallBack2, PicElement>)e.UserState;
            lock (tuple.Item3)
            {
                tuple.Item3(tuple.Item1, tuple.Item4, tuple.Item2);
            }
        }

        static public string DownloadThumbnail(HitomiArticle ha, string prefix = "")
        {
            string temp = Path.GetTempFileName();
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            wc.DownloadFile(prefix + ha.Thumbnail, temp);
            return temp;
        }
    }
}
