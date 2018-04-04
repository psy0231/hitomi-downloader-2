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
    }
}
