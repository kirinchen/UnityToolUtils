using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace surfm.tool {
    public class InputTextSwitch : MonoBehaviour {

        public TriggerEvent.BoolEvent uninputedEvent;
        public Text text;
        public InputField inputField;
        public bool inputed { get; private set; }
        private Action<string> onChange;


        void Awake() {
            inputField.onEndEdit.AddListener(onEndEdit);
        }

        void Start() {
            refleshEnable();
        }

        public void set(string txt,bool inped , Action<string> onC) {
            onChange = onC;
            text.text = inputField.text = txt;
            setInputed(inped);
        }

        private void onEndEdit(string arg0) {
            setInputed(false);
        }

        public void setInputed(bool b) {
            if (inputed == b) return;
            inputed = b;
            refleshEnable();
            uninputedEvent.Invoke(!inputed);
            if (!inputed) {
                text.text = inputField.text;
                onChange(text.text);
            }
        }

        private void refleshEnable() {
            inputField.gameObject.SetActive(  inputed);
            text.gameObject.SetActive( !inputed);
        }

        public void toggle() {
            setInputed(!inputed);
        }

    }
}
