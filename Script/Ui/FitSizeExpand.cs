using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace surfm.tool {
    public class FitSizeExpand : MonoBehaviour {
        public Text target;
        private RectTransform rt;
        private ContentSizeFitter sizeFitter;
        public float wPadding, hPadding;
        // Use this for initialization
        void Start() {
            rt = GetComponent<RectTransform>();
            sizeFitter = target.gameObject.AddComponent<ContentSizeFitter>();
            sizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        }

        void Update() {
            rt.sizeDelta = new Vector2(target.preferredWidth+ wPadding, target.preferredHeight+ hPadding);
        }
    }
}
