using System;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace surfm.tool {

    public class NavPath {
        public NavMeshPathStatus status;
        public Vector3[] corners;

        public override int GetHashCode() {
            int prime = 31;
            int result = 1;
            result = prime * result + CommUtils.GetHashCode(corners);
            result = prime * result + status.GetHashCode();
            return result;
        }


        public override bool Equals(object obj) {
            if (this == obj)
                return true;
            if (obj == null)
                return false;
            if (GetType() != obj.GetType())
                return false;
            NavPath other = (NavPath)obj;
            if (!Enumerable.SequenceEqual(corners, other.corners))
                return false;
            if (status != other.status)
                return false;
            return true;
        }

    }

    public static class NavPathEx {
        public static NavPath toNavPath(this NavMeshPath nmp) {
            return CommUtils.convert<NavPath>(nmp);
        }

    }


    [RequireComponent(typeof(NavMeshAgent))]
    public class NavClickMover : MonoBehaviour {

        public LayerMask mask;
        public NavMeshAgent agent { get; private set; }
        public IReactiveProperty<Vector3> targetPos { get; private set; } = new ReactiveProperty<Vector3>(Vector3.zero);
        private IReactiveProperty<float> moveSpeed = new ReactiveProperty<float>(7f);
        public IReactiveProperty<NavPath> path { get; private set; } = new ReactiveProperty<NavPath>(null);
        public Func<bool> onBeforeMove = () => true;
        private LineRenderer pathLine;


        void Awake() {
            agent = GetComponent<NavMeshAgent>();
            pathLine = GetComponent<LineRenderer>();
        }

        void Start() {
            targetPos.Value = transform.position;
            moveSpeed.Subscribe(mv => agent.speed = mv);
            targetPos.Subscribe(p => {
                agent.SetDestination(p);
            });
        }


        protected virtual void Update() {
            onNewMoveTo();
            drawPath();
            
                path.Value = agent.path.toNavPath();
              
        }

        private void drawPath() {
            NavMeshPath path = agent.path;
            pathLine.enabled = path.corners.Length >= 2;
            if (!pathLine.enabled) return;

            pathLine.numCornerVertices = (path.corners.Length);
            for (var i = 0; i < path.corners.Length; i++) {
                pathLine.SetPosition(i, path.corners[i]); //go through each corner and set that to the line renderer's position
            }
        }


        private void onNewMoveTo() {
            if (!Input.GetMouseButtonUp(0)) return;
            if (!onBeforeMove()) return;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, mask)) {
                targetPos.Value = hit.point;
            }
        }

    }
}
