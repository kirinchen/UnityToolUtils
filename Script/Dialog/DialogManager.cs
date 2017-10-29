using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace surfm.tool {
    public class DialogManager : MonoBehaviour {

        private static DialogManager instance;

        private Dictionary<Type, Dialog> map = new Dictionary<Type, Dialog>();

        void Awake() {
            foreach (Dialog d in GetComponentsInChildren<Dialog>()) {
                map.Add(d.GetType(), d);
            }
        }


        private T _get<T>() where T : Dialog {
            return (T)map[typeof(T)];
        }

        public static T get<T>() where T : Dialog {
            return getInstance()._get<T>();
        }

        public static DialogManager getInstance() {
            if (instance == null) {
                instance = Instantiate(Resources.Load<DialogManager>("@DialogManager"));
            }
            return instance;
        }

    }
}
