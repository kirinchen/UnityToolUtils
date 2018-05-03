using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace surfm.tool {
    public class InputFieldBG : InputField {


        public new string text
        {
            get
            {
                return base.text;
            }
            set
            {
                base.text = value;
                if (!isFocused) {
                    reflesh();
                }
            }
        }

        new public void Start() {
            base.Start();
            reflesh();
            onEndEdit.AddListener(onEnd);

        }

        private void reflesh() {
            targetGraphic.enabled = string.IsNullOrEmpty(text);
        }

        public override void OnSelect(BaseEventData eventData) {
            base.OnSelect(eventData);
            targetGraphic.enabled = true;
        }


        private void onEnd(string s) {
            reflesh();
        }



    }
}