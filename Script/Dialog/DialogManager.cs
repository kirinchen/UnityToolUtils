using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace surfm.tool {
    public class DialogManager : MonoBehaviour {

        public static DialogManager instance { get; private set; }

        private Dictionary<Type, Dialog> map = new Dictionary<Type, Dialog>();

        void Awake() {
            instance = this;
            foreach (Dialog d in GetComponentsInChildren<Dialog>()) {
                map.Add(d.GetType(), d);
            }
        }

        public T get<T>() where T : Dialog {
            return (T)map[typeof(T)];
        }

    }
}
