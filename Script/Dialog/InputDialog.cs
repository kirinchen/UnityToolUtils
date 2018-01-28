using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace surfm.tool {
    public class InputDialog : Dialog {
        public InputField inputField;
        public Text titleText;
        public Button okButton;
        private Action<string> doneAction;

        new void Awake() {
            base.Awake();
            okButton.onClick.AddListener(onOK);
        }

        private void onOK() {
            doneAction(inputField.text);
            show(false);
        }

        public void show(string t, Action<string> callback) {
            doneAction = callback;
            titleText.text = t;
            show(true);
        }



    }
}
