/* Copyright (C) 2018. Hitomi Parser Developer */

using HtmlAgilityPack;

namespace Pixiv_Downloader.PX
{
    public class PixivParser
    {
        public static string GetImagesAddress(string source)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(source);
            HtmlNode nodes = document.DocumentNode.SelectNodes("//div[@id='wrapper']")[0];

            HtmlNodeCollection items = nodes.SelectNodes(".//div[@class='layout-a']");

            return nodes.SelectSingleNode(".//div[@id='i3']//a//img").GetAttributeValue("src", "");
        }
    }
}
