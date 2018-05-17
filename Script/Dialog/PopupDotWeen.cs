using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace surfm.tool {
    public class PopupDotWeen : PopupAbs {

        private Transform rectT;
         void Awake() {
            rectT = GetComponent<Transform>();
        }

        void Start() {
            rectT.localScale = Vector3.zero;
        }



        protected override void hide() {
            rectT.DOScale(new Vector3(-1, -1, -1f), 0.3f).SetRelative().SetLoops(1, LoopType.Yoyo).OnComplete(()=> {
                rectT.localScale = Vector3.zero;
            });
        }

        protected override void show() {
            rectT.localScale = Vector3.zero;
            rectT.DOScale(new Vector3(1, 1, 1), 0.3f).SetRelative().SetLoops(1, LoopType.Yoyo);
        }
    }
}
