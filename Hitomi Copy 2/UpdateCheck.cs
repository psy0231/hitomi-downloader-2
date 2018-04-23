/* Copyright (C) 2018. Hitomi Parser Developers */

using HtmlAgilityPack;
using System.Net;
using System.Text;

namespace Hitomi_Copy_2
{
    public class UpdateCheck
    {
        public const string GitStatusSite = "https://github.com/dc-koromo/hitomi-downloader-2/releases";

        public static string GetLatestVersion()
        {
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            string html = wc.DownloadString(GitStatusSite);

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            HtmlNode nodes = document.DocumentNode.SelectNodes("//div[@class='application-main ']")[0];
            
            return "";
        }
    }
}
