using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace surfm.tool {
    public abstract class Box : MonoBehaviour {

        private GAui gaui;

        private bool showed = false;

        public void Awake() {
            gaui = GetComponent<GAui>();
        }


        private void show(bool b) {
            if (b != showed) {
                if (b) {
                    gaui.MoveIn(GUIAnimSystem.eGUIMove.Self);
                } else {
                    gaui.MoveOut(GUIAnimSystem.eGUIMove.Self);
                }
                showed = b;
            }
        }


        public virtual void show(BoxBundle b) {
            show(true);
        }

        public void close() {
            show(false);
        }


    }
}
