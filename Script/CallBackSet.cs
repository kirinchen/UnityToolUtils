using System;
using System.Collections.Generic;
using UnityEngine;

namespace surfm.tool {

    public class CallBackSet<E, F, R> where E : Enum {
        public Dictionary<E, F> map = new Dictionary<E, F>();
        public Func<F, R, CBResult<R>> customFunc;

        public CallBackSet(Func<F, R, CBResult<R>> f) {
            customFunc = f;
        }

        public void add(E e, F f) {
            map.Add(e, f);
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

        internal void crear() {
            map.Clear();
        }
    }


    public class CallBackSetTool {
        public delegate bool BoolFunc();
        public delegate string StringFunc(string ins);


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
