using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace surfm.tool {
    public abstract class PopupAbs : MonoBehaviour {

        public bool showed { get; protected set; }


        public virtual void show(bool b) {
            if (b != showed) {
                showed = b;
                if (showed) {
                   show();
                } else {
                    hide();
                }
            }
        }

        protected abstract void hide();
        protected abstract void show();
    }
}
