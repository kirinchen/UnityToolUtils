using UnityEngine;
using UnityEngine.UI;

namespace surfm.tool {

    public class PoolChild : MonoBehaviour {

        public TriggerEvent triggerEvent;
        public bool destroyMe = true;

        void OnUnspawn() {
            triggerEvent.invoke();
            if (destroyMe) ObjectPools.instance.unspawn(gameObject);
        }

    }
}
