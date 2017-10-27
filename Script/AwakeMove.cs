using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace surfm.tool {
    public class AwakeMove : MonoBehaviour {
        public Vector3 toPos;
        void Awake() {
            transform.localPosition = toPos;
        }

    }
}
