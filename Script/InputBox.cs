using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace surfm.tool {
    public class InputBox : Box {

        public InputField inputText;
        private Bundle current;


        public override void show(BoxBundle b) {
            current = (Bundle)b;
            base.show(b);
        }


        public void onConfirm() {
            close();
            current.doneAction(inputText.text);
        }


        public class Bundle : BoxBundle {
            public Action<string> doneAction;
        }

    }
}
