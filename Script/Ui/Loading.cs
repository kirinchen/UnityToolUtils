using UnityEngine;
using System.Collections;
using GUIAnimator;

namespace surfm.tool {
    public class Loading : MonoBehaviour {
        private static Loading insatnce;
        private GAui gaui;
        private bool showed = false;


        void Awake() {
            insatnce = this;
            gaui = GetComponent<GAui>();
        }

        public void show(bool b) {
            if (b != showed) {
                showed = b;
                if (showed) {
                    gaui.PlayInAnims(eGUIMove.Self);
                } else {
                    gaui.PlayOutAnims(eGUIMove.Self);
                }
            }
        }

        public static Loading getInstance() {
            return insatnce;
        }
    }
}
