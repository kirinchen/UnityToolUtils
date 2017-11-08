using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace surfm.tool {
    public class AwakeStartInvoke : MonoBehaviour {

        public TriggerEvent awakeEvent;
        public TriggerEvent startEvent;


        void Awake() {
            awakeEvent.invoke();
        }

        void Start() {
            startEvent.invoke();
        }


    }
}
