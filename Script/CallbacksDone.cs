using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace surfm.tool {
    public class CallbacksDone {

        private int cutdownNum = 0;
        private Action<bool> doneCb;

        public CallbacksDone(int i, Action<bool> d) {
            cutdownNum = i;
            doneCb = d;
        }

        public Action<bool> cutdown() {
            cutdownNum++;
            return action;
        }

        public void action(bool b) {
            if (!b) doneCb(false);
            cutdownNum--;
            if (cutdownNum <= 0) {
                doneCb(true);
            }
        }


    }
}
