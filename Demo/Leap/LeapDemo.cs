using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace surfm.tool {
    public class LeapDemo : MonoBehaviour {
        private Image img;
        // Use this for initialization
        void Start() {
            img = GetComponent<Image>();
              
        }

        public void run() {
            img.fillAmount = 1;
            StartCoroutine(UnityUtils.leap<Image>(img, i => {
                return i.fillAmount > 0;
            }, i => {
                i.fillAmount -= ((float)1 / (float)20);
            }, 0.25f));
        }


    }
}
