using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
namespace surfm.tool {
    public class Beans : MonoBehaviour {


        public class Bean {
            public bool instanced     { get; internal set; }
            public string name { get; internal set; }
            public Func<object> beanFunc { get; internal set; }
            public object beanInstance { get; internal set; }
            public Type returnType { get; internal set; }

            internal T getBean<T>() {
                if (instanced) {
                    return (T)beanInstance;
                } else {
                    return (T)beanFunc();
                }
            }
        }

        public void loadBeans(Action<Bean> cb) {
            Type t = GetType();
            MethodInfo[] methods = t.GetMethods();
            foreach (MethodInfo mi in methods) {
                BeanAttribute ba = (BeanAttribute)Attribute.GetCustomAttribute(mi, typeof(BeanAttribute));
                if (ba != null) {
                    Bean b = new Bean();
                    b.returnType = mi.ReturnType;
                    b.name = ba.name;
                    b.instanced = ba.instanced;
                    if (ba.instanced) {
                        b.beanInstance =  mi.Invoke(this, null);
                    } else {
                        b.beanFunc = () => { return mi.Invoke(this, null); };
                    }
                    cb(b);
                }
            }

        }

    }
}
