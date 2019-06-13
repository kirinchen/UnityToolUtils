using System.Collections.Generic;
using UnityEngine;

namespace surfm.tool {


    public class ObjectPools : MonoBehaviour {

        public static ObjectPools instance { get; private set; }

        public List<Listener> listeners { get; private set; } = new List<Listener>();
        private List<Pool> pools = new List<Pool>();

        void Awake() {
            instance = this;
        }

        public  GameObject spawn(GameObject obj, float activeDuration = -1) {
            return spawn(obj, Vector3.zero, Quaternion.identity, activeDuration);
        }

        public GameObject spawn(GameObject obj, Vector3 pos, Quaternion rot, float activeDuration = -1) {
            if (obj == null) {
                Debug.Log("NullReferenceException: obj unspecified");
                return null;
            }
            int ID = getPoolIdx(obj);
            if (ID == -1) ID = newOrGetPool(obj);
            GameObject spawnedObj = pools[ID].Spawn(pos, rot);
            listeners.ForEach(l=> l.onSpawn(spawnedObj));
            if (activeDuration > 0) unspawn(spawnedObj,activeDuration);
            return spawnedObj;
        }


        public void unspawn(GameObject obj, float delay) {
            UnityUtils.delay(this,()=> {
                unspawn(obj);
            },delay);
        }

        public void unspawn(GameObject obj) {
            obj.SendMessage("OnUnspawn", SendMessageOptions.DontRequireReceiver);
            listeners.ForEach(l => l.onUnSpawn(obj));
            for (int i = 0; i < pools.Count; i++) {
                if (pools[i].Unspawn(obj)) return;
            }
            Destroy(obj);
        }


        private int newOrGetPool(GameObject obj, int count = 2) {
            int ID = getPoolIdx(obj);
            if (ID == -1) {
                Pool pool = new Pool();
                pool.prefab = obj;
                pool.MatchObjectCount(count);
                pools.Add(pool);
                ID = pools.Count - 1;
            }
            return ID;
        }

        int getPoolIdx(GameObject obj) {
            for (int i = 0; i < pools.Count; i++) {
                if (pools[i].prefab == obj) return i;
            }
            return -1;
        }


        public  void clearAll() {
            for (int i = 0; i < pools.Count; i++) pools.Clear();
            pools = new List<Pool>();
        }


        [System.Serializable]
        public class Pool {
            public GameObject prefab;

            public List<GameObject> inactiveList = new List<GameObject>();
            public List<GameObject> activeList = new List<GameObject>();

            public int cap = 1000;


            public GameObject Spawn(Vector3 pos, Quaternion rot) {
                GameObject obj = null;

                while (inactiveList.Count > 0 && inactiveList[0] == null) inactiveList.RemoveAt(0);

                if (inactiveList.Count == 0) {
                    obj = (GameObject)MonoBehaviour.Instantiate(prefab, pos, rot);
                } else {
                    obj = inactiveList[0];
                    obj.transform.parent = null;
                    obj.transform.position = pos;
                    obj.transform.rotation = rot;
                    obj.SetActive(true);
                    inactiveList.RemoveAt(0);
                }

                activeList.Add(obj);
                return obj;
            }

            public bool Unspawn(GameObject obj) {
                if (activeList.Contains(obj)) {
                    obj.SetActive(false);
                    obj.transform.parent = ObjectPools.instance.transform;
                    activeList.Remove(obj);
                    inactiveList.Add(obj);
                    return true;
                }
                if (inactiveList.Contains(obj)) {
                    return true;
                }
                return false;
            }


            public void MatchObjectCount(int count) {
                if (count > cap) return;
                int currentCount = GetTotalObjectCount();
                for (int i = currentCount; i < count; i++) {
                    GameObject obj = (GameObject)MonoBehaviour.Instantiate(prefab);
                    obj.SetActive(false);
                    obj.transform.parent = ObjectPools.instance.transform;
                    inactiveList.Add(obj);
                }
            }

            public int GetTotalObjectCount() {
                return inactiveList.Count + activeList.Count;
            }

            public void Clear() {
                for (int i = 0; i < inactiveList.Count; i++) {
                    if (inactiveList[i] != null) MonoBehaviour.Destroy(inactiveList[i]);
                }
                for (int i = 0; i < activeList.Count; i++) {
                    if (activeList[i] != null) MonoBehaviour.Destroy(inactiveList[i]);
                }
            }

        }


        public interface Listener {
            void onSpawn(GameObject newspawnedObj);
            void onUnSpawn(GameObject newspawnedObj);
        }


    }
}