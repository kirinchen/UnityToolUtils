using System;
using System.Collections.Generic;
namespace surfm.tool {
    public class CallbackListT<T> {

        private List<Action<T>> list = new List<Action<T>>();
        private T t;

        public void add(Action<T> a) {
            if (isDone()) {
                a(t);
            } else {
                list.Add(a);
            }
        }

        public void done(T t) {
            this.t = t;
            List<Action<T>> _l = new List<Action<T>>(list);
            list = null;
            _l.ForEach(a => a(t));
        }

        public bool isDone() {
            return t != null || list == null;

        }

    }
}
