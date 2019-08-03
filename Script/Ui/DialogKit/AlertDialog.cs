using System;
using UnityEngine;
using UnityEngine.UI;

namespace surfm.tool {

    public class AlertDialog : DialogKit.Dialog {


        private Action onConfirmAction;
        public Text text;

        public void onConfirm() {
            onConfirmAction?.Invoke();
            close();
        }

        public void init(string msg, Action onC = null) {
            text.text = msg;
            onConfirmAction = onC;
        }


    }
}
