/* Copyright (C) 2018. Hitomi Parser Developers */

namespace Hitomi_Copy_3
{
    interface ILanguage
    {
        string TabSearch();
        string TabDownload();
        string TabArtistRecommend();
        string TabSetting();

        string TabSearchButtonSearch();
        string TabSearchButtonTidy();
        string TabSearchButtonSelectAll();
        string TabSearchButtonDeSelectAll();
        string TabSearchButtonDownload();
        string TabSearchMessageSearched();

        string TabDownloadMessageStayedElement();
        string TabDownloadMessageDownloadState();
    }

    //public class KoreanLanguage : ILanguage
    //{
    //    string ILanguage.TabSearch() => "Tab";

    //}

    public class Test
    {
    }

}
