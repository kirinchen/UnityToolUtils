using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace surfm.tool {
    public class BoxCenter : MonoBehaviour {

        private static BoxCenter instance;
        private Dictionary<Type, Box> map = new Dictionary<Type, Box>();
        public Canvas canvas;

        void Awake() {
            canvas.gameObject.SetActive(true);
            instance = this;
            foreach (Box b in GetComponentsInChildren<Box>()) {
                map.Add(b.GetType(), b);
            }
        }

        public Box _show(Type t, BoxBundle b) {
            Box ans = map[t];
            ans.show(b);
            return ans;
        }

        public static Box show(Type t, BoxBundle b) {
            return instance._show(t, b);
        }
    }
}
