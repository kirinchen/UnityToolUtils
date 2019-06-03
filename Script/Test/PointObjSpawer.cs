using UnityEngine;

namespace surfm.dreamon {

    public class PointObjSpawer : MonoBehaviour {

        public string layerName;

        public bool stop = true;

        public GameObject prefab;

        void Update() {

            if (Input.GetKey(KeyCode.S)) {
                stop = !stop;
            }
            if (stop) return;

            RaycastHit hit;
            mouseRaycast(Input.mousePosition, out hit);
            Instantiate(prefab, hit.point, Quaternion.identity);


        }

        public bool mouseRaycast(Vector3 mousePos, out RaycastHit hit) {
            LayerMask mask = 1 << LayerMask.NameToLayer(layerName);
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            return (Physics.Raycast(ray, out hit, Mathf.Infinity, mask));
        }
    }
}

