using System.Globalization;
using UnityEngine;
namespace surfm.tool {

    public class LangIsoUtils {
        private static Map<SystemLanguage, CultureInfo> map = new Map<SystemLanguage, CultureInfo>();

        private static void init() {
            if (map.Count > 0) return;
                map.Add( SystemLanguage.Afrikaans, CultureInfo.GetCultureInfo("AF"));
                map.Add( SystemLanguage.Arabic, CultureInfo.GetCultureInfo("AR"));
                map.Add( SystemLanguage.Basque, CultureInfo.GetCultureInfo("EU"));
                map.Add( SystemLanguage.Belarusian, CultureInfo.GetCultureInfo("BE"));
                map.Add( SystemLanguage.Bulgarian, CultureInfo.GetCultureInfo("BG"));
                map.Add( SystemLanguage.Catalan, CultureInfo.GetCultureInfo("CA"));
                map.Add( SystemLanguage.Chinese, CultureInfo.GetCultureInfo("ZH"));
                map.Add( SystemLanguage.ChineseSimplified, CultureInfo.GetCultureInfo("zh-Hans"));
                map.Add( SystemLanguage.ChineseTraditional, CultureInfo.GetCultureInfo("zh-Hant"));
                map.Add( SystemLanguage.Czech, CultureInfo.GetCultureInfo("CS"));
                map.Add( SystemLanguage.Danish, CultureInfo.GetCultureInfo("DA"));
                map.Add( SystemLanguage.Dutch, CultureInfo.GetCultureInfo("NL"));
                map.Add( SystemLanguage.English, CultureInfo.GetCultureInfo("EN"));
                map.Add( SystemLanguage.Estonian, CultureInfo.GetCultureInfo("ET"));
                map.Add( SystemLanguage.Faroese, CultureInfo.GetCultureInfo("FO"));
                map.Add( SystemLanguage.Finnish, CultureInfo.GetCultureInfo("FI"));
                map.Add( SystemLanguage.French, CultureInfo.GetCultureInfo("FR"));
                map.Add( SystemLanguage.German, CultureInfo.GetCultureInfo("DE"));
                map.Add( SystemLanguage.Greek, CultureInfo.GetCultureInfo("EL"));
                map.Add( SystemLanguage.Hebrew, CultureInfo.GetCultureInfo("HE"));
                map.Add( SystemLanguage.Hungarian, CultureInfo.GetCultureInfo("HU"));
                map.Add( SystemLanguage.Icelandic, CultureInfo.GetCultureInfo("IS"));
                map.Add( SystemLanguage.Indonesian, CultureInfo.GetCultureInfo("ID"));
                map.Add( SystemLanguage.Italian, CultureInfo.GetCultureInfo("IT"));
                map.Add( SystemLanguage.Japanese, CultureInfo.GetCultureInfo("JA"));
                map.Add( SystemLanguage.Korean, CultureInfo.GetCultureInfo("KO"));
                map.Add( SystemLanguage.Latvian, CultureInfo.GetCultureInfo("LV"));
                map.Add( SystemLanguage.Lithuanian, CultureInfo.GetCultureInfo("LT"));
                map.Add( SystemLanguage.Norwegian, CultureInfo.GetCultureInfo("NO"));
                map.Add( SystemLanguage.Polish, CultureInfo.GetCultureInfo("PL"));
                map.Add( SystemLanguage.Portuguese, CultureInfo.GetCultureInfo("PT"));
                map.Add( SystemLanguage.Romanian, CultureInfo.GetCultureInfo("RO"));
                map.Add( SystemLanguage.Russian, CultureInfo.GetCultureInfo("RU"));
                map.Add( SystemLanguage.Slovak, CultureInfo.GetCultureInfo("SK"));
                map.Add( SystemLanguage.Slovenian, CultureInfo.GetCultureInfo("SL"));
                map.Add( SystemLanguage.Spanish, CultureInfo.GetCultureInfo("ES"));
                map.Add( SystemLanguage.Swedish, CultureInfo.GetCultureInfo("SV"));
                map.Add( SystemLanguage.Thai, CultureInfo.GetCultureInfo("TH"));
                map.Add( SystemLanguage.Turkish, CultureInfo.GetCultureInfo("TR"));
                map.Add( SystemLanguage.Ukrainian, CultureInfo.GetCultureInfo("UK"));
                map.Add( SystemLanguage.Unknown, CultureInfo.GetCultureInfo("EN"));
                map.Add( SystemLanguage.Vietnamese, CultureInfo.GetCultureInfo("VI"));
        }




        public static SystemLanguage getByCulture(CultureInfo c) {
            init();
            if (map.ContainsValue(c)) return map.listKeysByValue(c)[0];
            throw new System.NullReferenceException("not impl this lang=" + c);
        }

        public static CultureInfo getByUnityLan(SystemLanguage lang) {
            init();
            if (map.ContainsKey(lang)) return map[lang];
            throw new System.NullReferenceException("not impl this lang=" + lang);
        }
    }
}
