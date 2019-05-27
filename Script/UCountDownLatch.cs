using System;
using System.Threading;
using UnityEngine;

namespace surfm.tool {

    public class UCountDownLatch  {
        private int counter;
        private Action action;

        public UCountDownLatch(int counter) {
            this.counter = counter;
        }



        public void await(Action a) {
            triggerAction(a);
            action = a;


        }

        public void CountDown() {
                counter--;
            triggerAction(action);
        }

        private void triggerAction(Action a) {
            if (counter == 0) {
                a();
            }
            if (counter < 0) {
                throw new System.Exception("can not counter <0");
            }
        }



    }

}
