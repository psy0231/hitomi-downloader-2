/* Copyright (C) 2018. Hitomi Parser Developers */

using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace Hitomi_403
{
    public class ExHentaiParser
    {
        /// <summary>
        /// 테이블 테스트 (썸네일 ㄴㄴ)
        /// </summary>
        /// <param name="source"></param>
        public static void TestArticle(string source)
        {
            
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(source);
            HtmlNode nodes = document.DocumentNode.SelectNodes("//div[@class='ido']")[0];

            //article.Thumbnail = nodes.SelectSingleNode(".//div//img").GetAttributeValue("src", "").Substring("//tn.hitomi.la/".Length);
            var articles = nodes.SelectNodes(".//div[contains(@style, 'position')]//table[@class='itg']")[0];
            string a1 = articles.SelectNodes(".//tr")[0].SelectNodes("//td")[2].SelectSingleNode("//div//div[@class='it5']").InnerText;
            var vv = articles.SelectNodes(".//tr");
        }

        public static string GetNextUri(string source)
        {
            return "";
        }

        /// <summary>
        /// 이미지 주소를 얻으려면 여기에 아티클 소스를 넣으세요 [이치하야 예제]
        /// ex: https://exhentai.org/g/1212168/421ef300a8/
        /// </summary>
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
                } catch { }

            return uri.ToArray();
        }

        /// <summary>
        /// 페이지 주소를 얻으려면 여기에 아티클 소스를 넣으세요
        /// ex: https://exhentai.org/g/1212168/421ef300a8/ [이치하야 예제]
        /// ex: https://exhentai.org/g/1201400/48f9b8e20a/ [85 페이지 예제]
        /// 
        /// </summary>
        public static string[] GetPagesUri(string source)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(source);
            HtmlNode nodes = document.DocumentNode.SelectNodes("//div[@class='gtb']")[0];

            List<string> uri = new List<string>();
            foreach (var div in nodes.SelectNodes(".//table//tr//td[contains(@onclick, 'document')]"))
                try
                {
                    uri.Add(div.SelectSingleNode(".//a").GetAttributeValue("href", ""));
                }
                catch { }

            return uri.Distinct().ToArray();
        }
    }
}
