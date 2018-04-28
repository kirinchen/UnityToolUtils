using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace surfm.tool {
    public class ObjPool : MonoBehaviour {

        private static ObjPool instance;

        private List<ReuseObj> pool = new List<ReuseObj>();

        public ReuseObj instantiate(ReuseObj original) {
            ReuseObj ro= findClose(original.GetType());
            if (ro == null) {
                original = Instantiate(original);
                pool.Add(original);
                original.initCreated();
                return original;
            } else {
                ro.reActive();
                return ro;
            }
        }

        public void close(ReuseObj _o) {
            bool b= pool.Exists(o => {
                return _o == o;
            });
            if (!b) throw new NullReferenceException("not find this obj in pool");
            _o.close();
        }

        private ReuseObj findClose(Type type) {
             return pool.Find( o=> {
                return o.isClosed() && o.GetType().Equals(type) ;
            } );
        }

        public static ObjPool getInstance() {


            if (instance == null) {
                GameObject go = new GameObject("@ObjPool");
                instance= go.AddComponent<ObjPool>();
            }
            return instance;
        }


    }
}
