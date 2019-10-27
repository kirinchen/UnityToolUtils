using UnityEngine;
using UnityEngine.UI;

namespace surfm.tool {
    public class DynamicBox : MonoBehaviour {

        private ContentSizeFitter sizeFitter;
        private RectTransform contentRectT;
        private RectTransform rectT;
        public float padding;

        public float fitInterval = 0.5f;

        public void Awake() {
            sizeFitter = GetComponentInChildren<ContentSizeFitter>();
            contentRectT = sizeFitter.GetComponent<RectTransform>();
            rectT = GetComponent<RectTransform>();
        }

        public void Start() {
            InvokeRepeating("routineFit", 0.1f, fitInterval);
        }

        void routineFit() {
            float x, y;
            bool fitble = false;
            if (sizeFitter.horizontalFit == ContentSizeFitter.FitMode.PreferredSize) {
                x = contentRectT.sizeDelta.x + padding;
                fitble = true;
            } else {
                x = rectT.sizeDelta.x;
            }

            if (sizeFitter.verticalFit == ContentSizeFitter.FitMode.PreferredSize) {
                y = contentRectT.sizeDelta.y + padding;
                fitble = true;
            } else {
                y = rectT.sizeDelta.y;
            }
            Vector2 v = new Vector2(x, y);
             if (fitble  && v.x > 0 && v.y>0 ) {
                 rectT.sizeDelta = v;
             }       


        }


    }
}
