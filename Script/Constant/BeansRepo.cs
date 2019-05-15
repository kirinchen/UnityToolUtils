using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static surfm.tool.Beans;

namespace surfm.tool {
    public class BeansRepo  {

        private static BeansRepo instance;

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

        public T _bean<T>(Type t, string name = BeanAttribute.DEFAULT) {
            Bean b= map[t][name];
            return b.getBean<T>();
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
