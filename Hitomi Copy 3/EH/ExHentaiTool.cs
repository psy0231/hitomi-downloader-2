/* Copyright (C) 2018. Hitomi Parser Developers */

using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Hitomi_Copy_3.EH
{
    public class ExHentaiTool
    {
        public static string GetAddressFromMagicTitle(string magic, string title)
        {
            string search_url = $"https://exhentai.org/?f_search={title}&page=0";
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            wc.Headers.Add(HttpRequestHeader.Cookie, "igneous=fc251d23e;ipb_member_id=1904662;ipb_pass_hash=ff8940e2cc632d601091b8836fca66f5;");
            string html = wc.DownloadString(search_url);
            if (html.Contains($"/{magic}/"))
                return Regex.Match(html, $"(https://exhentai.org/g/{magic}/\\w+/)").Value;
            return "";
        }
    }
}
