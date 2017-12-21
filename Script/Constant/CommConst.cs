
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
namespace surfm.tool {
    public class CommConst : MonoBehaviour {

        [System.Serializable]
        public class Set {

            public enum ValType {
                _string, _float, _vector3, _sprite,_bool
            }
            public ValType valType;
            public string key;
            public string _stringVal;
            public float _floatVal;
            public Vector3 _vector3Val;
            public Sprite _spriteVal;
            public bool _boolVal;

            public object getValue() {
                Type t = GetType();
                FieldInfo fi = t.GetField(valType.ToString() + "Val");
                return fi.GetValue(this);

            }
        }


        public List<Set> sets = new List<Set>();

        public void injectCommConst(Action<string, object> cb) {
            Type t = GetType();
            FieldInfo[] fis = t.GetFields();
            foreach (FieldInfo fi in fis) {
                ConstAttribute ca = (ConstAttribute)Attribute.GetCustomAttribute(fi, typeof(ConstAttribute));
                if (ca != null) {
                    object o = fi.GetValue(this);
                    cb(ca.key, o);
                }
            }
            sets.ForEach(s => {
                cb(s.key, s.getValue());
            });


        }

    }
}
