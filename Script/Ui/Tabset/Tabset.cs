using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace surfm.tool {
    public class Tabset : MonoBehaviour {

        private List<Tab> tabs;
        public Tab currentSelect;

        public void Awake() {
            tabs = new List<Tab>(GetComponentsInChildren<Tab>());
        }

        public void Start() {
            tabs.ForEach(t => {
                t.init(this);
            });
            reflesh();
        }

        internal void onChanageTab(Tab tab) {
            if (tab != currentSelect) {
                currentSelect = tab;
                reflesh();
            }
        }

        private void reflesh() {
            tabs.ForEach(t => {
                t.setSelectUi(t == currentSelect);
            });
        }

    }
}
