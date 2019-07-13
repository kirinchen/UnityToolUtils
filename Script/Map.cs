using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

        public V get(K fid, V defaultV) {
            if (!this.ContainsKey(fid)) {
                this.Add(fid, defaultV);
            }
            return this[fid];
        }

    }
}
