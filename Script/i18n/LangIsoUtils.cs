using System.Globalization;
using UnityEngine;
namespace surfm.tool {

    public class LangIsoUtils {

        public static CultureInfo getByUnityLan(SystemLanguage lang) {
            switch (lang) {
                case SystemLanguage.Afrikaans: return CultureInfo.GetCultureInfo("AF");
                case SystemLanguage.Arabic: return CultureInfo.GetCultureInfo("AR");
                case SystemLanguage.Basque: return CultureInfo.GetCultureInfo("EU");
                case SystemLanguage.Belarusian: return CultureInfo.GetCultureInfo("BY");
                case SystemLanguage.Bulgarian: return CultureInfo.GetCultureInfo("BG");
                case SystemLanguage.Catalan: return CultureInfo.GetCultureInfo("CA");
                case SystemLanguage.Chinese: return CultureInfo.GetCultureInfo("ZH");
                case SystemLanguage.ChineseSimplified: return CultureInfo.GetCultureInfo("zh-Hans");
                case SystemLanguage.ChineseTraditional: return CultureInfo.GetCultureInfo("zh-Hant");
                case SystemLanguage.Czech: return CultureInfo.GetCultureInfo("CS");
                case SystemLanguage.Danish: return CultureInfo.GetCultureInfo("DA");
                case SystemLanguage.Dutch: return CultureInfo.GetCultureInfo("NL");
                case SystemLanguage.English: return CultureInfo.GetCultureInfo("EN");
                case SystemLanguage.Estonian: return CultureInfo.GetCultureInfo("ET");
                case SystemLanguage.Faroese: return CultureInfo.GetCultureInfo("FO");
                case SystemLanguage.Finnish: return CultureInfo.GetCultureInfo("FI");
                case SystemLanguage.French: return CultureInfo.GetCultureInfo("FR");
                case SystemLanguage.German: return CultureInfo.GetCultureInfo("DE");
                case SystemLanguage.Greek: return CultureInfo.GetCultureInfo("EL");
                case SystemLanguage.Hebrew: return CultureInfo.GetCultureInfo("IW");
                case SystemLanguage.Hungarian: return CultureInfo.GetCultureInfo("HU");
                case SystemLanguage.Icelandic: return CultureInfo.GetCultureInfo("IS");
                case SystemLanguage.Indonesian: return CultureInfo.GetCultureInfo("IN");
                case SystemLanguage.Italian: return CultureInfo.GetCultureInfo("IT");
                case SystemLanguage.Japanese: return CultureInfo.GetCultureInfo("JA");
                case SystemLanguage.Korean: return CultureInfo.GetCultureInfo("KO");
                case SystemLanguage.Latvian: return CultureInfo.GetCultureInfo("LV");
                case SystemLanguage.Lithuanian: return CultureInfo.GetCultureInfo("LT");
                case SystemLanguage.Norwegian: return CultureInfo.GetCultureInfo("NO");
                case SystemLanguage.Polish: return CultureInfo.GetCultureInfo("PL");
                case SystemLanguage.Portuguese: return CultureInfo.GetCultureInfo("PT");
                case SystemLanguage.Romanian: return CultureInfo.GetCultureInfo("RO");
                case SystemLanguage.Russian: return CultureInfo.GetCultureInfo("RU");
                case SystemLanguage.SerboCroatian: return CultureInfo.GetCultureInfo("SH");
                case SystemLanguage.Slovak: return CultureInfo.GetCultureInfo("SK");
                case SystemLanguage.Slovenian: return CultureInfo.GetCultureInfo("SL");
                case SystemLanguage.Spanish: return CultureInfo.GetCultureInfo("ES");
                case SystemLanguage.Swedish: return CultureInfo.GetCultureInfo("SV");
                case SystemLanguage.Thai: return CultureInfo.GetCultureInfo("TH");
                case SystemLanguage.Turkish: return CultureInfo.GetCultureInfo("TR");
                case SystemLanguage.Ukrainian: return CultureInfo.GetCultureInfo("UK");
                case SystemLanguage.Unknown: return CultureInfo.GetCultureInfo("EN");
                case SystemLanguage.Vietnamese: return CultureInfo.GetCultureInfo("VI");
            }
            throw new System.NullReferenceException("not impl this lang=" + lang);
        }
    }
}
