using System;
using System.Collections.Generic;
using UnityEngine;
using static surfm.tool.BeansRepo;

namespace surfm.tool {
    public class UnityBeansFinder : FindFunc {
        public object findBean(Type t, string name) {
            List<UnityEngine.Object> os = findObjects(t);
            foreach (UnityEngine.Object o in os) {
                CheckBD checkBD = check(o, name);
                if (checkBD.yes) {
                    if (checkBD.cache) {
                        saveBean(o, name);
                    }

                    return o;
                }
            }
            return null;

        }

        private List<UnityEngine.Object> findObjects(Type t) {
            List<UnityEngine.Object> ans = new List<UnityEngine.Object>();
            if (t.IsInterface) {
                MonoBehaviour[] oss = UnityEngine.Object.FindObjectsOfType<MonoBehaviour>();
                foreach (MonoBehaviour mb in oss) {
                    if (mb.GetType().GetInterface(t.Name) == t) {
                        ans.Add(mb);
                    }
                }
            } else {
                ans = new List<UnityEngine.Object>(UnityEngine.Object.FindObjectsOfType(t));
            }
            return ans;
        }


        private void saveBean(UnityEngine.Object o,string name) {
            Beans.Bean b = new Beans.Bean();
            b.instanced = true;
            b.name = name;
            b.returnType = o.GetType();
            b.beanInstance = o;
            BeansRepo.getInstance().addOne(b,true);
        }

        public struct CheckBD {
            public bool yes;
            public bool cache;
        }

        private CheckBD check(UnityEngine.Object o,string name) {
            Type ot = o.GetType();
            UnityBeanAttribute uba = (UnityBeanAttribute)Attribute.GetCustomAttribute(ot, typeof(UnityBeanAttribute));
            if (uba == null && BeanAttribute.DEFAULT.Equals(name)) return new CheckBD { yes = true ,cache = false };
            if (string.Equals(name, uba.name)) return new CheckBD { yes = true, cache = uba.cache };
            return new CheckBD { yes = false };
        }

        public static void enable() {
            BeansRepo.getInstance().setFindFunc(new UnityBeansFinder());
        }


    }
}