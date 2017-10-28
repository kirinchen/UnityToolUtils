using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace surfm.tool {
    public abstract class Dialog : MonoBehaviour {
        private EasyTween tween;
        public bool showed { get { return tween.IsObjectOpened(); } }

        public virtual void Awake() {
            tween = GetComponent<EasyTween>();
            tween.rectTransform.localPosition = new Vector3(-9999, -9999, 0);
        }

        public void show(bool b) {
            if (b != showed) {
                tween.OpenCloseObjectAnimation();
            }
        }

    }
}
