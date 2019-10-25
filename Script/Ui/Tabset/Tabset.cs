using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace surfm.tool {
    public class Tabset : MonoBehaviour {

        public List<Tab> tabs;
        public Tab currentSelect;
        public TriggerEvent.BoolEvent onSelectedEvent;
        public InitCallBack initActions { get; private set; } = new InitCallBack();


        public void Start() {
            initTabs();
            initActions.done();
        }

        public void initTabs() {
            if(tabs==null) tabs = new List<Tab>(GetComponentsInChildren<Tab>());
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

        public void reflesh() {
            tabs.ForEach(t => {
                t.setSelectUi(t == currentSelect);
            });
            if (currentSelect != null) {
                onSelectedEvent.Invoke(true);
            }
        }

    }
}
