#if DOTween
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace surfm.tool {
    public class Toast : DialogKit.Dialog {

        public Text text { get; private set; }

        protected override void Awake() {
            base.Awake();
            text = GetComponentInChildren<Text>();
        }

        public Toast toast(string msg,float time) {
            text.text = msg;
            show(true);
            this.delay(()=> show(false),time );
            return this;
        }



    }
}
#endif
