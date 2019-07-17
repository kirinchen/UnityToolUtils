using UnityEngine;
namespace surfm.tool {

    public class OverlayBoxShower : MonoBehaviour {
        public OverlayBox overlayBox;
        public OverlayBoxBundle bundle = new OverlayBoxBundle();
        private RectTransform rectT;
        public bool followed;
        public float followInterval = 0.1f;

        void Awake() {
            rectT = GetComponent<RectTransform>();
        }

        public void show() {
            if (followed) {
                bundle.followTram = rectT;
                bundle.followInterval = followInterval;
            } else {
                bundle.position = rectT.position;
            }
            overlayBox.show(bundle);
        }


    }
}
