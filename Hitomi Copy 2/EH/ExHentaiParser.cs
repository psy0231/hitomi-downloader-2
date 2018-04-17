/* Copyright (C) 2018. Hitomi Parser Developers */

using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Hitomi_Copy_2.EH
{
    class ExHentaiParser
    {
        public static string[] GetImagesUri(string source)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(source);
            HtmlNode nodes = document.DocumentNode.SelectNodes("//div[@id='gdt']")[0];

            List<string> uri = new List<string>();
            foreach (var div in nodes.SelectNodes(".//div"))
                try
                {
                    uri.Add(div.SelectSingleNode(".//a").GetAttributeValue("href", ""));
                }
                catch { }

            return uri.ToArray();
        }
        
        public static string GetImagesAddress(string source)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(source);
            HtmlNode nodes = document.DocumentNode.SelectNodes("//div[@id='i1']")[0];

            return nodes.SelectSingleNode(".//div[@id='i3']//a//img").GetAttributeValue("src", "");
        }
        
        public static string[] GetPagesUri(string source)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(source);
            HtmlNode nodes = document.DocumentNode.SelectNodes("//div[@class='gtb']")[0];

            List<string> uri = new List<string>();
            try
            {
                foreach (var div in nodes.SelectNodes(".//table//tr//td[contains(@onclick, 'document')]"))
                    try
                    {
                        uri.Add(div.SelectSingleNode(".//a").GetAttributeValue("href", ""));
                    }
                    catch { }
            }
            catch
            {
                uri.Add(nodes.SelectSingleNode(".//table//tr//td[@class='ptds']//a").GetAttributeValue("href", "") + "?p=0");
            }
            
            int max = 0;
            foreach (var page_c in uri)
            {
                int value;
                if (int.TryParse(Regex.Split(page_c, @"\?p\=")[1], out value))
                    if (max < value)
                        max = value;
            }
            
            if (uri.Count == 0) return null;

            List<string> result = new List<string>();
            string prefix = Regex.Split(uri[0], @"\?p\=")[0];
            for (int i = 0; i <= max; i++)
                result.Add(prefix + "?p=" + i.ToString());

            return result.ToArray();
        }
        
        public static ExHentaiArticle GetArticleData(string source)
        {
            ExHentaiArticle article = new ExHentaiArticle();

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(source);
            HtmlNode nodes = document.DocumentNode.SelectNodes("//div[@class='gm']")[0];

            article.Thumbnail = Regex.Match(nodes.SelectSingleNode(".//div[@id='gleft']//div//div").GetAttributeValue("style", ""), @"https://exhentai.org/.*?(?=\))").Groups[0].Value;

            article.Title = nodes.SelectSingleNode(".//div[@id='gd2']//h1[@id='gn']").InnerText;
            article.SubTitle = nodes.SelectSingleNode(".//div[@id='gd2']//h1[@id='gj']").InnerText;

            article.Type = nodes.SelectSingleNode(".//div[@id='gmid']//div//div[@id='gdc']//a//img").GetAttributeValue("alt", "");
            article.Uploader = nodes.SelectSingleNode(".//div[@id='gmid']//div//div[@id='gdn']//a").InnerText;

            HtmlNodeCollection nodes_static = nodes.SelectNodes(".//div[@id='gmid']//div//div[@id='gdd']//table//tr");

            article.Posted = nodes_static[0].SelectSingleNode(".//td[@class='gdt2']").InnerText;
            article.Parent = nodes_static[1].SelectSingleNode(".//td[@class='gdt2']").InnerText;
            article.Visible = nodes_static[2].SelectSingleNode(".//td[@class='gdt2']").InnerText;
            article.Language = nodes_static[3].SelectSingleNode(".//td[@class='gdt2']").InnerText.Split(' ')[0].ToLower();
            article.FileSize = nodes_static[4].SelectSingleNode(".//td[@class='gdt2']").InnerText;
            article.Length = int.Parse(nodes_static[5].SelectSingleNode(".//td[@class='gdt2']").InnerText.Split(' ')[0]);
            article.Favorited = int.Parse(nodes_static[6].SelectSingleNode(".//td[@class='gdt2']").InnerText.Split(' ')[0]);

            HtmlNodeCollection nodes_data = nodes.SelectNodes(".//div[@id='gmid']//div[@id='gd4']//table//tr");

            article.reclass = nodes_data[0].SelectNodes(".//td")[1].SelectSingleNode(".//div//a").InnerText;
            article.language = nodes_data[1].SelectNodes(".//td")[1].SelectNodes(".//div").Select(e => e.SelectSingleNode(".//a").InnerText).ToArray();
            article.group = nodes_data[2].SelectNodes(".//td")[1].SelectNodes(".//div").Select(e => e.SelectSingleNode(".//a").InnerText).ToArray();
            article.artist = nodes_data[3].SelectNodes(".//td")[1].SelectNodes(".//div").Select(e => e.SelectSingleNode(".//a").InnerText).ToArray();
            article.male = nodes_data[4].SelectNodes(".//td")[1].SelectNodes(".//div").Select(e => e.SelectSingleNode(".//a").InnerText).ToArray();
            article.female = nodes_data[5].SelectNodes(".//td")[1].SelectNodes(".//div").Select(e => e.SelectSingleNode(".//a").InnerText).ToArray();
            article.misc = nodes_data[6].SelectNodes(".//td")[1].SelectNodes(".//div").Select(e => e.SelectSingleNode(".//a").InnerText).ToArray();

            return article;
        }
    }
}
