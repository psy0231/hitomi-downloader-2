/* Copyright (C) 2018. Hitomi Parser Developers */

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MM_Downloader.MM
{
    class MMParser
    {
        /// <summary>
        /// ex: https://marumaru.in/b/manga/241501
        /// </summary>
        /// <param name="html"></param>
        public static List<string> ParseManga(string html)
        {
            Regex regex = new Regex("(http://wasabisyrup.com/archives/.*?)\\\"");
            Match match = regex.Match(html);
            List<string> result = new List<string>();

            while (match.Success)
            {
                result.Add(match.Groups[1].Value);
                match = match.NextMatch();
            }

            return result;
        }

        /// <summary>
        /// ex: http://wasabisyrup.com/archives/xnc4I45ZMRI
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static List<string> ParseArchives(string html)
        {
            Regex regex = new Regex("(/storage/gallery/.*?)\\\"");
            Match match = regex.Match(html);
            List<string> result = new List<string>();

            while (match.Success)
            {
                result.Add(match.Groups[1].Value);
                match = match.NextMatch();
            }

            return result;
        }
    }
}
