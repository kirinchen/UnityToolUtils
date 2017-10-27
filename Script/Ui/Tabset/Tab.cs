using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace surfm.tool {
    public class Tab : MonoBehaviour {
        public Button triggerButton;
        public bool oned { get; private set; }
        private Tabset tabset;
        public TriggerEvent.BoolEvent selectEvent;

        void Awake() {
            if (triggerButton == null) {
                triggerButton = GetComponent<Button>();
            }
        }

        internal void init(Tabset tb) {
            tabset = tb;
            triggerButton.onClick.AddListener(onTabClick);
        }

        public void onTabClick() {

            tabset.onChanageTab(this);
        }

        internal void setSelectUi(bool v) {
            selectEvent.Invoke(v);
        }
    }
}