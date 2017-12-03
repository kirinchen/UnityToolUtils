using surfm.tool.i18n;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace surfm.tool {
    public class YesNoDialog : Dialog {
        public Text msgText;
        public Button yesButton, noButton;
        private Action<bool> onYesNoCallback;


        public override void Awake() {
            base.Awake();
            yesButton.onClick.AddListener(hide);
            noButton.onClick.AddListener(hide);
            yesButton.onClick.AddListener(onYesClick);
            yesButton.onClick.AddListener(onNoClick);
        }

        public void show(string t, Action<bool> callback) {
            msgText.text = t;
            onYesNoCallback = callback;
            show(true);
        }

        public void show(I18nKey k, Action<bool> callback) {
            string t = I18n.get(k);
            show(t, callback);
        }


        private void onNoClick() {
            onYesNoCallback(false);
        }

        private void onYesClick() {
            onYesNoCallback(true);
        }
    }
}
