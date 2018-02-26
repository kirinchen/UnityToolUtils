using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
namespace surfm.tool.i18n {
    public class I18nDB : MonoBehaviour {

        public enum Language {
            NONE, Taiwan, CN, English, ru, kr, es, pt, jp
        }

        [System.Serializable]
        public class Term {
            [TextArea(1, 10)]
            public string name;
            [TextArea(1, 10)]
            public string tw;
            [TextArea(1, 10)]
            public string cn;
            [TextArea(1, 10)]
            public string eng;
            [TextArea(1, 10)]
            public string ru;
            [TextArea(1, 10)]
            public string kr;
            [TextArea(1, 10)]
            public string es;
            [TextArea(1, 10)]
            public string pt;
            [TextArea(1, 10)]
            public string jp;

            public string get(Language l) {
                switch (l) {
                    case Language.Taiwan:
                        return tw;
                    case Language.CN:
                        return cn;
                    case Language.English:
                        return eng;
                    case Language.es:
                        return es;
                    case Language.pt:
                        return pt;
                    case Language.ru:
                        return ru;
                    case Language.kr:
                        return kr;
                    case Language.jp:
                        return jp;
                }
                return name;
            }

            public override string ToString() {
                string s = "";
                foreach (Language l in Enum.GetValues(typeof(Language))) {
                    s += l + ":" + get(l) + ", ";
                }
                return s;
            }
        }
        public Language defaultLanguage = Language.English;
        public string search;
        public bool sort;
        private Dictionary<string, Term> map = new Dictionary<string, Term>();
        public List<Term> terms = new List<Term>();


        public static string get(string cat, string key, Language l) {
            try {
                return getInstance(cat)._get(key, l);
            } catch (Exception e) {
                return string.Format("<cat:{0} key:{1} l:{2}>", cat, key, l);
            }
        }

        public string _get(string key, Language l) {
            if (map.Count <= 0) {
                injectMap();
            }
            if (!map.ContainsKey(key)) {
                Debug.Log("[i18n] Not find key=" + key);
                return key;
            }
            string ans = map[key].get(l);
            if (string.IsNullOrEmpty(ans)) {
                Debug.Log("[i18n] Not find Language=" + l + "  key=" + key);
                return l == defaultLanguage ? key : _get(key, defaultLanguage);
            } else {
                return ans;
            }
        }

        private void injectMap() {
            foreach (Term t in terms) {
                map.Add(t.name, t);
            }
        }

        public Dictionary<string, int> getSchema() {
            FieldInfo[] fs = typeof(Term).GetFields();
            Dictionary<string, int> ans = new Dictionary<string, int>();
            for (int i = 0; i < fs.Length; i++) {
                ans.Add(fs[i].Name, i);
            }
            return ans;
        }

        public List<string[]> listRows() {
            List<string[]> ans = new List<string[]>();
            FieldInfo[] fs = typeof(Term).GetFields();
            foreach (Term t in terms) {
                string[] data = getRowData(t, fs);
                ans.Add(data);
            }
            return ans;
        }

        private string[] getRowData(Term t, FieldInfo[] fs) {
            string[] ans = new string[fs.Length];
            for (int i = 0; i < fs.Length; i++) {
                string s = (string)fs[i].GetValue(t);
                ans[i] = s;
            }
            return ans;
        }

        private static Dictionary<string, I18nDB> cache = new Dictionary<string, I18nDB>();


        public static I18nDB getInstance(string category) {
            if (!cache.ContainsKey(category)) {
                cache.Add(category, Resources.Load<I18nDB>(category));
            }
            return cache[category];
        }


    }
}