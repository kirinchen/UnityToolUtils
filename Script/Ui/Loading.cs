using UnityEngine;
using System.Collections;
using GUIAnimator;
using System;

namespace surfm.tool {
    public class Loading : MonoBehaviour {
        private static Loading insatnce;
        private GAui gaui;
        private bool showed = false;


        void Awake() {
            insatnce = this;
            gaui = GetComponent<GAui>();
        }

        public bool isShowed() {
            
            return showed;
        }

        public void show(bool b) {
            //Debug.Log("Loading");
            Action a = () => {
                if (b != showed) {
                    showed = b;
                    if (showed) {
                        gaui.PlayInAnims(eGUIMove.SelfAndChildren);
                    } else {
                        gaui.PlayOutAnims(eGUIMove.SelfAndChildren);
                    }
                }
            };
            StartCoroutine(UnityUtils.wait(()=> {
                return gaui.IsAnimating();
            },a));
        }

        private IEnumerator  waitGaui(GAui g,Action a) {
            while (g.IsAnimating()) {
                yield return new WaitForSeconds(.2f);
            }
            a();
                                                    
        }

        public static Loading getInstance() {
            return insatnce;
        }
    }
}
