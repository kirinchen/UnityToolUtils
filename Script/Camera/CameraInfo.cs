using UnityEngine;

namespace surfm.tool {
    public class CameraInfo {
        private Camera cam;
        public Rect cr { get { return cam.rect; } }
        //private Vector2 leftTop { get { return new Vector2(cr.xMin + cr.center.x, cr.yMin + cr.center.y); } }
        //private Vector2 rightBottom { get { return new Vector2(cr.xMax + cr.center.x, cr.yMax + cr.center.y); } }

        public CameraInfo(Camera c) {
            cam = c;
        }

        public Rect getPlaneRect(int maskIdx) {
            Vector3 rb;
            if (!getPlanePoint(out rb, cr.max, maskIdx)) return new Rect();
            Vector3 lt;
            if (!getPlanePoint(out lt, cr.min, maskIdx)) return new Rect();
            Vector3 c = (lt + rb) / 2;
            Vector2 c2 = new Vector2(c.x, c.z);
            Vector3 size = rb - lt;
            Vector2 size2 = new Vector2(size.x, size.y);
            return new Rect(c2, size2);

        }



        public bool getPlanePoint(out Vector3 _point, Vector3 v, int maskIdx) {
            RaycastHit hit;
            Camera cm = Camera.main;
            Rect cr = cm.rect;
            Ray ray = cm.ViewportPointToRay(v);
            int mask = ~(1 << maskIdx);
            if (Physics.Raycast(ray, out hit, 99999, mask)) {
                // Transform objectHit = hit.transform;
                // Debug.DrawRay(hit.point, ray.direction * 10, Color.yellow);
                _point = hit.point;
                // Do something with the object that was hit by the raycast.
                return true;
            }
            _point = Vector3.zero;
            return false;
        }


    }
}