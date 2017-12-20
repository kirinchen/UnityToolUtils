using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace surfm.tool {
    public class Toast : MonoBehaviour {
        private static Toast instance;
        private GAui anim;
        public Text text;
        public Image icon;
        void Awake() {
            instance = this;
            anim = GetComponent<GAui>();
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
            anim.MoveIn(GUIAnimSystem.eGUIMove.Self);
            StartCoroutine(delayClose());
        }



        private IEnumerator delayClose() {
            yield return new WaitForSeconds(1f);
            anim.MoveOut(GUIAnimSystem.eGUIMove.Self);
        }

        public static Toast getInstance() {
            return instance;
        }

    }
}
