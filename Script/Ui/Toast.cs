using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using GUIAnimator;

namespace surfm.tool {
    public class Toast : MonoBehaviour {
        private static Toast instance;
        private GAui anim;
        public Text text;
        public Image icon;
        public Action callback;

        void Awake() {
            instance = this;
            anim = GetComponent<GAui>();
        }


        public Toast setCallback(Action a) {
            callback = a;
            return this;
        }

        public void show(string t) {
            show(null, Color.white, t);
        }

        public void show(Sprite spr, Color iconColor, string t) {
            if (spr == null) {
                icon.gameObject.SetActive(false);
            } else {
                icon.gameObject.SetActive(true);
                icon.sprite = spr;
                icon.color = iconColor;
            }
            text.text = t;
            anim.PlayInAnims(eGUIMove.Self);
            StartCoroutine(delayClose());
        }



        private IEnumerator delayClose() {
            yield return new WaitForSeconds(2f);
            anim.PlayOutAnims(eGUIMove.Self);
            yield return new WaitForSeconds(.3f);
            if (callback != null) {
                callback();
                callback = null;
            }
        }

        public static Toast getInstance() {
            return instance;
        }

    }
}
