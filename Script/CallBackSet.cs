using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace surfm.tool {

    public class CallBackSet<E, F, R> where E : Enum {

        private Dictionary<F, object> tarMap = new Dictionary<F, object>();
        private Dictionary<E, SortF> map = new Dictionary<E, SortF>();
        private Func<F, R, CBResult<R>> customFunc;

        public CallBackSet(Func<F, R, CBResult<R>> f) {
            customFunc = f;
        }


        public void add(E e, F f ,int idx ,object o ) {
            if (tarMap.ContainsKey(f)) return;
            if (!map.ContainsKey(e)) {
                map.Add(e, new SortF());
            }
            SortF sortF =  map[e] ;
            tarMap.Add(f,o);
            sortF.add(f,idx);
        }

        public void remove(E e, F f) {
            if (!tarMap.ContainsKey(f)) return;
            tarMap.Remove(f);
            map[e].remove(f);
        }



        public R apply(R _default) {
            R ans = _default;
            foreach (E e in Enum.GetValues(typeof(E))) {
                if (!map.ContainsKey(e)) continue;
                SortF sortF = map[e];
                foreach (F f in sortF.list) {
                    CBResult<R> cr = customFunc(f, ans);
                    if (cr.breakloop) {
                        return cr.ans;
                    }
                    ans = cr.ans;
                }
            }
            return ans;
        }

        public void clear() {
            map.Clear();
            tarMap.Clear();
        }

        public void removeAll(Func<object,bool> checkF) {
            foreach (SortF sortF in map.Values) {
                List<F> removeF = sortF.list.FindAll(f => checkF(tarMap[f]));
                removeF.ForEach(f=> tarMap.Remove(f));
                removeF.ForEach(f => sortF.remove(f));
            }
        }

        public int size() {
            return map.Count;
        }


        public class SortF {
            private Dictionary<F, int> map = new Dictionary<F, int>();
            public List<F> list { get; private set; } = new List<F>();

            public void add(F msf, int idx = 0) {
                if (!map.ContainsKey(msf)) {
                    map.Add(msf, idx);
                    sortFuncs();
                }
            }

            internal void remove(F f) {
                list.Remove(f);
                map.Remove(f);
            }

            private void sortFuncs() {
                list = new List<F>(map.Keys);
                list.Sort((a, b) => {
                    int aI = map[a];
                    int bI = map[b];
                    return aI.CompareTo(bI);
                });
            }


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
