using UnityEngine;
using UnityEngine.UI;
namespace surfm.tool {


    public class OverlayBox : MonoBehaviour {

        private static OverlayBox instance;
        public float hideTime = .7f;
        private Text text;
        private float _hideAt;
        private bool showed = false;
        private RectTransform rectT;
        private Vector2 orgSizeDelta;
        private Coroutine followCoroutine;

        void Awake() {
            instance = this;
            rectT = GetComponent<RectTransform>();
            orgSizeDelta = rectT.sizeDelta;
            text = GetComponentInChildren<Text>();
            gameObject.SetActive(false);
        }




        public void show(OverlayBoxBundle b) {

            if (followCoroutine != null) {
                StopCoroutine(followCoroutine);
            }

            gameObject.SetActive(true);
            if (!b.followTram) {
                rectT.position = b.position + b.shiftPos;
            } else {
                rectT.position = b.followTram.position + b.shiftPos;
                followCoroutine = UnityUtils.loop(this, () => {
                    rectT.position = b.followTram.position + b.shiftPos;
                }, 0, b.followInterval);

            }
            text.text = b.msg;
            _hideAt = Time.time + hideTime;

        }


    }

    [System.Serializable]
    public class OverlayBoxBundle {
        public bool top;
        public string msg;
        public Vector3 shiftPos;
        internal Vector3 position;
        internal RectTransform followTram;
        internal float followInterval = 0.1f;
    }

}
