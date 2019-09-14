using System;
using System.Collections.Generic;
namespace surfm.tool {
    public class CallbackListT<T> {

        private List<Action<T>> list = new List<Action<T>>();
        private T t;
        private bool doned = false;

        public void add(Action<T> a) {
            if (isDone()) {
                a(t);
            } else {
                list.Add(a);
            }
        }

        public void done(T t) {
            if (doned) return;
            doned = true;
            this.t = t;
            list.ForEach(a => a(t));
            list = null;
        }

        public bool isDone() {
            return doned;

        }

    }
}
