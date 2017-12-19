
using System;
using System.Reflection;
using UnityEngine;
namespace surfm.tool {
    public class CommConst : MonoBehaviour {

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
        }

    }
}
