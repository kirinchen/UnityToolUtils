using System;
using UnityEngine;
namespace surfm.tool {


    public class OverlayBox : MonoBehaviour {

        public float hideTime = .7f;
        private bool showed = false;
        private RectTransform rectT;
        private Vector2 orgSizeDelta;
        private Coroutine followCoroutine;


        protected virtual void Awake() {
            rectT = GetComponent<RectTransform>();
            orgSizeDelta = rectT.sizeDelta;
            gameObject.SetActive(false);
        }

        internal void close() {
            if (followCoroutine != null) {
                StopCoroutine(followCoroutine);
            }
            gameObject.SetActive(false);
        }

        public void show(OverlayBoxBundle b) {
            if (followCoroutine != null) {
                StopCoroutine(followCoroutine);
            }
            gameObject.SetActive(true);
            if (!b.followTram) {
                rectT.position = b.getPosition() + b.setting.shiftPos ;
            } else {
                rectT.position = b.getPosition() + b.setting.shiftPos;
                followCoroutine = UnityUtils.loop(this, () => {
                    rectT.position = b.getPosition() + b.setting.shiftPos;
                }, 0, b.setting.followInterval);

            }
        }



        public bool isShow() {
            return gameObject.activeInHierarchy;
        }


    }
    [System.Serializable]
    public class OverlayBoxSetting {
        public Vector3 shiftPos;
        public float followInterval = 0.1f;
    }


    public class OverlayBoxBundle {
        public OverlayBoxSetting setting { get; private set; }
        private Vector3 position;
        public RectTransform followTram { get; private set; }

        internal OverlayBoxBundle(OverlayBoxSetting s, RectTransform rt) {
            setting = s;
            followTram = rt;
        }

        internal OverlayBoxBundle(OverlayBoxSetting s, Vector3 pos) {
            setting = s;
            position = pos;
        }

        public Vector3 getPosition() {
            if (followTram) return followTram.position;
            return position;
        }

    }

}
