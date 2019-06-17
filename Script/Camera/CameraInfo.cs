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


        public ViewPortVertex getViewPortVertex(int maskIdx) {
            ViewPortVertex ans = new ViewPortVertex();
            ans.lt = getPlanePoint(cr.min, maskIdx);
            ans.lb = getPlanePoint(cr.min+new Vector2(0,cr.height), maskIdx);
            ans.rt = getPlanePoint(cr.min + new Vector2(cr.width, 0), maskIdx);
            ans.rb = getPlanePoint(cr.max, maskIdx);
            return ans;
        }


        public PointInfo getPlanePoint( Vector3 v, int maskIdx) {
            RaycastHit hit;
            Camera cm = Camera.main;
            Rect cr = cm.rect;
            Ray ray = cm.ViewportPointToRay(v);
            int mask = ~(1 << maskIdx);
            if (Physics.Raycast(ray, out hit, 99999, mask)) {
                // Transform objectHit = hit.transform;
                // Debug.DrawRay(hit.point, ray.direction * 10, Color.yellow);
                return new PointInfo() {
                    goted = true,
                    point = hit.point
                };
            }
            return new PointInfo() {
                goted = false
            };
        }

        public class ViewPortVertex {
            public PointInfo lt, lb, rt, rb;

            public PointInfo[] getPoints() {
                return new PointInfo[] { lt, lb, rt, rb };
            }

        }

        public struct PointInfo {
            public Vector3 point;
            public bool goted;
        }


    }
}