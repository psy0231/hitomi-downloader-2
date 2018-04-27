/* Copyright (C) 2018. Hitomi Parser Developers */

using hitomi.Parser;
using Hitomi_Copy.Data;

namespace Hitomi_Copy_2
{
    public class HitomiCommon
    {
        public static HitomiArticle MetadataToArticle(HitomiMetadata metadata)
        {
            HitomiArticle article = new HitomiArticle();
            article.Artists = metadata.Artists;
            article.Characters = metadata.Characters;
            article.Groups = metadata.Groups;
            article.Language = metadata.Language;
            article.Magic = metadata.ID.ToString();
            article.Series = metadata.Parodies;
            article.Tags = metadata.Tags;
            article.Title = metadata.Name;
            article.Types = metadata.Type;
            return article;
        }

        public static string LegalizeTag(string tag)
        {
            if (tag.Trim().EndsWith("♀")) return "female:" + tag.Trim('♀').Trim();
            if (tag.Trim().EndsWith("♂")) return "male:" + tag.Trim('♂').Trim();
            return tag.Trim();
        }
    }
}
