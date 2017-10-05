using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace surfm.tool {
    [System.Serializable]
    public class TriggerEvent {


        public UnityEvent emptyEvent;

        [System.Serializable]
        public class TransformEvent : UnityEvent<Transform> { }
        public Transform transData;
        public TransformEvent transformEvent;

        [System.Serializable]
        public class Vector3Event : UnityEvent<Vector3> { }
        public Vector3 vecData;
        public Vector3Event vecEvent;

        [System.Serializable]
        public class IntEvent : UnityEvent<int> { }
        public int intData;
        public IntEvent intEvent;

        [System.Serializable]
        public class FloatEvent : UnityEvent<float> { }
        public int floatData;
        public IntEvent floatEvent;

        [System.Serializable]
        public class StringEvent : UnityEvent<string> { }
        public string stringData;
        public StringEvent stringEvent;



        public void invoke() {
            if (emptyEvent != null) emptyEvent.Invoke();
            if (transformEvent != null) transformEvent.Invoke(transData);
            if (vecEvent != null) vecEvent.Invoke(vecData);
            if (intEvent != null) intEvent.Invoke(intData);
            if (floatEvent != null) floatEvent.Invoke(floatData);
            if (stringEvent != null) stringEvent.Invoke(stringData);

        }



    }
}
