using System;
using UnityEngine;
namespace surfm.tool {

    public class OverlayBoxShower : MonoBehaviour {
        public OverlayBoxSetting setting;
        public OverlayBox overlayBox;
        private RectTransform rectT;
        public bool followed;
        

        void Awake() {
            rectT = GetComponent<RectTransform>();
        }

        public void show(bool b) {
            if (!b) {
                overlayBox.close();
                return;
            }
            OverlayBoxBundle bundle = null;
            if (followed) {
                bundle = new OverlayBoxBundle(setting, rectT);
            } else {
                bundle = new OverlayBoxBundle(setting, rectT.position);
            }
            overlayBox.show(bundle);
        }



    }
}
