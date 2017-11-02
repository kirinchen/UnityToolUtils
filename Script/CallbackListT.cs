using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace surfm.tool {
    public class CallbackListT<T> {

        private List<Action<T>> list = new List<Action<T>>();
        private T t;

        public void add(Action<T> a) {
            if (list == null) {
                a(t);
            } else {
                list.Add(a);
            }
        }

        public void done(T t) {
            this.t = t;
            list.ForEach(a => a(t));
            list = null;
        }



    }
}
