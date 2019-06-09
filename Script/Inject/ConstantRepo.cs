﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace surfm.tool {
    public class ConstantRepo {
        private static ConstantRepo instance;
        private Dictionary<string, object> map = new Dictionary<string, object>();

        private ConstantRepo() {
            injectAll();
        }

        private void injectAll() {
            List<CommConst> ss = new List<CommConst>( Resources.LoadAll<CommConst>(""));
            ss.Sort((a,b)=> { return a.index.CompareTo(b.index); });
            foreach (CommConst cc in ss) {
                cc.injectCommConst(addOne);
            }
        }

        private void addOne(string k, object v) {
            if (map.ContainsKey(k)) map.Remove(k);
            map.Add(k, v);
        }

        public T opt<T>(string k, T _default) {
            if (map.ContainsKey(k)) return (T)map[k];
            return _default;
        }

        public T get<T>(string k) {
            return (T)map[k];
        }

        public object get(string k) {
            return map[k];
        }

        public static ConstantRepo getInstance() {
            if (instance == null) {
                instance = new ConstantRepo();
            }
            return instance;
        }


    }
}
