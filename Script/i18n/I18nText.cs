using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Text;
namespace surfm.tool.i18n {
    public class I18nText : MonoBehaviour {

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
                self.text = I18n.get(i18nKey);
            } else {
                self.text = I18n.get(self.text);
            }
            bool orgEnabled = self.enabled;
            self.enabled = false;
            self.enabled = true;
            self.enabled = orgEnabled;

        }

    }
}
