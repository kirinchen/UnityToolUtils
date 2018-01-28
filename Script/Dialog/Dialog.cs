using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace surfm.tool {
    [RequireComponent(typeof(Popup))]
    public abstract class Dialog : MonoBehaviour {

        public Popup pupop { get; private set; }

        public virtual void Awake() {
            pupop = GetComponent<Popup>();
        }

        public virtual void show(bool b) {
            pupop.show(b);
        }

        public void hide() {
            show(false);
        }

    }
}
