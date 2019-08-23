using BitBenderGames;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace surfm.tool {

    [RequireComponent(typeof(EventTrigger))]
    public class TouchDisable : MonoBehaviour {

        private EventTrigger trigger;
        //private static TouchInputController touchInputController;

        void Awake() {
            trigger = GetComponent<EventTrigger>();
            trigger.AddEventTrigger(onPointerEnter, EventTriggerType.PointerEnter);
            trigger.AddEventTrigger(onPointerExit, EventTriggerType.PointerExit);
        }



        private void onPointerExit(BaseEventData arg0) {

            TouchDisableTaskPool.getInstance().touchEnable.Value = true;
        }

        private void onPointerEnter(BaseEventData arg0) {
            //Debug.Log("onPointerEnter ctl="+ touchInputController);
            TouchDisableTaskPool.getInstance().touchEnable.Value = false;
        }
    }

    public class TouchDisableTaskPool : MonoBehaviour {

        private static TouchDisableTaskPool instance;

        public List<Action<bool>> actions { get; private set; } = new List<Action<bool>>();
        public IReactiveProperty<bool> touchEnable { get; private set; } = new ReactiveProperty<bool>(true);

        private void init() {
            touchEnable.Subscribe(b => {
                actions.ForEach(a => a(b));
            });
        }
        

        public static TouchDisableTaskPool getInstance() {
            if (!instance) {
                GameObject go = new GameObject(typeof(TouchDisableTaskPool).ToString());
                instance= go.AddComponent<TouchDisableTaskPool>();
            }
            return instance;
        }

    }
}
