using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace surfm.tool {
    public class ReuseObj : MonoBehaviour {

        public bool closed { get; private set; }


        public virtual bool isClosed() {
            return closed;
        }

        public virtual void initCreated() {
            closed = false;
        }

        public virtual void reActive() {
            closed = false;
            gameObject.SetActive(true);
        }

        public virtual void close() {
            closed = true;
            gameObject.SetActive(false);
        }
    }
}
