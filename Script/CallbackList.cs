using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace surfm.tool {
    public class CallbackList {

        private List<Action> list = new List<Action>();

        public void add(Action a) {
            if (list == null) {
                a();
            } else {
                list.Add(a);
            }
        }

        public void done() {
            if (list == null) return;
            list.ForEach(a => a());
            list = null;
        }



    }
}
