using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static surfm.tool.Beans;

namespace surfm.tool {
    public class BeansRepo  {

        private static BeansRepo instance;
        public delegate object FindFunc(Type t, string name);
        private FindFunc findFunc;

        private Dictionary<Type, Dictionary<string, Bean>> map = new Dictionary<Type, Dictionary<string, Bean>>();

        private BeansRepo() {
            injectAll();
        }

        private void injectAll() {
            List<Beans> ss = new List<Beans>(Resources.LoadAll<Beans>(""));
            foreach (Beans cc in ss) {
                cc.loadBeans(addOne);
            }
        }

        private void addOne(Beans.Bean b) {
            if (!map.ContainsKey(b.returnType)) {
                map.Add(b.returnType, new Dictionary<string, Beans.Bean>());
            }
            map[b.returnType].Add(b.name,b);
        }

        public void setFindFunc(FindFunc f) {
            findFunc = f;
        }

        public T _bean<T>(Type t, string name = BeanAttribute.DEFAULT) {
            if (map.ContainsKey(t)) {
                Dictionary<string, Bean> beanMap = map[t];
                if (beanMap.ContainsKey(name)) {
                    Bean b = beanMap[name];
                    return b.getBean<T>();
                }
            }
            if (findFunc != null) return (T)findFunc(t,name);
            throw new NullReferenceException("not find this bean t="+t+" name="+name);
        }

        public static T bean<T>(Type t, string name = BeanAttribute.DEFAULT) {
            return getInstance()._bean<T>(t,name);
        }


        public static BeansRepo getInstance() {
            if (instance == null) {
                instance = new BeansRepo();
            }
            return instance;
        }

    }
}
