/* Copyright (C) 2018. Hitomi Parser Developers */

using System;
using System.Collections.Generic;

namespace hitomi.Parser
{
    public class HitomiDef
    {
        /*
         * Hitomi Defines
         */
        public const string HitomiAddress = @"https://hitomi.la/";
        public const string HitomiGalleryAddress = @"https://hitomi.la/galleries/";
        public const string HitomiReaderAddress = @"https://hitomi.la/reader/";
        public const string HitomiThumbnail = @"https://tn.hitomi.la/";
        
        public static string GetDownloadImageAddress(
            string gallery,
            string page_with_extension)
        {
            // download.js
            var number_of_frontends = 2;
            char subdomain = Convert.ToChar(97 + (Convert.ToInt32(gallery) % number_of_frontends));
            return $"https://{subdomain}a.hitomi.la/galleries/{gallery}/{page_with_extension}";
        }

        private const string HitomiTagsPrefix = @"https://hitomi.la/alltags-";
        private const string HitomiArtistsPrefix = @"https://hitomi.la/allartists-";
        private const string HitomiSeriesPrefix = @"https://hitomi.la/allseries-";
        private const string HitomiCharactersPrefix = @"https://hitomi.la/allcharacters-";
        
        private static List<string> addSuffix(string starts, string ends = ".html")
        {
            List<string> result = new List<string>();
            result.Add($"{starts}123{ends}");
            for (char ch = 'a'; ch <= 'z'; ch++)
                result.Add($"{starts}{ch}{ends}");
            return result;
        }

        public static List<string> GetTagsAddress() { return addSuffix(HitomiTagsPrefix); }
        public static List<string> GetArtistsAddress() { return addSuffix(HitomiArtistsPrefix); }
        public static List<string> GetSeriesAddress() { return addSuffix(HitomiSeriesPrefix); }
        public static List<string> GetCharactersAddress() { return addSuffix(HitomiCharactersPrefix); }

        public const string HitomiTagsPage = @"<li><a href="".*?"">(.*?)</a>(.*?)</li>";

        /*
         * Nozomi Defines
         */
        public const string NozomiAddress = @"https://nozomi.la/";
    }
}
