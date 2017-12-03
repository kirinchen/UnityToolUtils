using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Text;
namespace surfm.tool.i18n {
    public class I18nText : MonoBehaviour {

        public string category;
        public string i18nKey;
        private Text self;
        private string showText;

        void Awake() {
            self = GetComponent<Text>();
        }

        void Start() {
            setText(i18nKey);
        }

        internal void setText(string key) {
            i18nKey = key;
            if (i18nKey != null && i18nKey.Length > 0) {
                self.text = get(i18nKey);
            } else {
                self.text = get(self.text);
            }
            bool orgEnabled = self.enabled;
            self.enabled = false;
            self.enabled = true;
            self.enabled = orgEnabled;
        }

        public string get(string k) {
            return isCategorySet() ? I18n.get(category, k) : I18n.get( k);
        }

        private bool isCategorySet() {
            return !string.IsNullOrEmpty(category);
        }

    }
}
