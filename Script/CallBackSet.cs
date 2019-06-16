using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace surfm.tool {

    public class CallBackSet<E, F, R> where E : Enum {

        public Dictionary<E, object> tarMap = new Dictionary<E, object>();
        public Dictionary<E, F> map = new Dictionary<E, F>();
        public Func<F, R, CBResult<R>> customFunc;

        public CallBackSet(Func<F, R, CBResult<R>> f) {
            customFunc = f;
        }

        public void add(E e, F f ,object o = null) {
            map.Add(e, f);
            tarMap.Add(e,o);
        }

        public R apply(R _default) {
            R ans = _default;
            foreach (E e in Enum.GetValues(typeof(E))) {
                if (!map.ContainsKey(e)) continue;
                F f = map[e];
                CBResult<R> cr = customFunc(f, ans);
                ans = cr.ans;
                if (cr.breakloop) {
                    break;
                }
            }
            return ans;
        }

        public void clear() {
            map.Clear();
            tarMap.Clear();
        }

        public void removeAll(Func<E,object,bool> checkF) {
            List<E> removedKeys= tarMap.Keys.ToList().FindAll(k => checkF(k, tarMap[k]));
            removedKeys.FindAll(k => tarMap.Remove(k) );
            removedKeys.FindAll(k => map.Remove(k));
        }

        public int size() {
            return map.Count;
        }
    }


    public class CallBackSetTool {
        public delegate bool BoolFunc();
        public delegate string StringFunc(string ins);

        public static CBResult<object>  action(Action arg1, object arg2) {
            arg1();
            return new CBResult<object>() {
                breakloop = false,
                ans = arg2
            };
        }

        public static CBResult<bool> boolPositive(BoolFunc f, bool arg2) {
            bool b = f();
            return new CBResult<bool>() {
                breakloop = b,
                ans = b
            };
        }


        public static CBResult<string> stringNormal(StringFunc f, string arg2) {
            string b = f(arg2);
            return new CBResult<string>() {
                breakloop = false,
                ans = b
            };
        }
    }


    public struct CBResult<R> {
        public bool breakloop;
        public R ans;
    }

}
