using GUIAnimator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace surfm.tool {
    public class Popup : PopupAbs {

        public GAui gaui;

        public virtual void Awake() {
            if (gaui == null) {
                gaui = GetComponent<GAui>();
            }
        }


        protected override void hide() {
            gaui.PlayOutAnims(eGUIMove.Self);
        }

        protected override void show() {
            gaui.PlayInAnims(eGUIMove.Self);
        }
    }
}
