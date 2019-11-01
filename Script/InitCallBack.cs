using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace surfm.tool {
    public class InitCallBack {

        private List<Action> list = null;

        public InitCallBack() {
            list = new List<Action>();
        }

        public void add(Action a) {
            if (list == null) {
                a();
            } else {
                list.Add(a);
            }
        }

        public bool isDone() {
            return list == null;
        }

        public void done() {
            if (isDone()) return;
            List<Action> al = new List<Action>(list);
            list = null;
            al.ForEach(a => a());
        }

        internal void reset() {
            list = new List<Action>();
        }
    }
}
