using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Globalization;

namespace surfm.tool {

    public class Map<K,V> : Dictionary<K, V> {

        public Map() { }

        public void put(K fid, V rate) {
            if (!this.ContainsKey(fid)) {
                this.Add(fid, rate);
                return;
            }
            this[fid] = rate;
        }

        public V get(K fid) {
            return get(fid , defaultV());
        }

        public V get(K fid, V defaultV) {
            if (!this.ContainsKey(fid)) {
                this.Add(fid, defaultV);
            }
            return this[fid];
        }

        internal List<K> listKeysByValue(V c) {
            List<K> ans = new List<K>();
            foreach (K k in Keys) {
                if (this[k].Equals(c)) {
                    ans.Add(k);
                }
            }
            return ans;
        }

        protected virtual V defaultV() {
            return default(V);
        }
    }
}
