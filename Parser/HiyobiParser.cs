/* Copyright (C) 2018. Hitomi Parser Developer */

using HtmlAgilityPack;
using System.Collections.Generic;

namespace hitomi.Parser
{
    public class HiyobiParser
    {
        // Current Settings
        //---------
        //<main role="main" class="container">
        //<div class="gallery-content">
        //   <a target="_blank" href="/reader/1192959">
        //   <img src="https://aa.hiyobi.me/tn/1192959.jpg" />
        //   </a>	
        //   <span>
        //      <h5><a target="_blank" href="/reader/1192959">
        //         <b>Urara no Makura</b></a>
        //      </h5>
        //      <table>
        //         <tr>
        //            <td>작가 : </td>
        //            <td>
        //               <a target="_blank" href="/search/artist:amanagi_seiji">amanagi seiji</a> (<a target="_blank" href="/search/group:ins-mode">ins-mode</a>)
        //            </td>
        //         </tr>
        //         <tr>
        //            <td>원작 : </td>
        //            <td>N/A</td>
        //         </tr>
        //         <tr>
        //            <td>
        //               종류 : 
        //            </td>
        //            <td><a target='_blank' href='/search/type:doujinshi'>동인지</a></td>
        //         </tr>
        //         <tr>
        //            <td>
        //               태그 : 
        //            </td>
        //            <td><a target="_blank" data-original="female:anal" href="/search/female:anal">
        //               애널 ♀</a> <a target="_blank" data-original="female:lingerie" href="/search/female:lingerie">
        //               란제리 ♀</a> <a target="_blank" data-original="female:sex toys" href="/search/female:sex_toys">
        //               성기구 ♀</a> <a target="_blank" data-original="female:sole female" href="/search/female:sole_female">
        //               단독 여성 ♀</a> <a target="_blank" data-original="female:stockings" href="/search/female:stockings">
        //               스타킹 ♀</a>
        //            </td>
        //         </tr>
        //      </table>
        //   </span>
        //</div>
        //---------
        static public List<HitomiArticle> ParseInfo(string source)
        {
            List<HitomiArticle> result = new List<HitomiArticle>();
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(source);

            HtmlNodeCollection nodes = document.DocumentNode.SelectNodes("//div[@class='gallery-content']");

            if (nodes == null) return result;

            foreach(HtmlNode node in nodes)
            {
                HitomiArticle article = new HitomiArticle();
                article.Magic = node.SelectSingleNode(".//a[@target='_blank']").GetAttributeValue("href", "").Split('/')[2];
                article.Thumbnail = node.SelectSingleNode(".//img").GetAttributeValue("src", "");

                article.Title = node.SelectSingleNode(".//h5//b").InnerText;
                article.Artists = node.SelectSingleNode(".//a[contains(@href, '/search/artist:')]")?.InnerText;
                article.Groups = node.SelectSingleNode(".//a[contains(@href, '/search/group:')]")?.InnerText;

                article.Series = node.SelectNodes(".//tr")[1].SelectNodes(".//td")[1].InnerText;
                article.Types = node.SelectSingleNode(".//a[contains(@href, '/search/type:')]").InnerText;

                article.TagsKor = true;
                List<string> tags = new List<string>();
                HtmlNodeCollection tagsnode = node.SelectNodes(".//tr")[3].SelectNodes(".//a");
                if (tagsnode != null)
                    foreach (HtmlNode tag in tagsnode)
                        tags.Add(tag.InnerText + "|" + tag.GetAttributeValue("href", "").Split('/')[2]);
                result.Add(article);
            }

            return result;
        }

        static public List<string> ParseImageLink(string source)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(source);

            HtmlNodeCollection nodes = document.DocumentNode.SelectNodes("//div[@class='img-url']");

            List<string> result = new List<string>();
            foreach (HtmlNode node in nodes)
                result.Add(node.InnerText);
            return result;
        }
    }
}
