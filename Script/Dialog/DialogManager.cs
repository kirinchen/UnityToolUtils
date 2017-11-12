using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace surfm.tool {
    public class DialogManager : MonoBehaviour {



        private Dictionary<Type, Dialog> map = new Dictionary<Type, Dialog>();

        void Awake() {

            foreach (Dialog d in GetComponentsInChildren<Dialog>()) {
                map.Add(d.GetType(), d);
            }
        }


        public T get<T>() where T : Dialog {
            return (T)map[typeof(T)];
        }





    }
}
