using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace surfm.tool {
    public class NavSurfaceLoader : MonoBehaviour {
        private NavMeshSurface surface;
        public string dataPath = "NavMesh-Terrain";


        void Awake() {
            surface = GetComponent<NavMeshSurface>();
        }

        void Update() {
            if(Input.GetKeyUp(KeyCode.T)) {
                setData();
            }
        }

        public void setData() {
            NavMeshData data = Resources.Load<NavMeshData>(dataPath);
            surface.navMeshData = data;
            surface.AddData();
        }

    }
}
