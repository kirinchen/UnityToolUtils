using System;
using UnityEngine;


namespace surfm.tool.i18n {
    public class I18n : MonoBehaviour {

        public static readonly string KEY_I18N = "KEY_I18N";
        public static I18n instance = null;
        internal I18nDB.Language language
        {
            get; private set;
        }

        public string category = "I18nDB";

        void Awake() {
            if (instance == null) {
                instance = this;
                setupLanguage();
            }
        }


        internal void setupLanguage() {
            language = getI18nName();
        }

        private I18nDB.Language getI18nName() {
#if !UNITY_WEBGL
            I18nDB.Language ans = loadAssignLanguage();
            if (ans != I18nDB.Language.NONE) {
                return ans;
            }
            switch (Application.systemLanguage) {
                case SystemLanguage.English:
                    return I18nDB.Language.English;
                case SystemLanguage.ChineseTraditional:
                case SystemLanguage.Chinese:
                    return I18nDB.Language.Taiwan;
                case SystemLanguage.ChineseSimplified:
                    return I18nDB.Language.CN;
                case SystemLanguage.Russian:
                    return I18nDB.Language.ru;
                case SystemLanguage.Korean:
                    return I18nDB.Language.kr;
                case SystemLanguage.Portuguese:
                    return I18nDB.Language.pt;
                case SystemLanguage.Spanish:
                    return I18nDB.Language.es;
            }
#endif
            return I18nDB.Language.English;

        }

        private I18nDB.Language loadAssignLanguage() {
            try {
                string ui = PlayerPrefs.GetString(KEY_I18N);
                I18nDB.Language lg = (I18nDB.Language)Enum.Parse(typeof(I18nDB.Language), ui);
                return lg;
            } catch (Exception e) {
                return I18nDB.Language.NONE;
            }
        }

        public void saveAssignLanguage(I18nDB.Language lg) {
            PlayerPrefs.SetString(KEY_I18N, lg.ToString());
        }

        void OnDestroy() {
            instance = null;
        }

        public string _get(I18nKey k) {
            if (string.IsNullOrEmpty(k.category)) {
                return _get(k.key);
            } else {
                return _get(k.category, k.key);
            }
        }

        public string _get(string key) {
            return _get(category, key);
        }

        public string _get(string cat, string key) {
            return I18nDB.get(cat, key, language);
        }

        public static string get(string key) {
            return getInstance()._get(key);
        }

        public static string get(I18nKey key) {
            return getInstance()._get(key);
        }

        public static string get(string cat, string key) {
            return getInstance()._get(cat, key);
        }

        public static I18n getInstance() {
            initLanguageIniter();
            return instance;
        }

        private static void initLanguageIniter() {
            if (instance == null) {
                instance = FindObjectOfType<I18n>();
                instance.setupLanguage();
            }
        }

    }
}
