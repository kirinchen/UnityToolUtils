using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using Newtonsoft.Json;

namespace surfm.tool.i18n {
    public class I18nDB : MonoBehaviour {

        [System.Serializable]
        public class TermLang {
            public SystemLanguage language;
            public string value;

            public TermLang() { }

            public TermLang(SystemLanguage systemLanguage) {
                language = systemLanguage;
            }
        }

        [System.Serializable]
        public class Term {
            public string name;
            public List<TermLang> termLangs = new List<TermLang>();

            public Term() { }
            public Term(string n) { name = n; }

            public TermLang getTermLang(SystemLanguage l) {
                return termLangs.Find(tl => tl.language == l);
            }

            public string get(SystemLanguage l,SystemLanguage dl) {
                TermLang tl = getTermLang(l);
                if (tl != null) return tl.value;
                return getTermLang(dl).value;
            }

            public override string ToString() {
                return CommUtils.toJson(termLangs);
            }
        }
        public string category { get; private set; }
        public SystemLanguage defaultLanguage = SystemLanguage.English;
        public bool recordLostKeys;
        public string search;
        public bool sort;
        private Dictionary<string, Term> map = new Dictionary<string, Term>();
        public List<Term> terms = new List<Term>();


        public static string get(string cat, string key, SystemLanguage l) {
            try {
                return getInstance(cat)._get(key, l);
            } catch (Exception e) {
                return string.Format("<cat:{0} key:{1} l:{2}>", cat, key, l);
            }
        }


        public List<string> loadLostKeys() {
            string ss= PlayerPrefs.GetString(category+"_I18nlostKeys","[]");
            return JsonConvert.DeserializeObject<List<string>>(ss);
        }

        private void appendLostKey(string key) {
            if (!recordLostKeys) return; 
            List<string> lkeys = loadLostKeys();
            if (lkeys.Contains(key)) return;
            lkeys.Add(key);
            saveLostKeys(lkeys);
        }

        public void saveLostKeys(List<string> lkeys) {
            if (!recordLostKeys) return;
            string ss = CommUtils.toJson(lkeys);
            PlayerPrefs.SetString(category + "_I18nlostKeys", ss);
        } 

        public string _get(string key, SystemLanguage l) {
            if (map.Count <= 0) {
                injectMap();
            }
            if (!map.ContainsKey(key)) {
               // Debug.Log("[i18n] Not find key=" + key);
                appendLostKey( key);
                return key;
            }
            string ans = map[key].get(l,defaultLanguage);
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
                I18nDB i18NDB = Resources.Load<I18nDB>(category);
                i18NDB.category = category;
                cache.Add(category, i18NDB);
            }
            return cache[category];
        }


    }
}