using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace surfm.tool {
    public class MViewCBinder<D> : MonoBehaviour {
        public delegate bool CustomShow(bool b, MonoBehaviour obj);
        public delegate void OnView(D d, MonoBehaviour b);
        public delegate bool OnShow(D d);
        private CustomShow customShow = (b, o) => { return false; };
        private Dictionary<MonoBehaviour, B> bindMap = new Dictionary<MonoBehaviour, B>();

        public struct B {
            public MonoBehaviour obj;
            public OnView view;
            public OnShow show;
        }

        public MViewCBinder<D> bind(B b) {
            bindMap.Add(b.obj, b);
            return this;
        }

        public MViewCBinder<D> setCustomShow(CustomShow cs) {
            customShow = cs;
            return this;
        }

        public void reflesh(D model) {
            foreach (B b in bindMap.Values) {
                bool showed = b.show==null ? true : b.show(model);
                show(showed, b.obj);
                if (showed) {
                    if (b.view != null) {
                        b.view(model, b.obj);
                    }
                }
            }
        }

        private void show(bool b, MonoBehaviour obj) {
            if (!customShow(b, obj)) {
                obj.gameObject.SetActive(b);
            }
        }



    }
}
