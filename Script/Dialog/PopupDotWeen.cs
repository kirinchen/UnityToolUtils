using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace surfm.tool {
    public class PopupDotWeen : Popup {

        private Transform rectT;
        new void Awake() {
            rectT = GetComponent<Transform>();
        }

        void Start() {
            rectT.localScale = Vector3.zero;
        }

        public override void show(bool b) {
            if (b != showed) {
                showed = b;
                if (showed) {
                    fadeIn();
                } else {
                    fadeOut();
                }
            }
        }

        private void fadeOut() {
            rectT.DOScale(new Vector3(-1, -1, -1f), 0.3f).SetRelative().SetLoops(1, LoopType.Yoyo);
        }

        private void fadeIn() {
            rectT.DOScale(new Vector3(1, 1, 1), 0.3f).SetRelative().SetLoops(1, LoopType.Yoyo);
        }
    }
}
