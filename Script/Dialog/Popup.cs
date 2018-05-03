using GUIAnimator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace surfm.tool {
    public class Popup : MonoBehaviour {

        public GAui gaui;
        public bool showed { get; protected set; }

        public virtual void Awake() {
            if (gaui == null) {
                gaui = GetComponent<GAui>();
            }
        }

        public virtual void show(bool b) {
            if (b != showed) {
                showed = b;
                if (showed) {
                    gaui.PlayInAnims(eGUIMove.Self);
                } else {
                    gaui.PlayOutAnims(eGUIMove.Self);
                }
            }
        }
    }
}
