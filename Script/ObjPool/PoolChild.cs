using UnityEngine;
using UnityEngine.UI;

namespace surfm.tool {

    public class PoolChild : MonoBehaviour {

        public TriggerEvent triggerEvent;
        public bool destroyMe = true;

        public void onUnspawn() {
            Debug.Log("OnUnspawn");
            triggerEvent.invoke();
            if (destroyMe) Destroy(gameObject);
        }

    }
}
