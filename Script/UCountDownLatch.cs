using System;
using UniRx;
using UnityEngine;

namespace surfm.tool {

    public class UCountDownLatch {
        public int counter { get; private set; }
        private Action action = ()=> { };

        public UCountDownLatch() {
        }

        public UCountDownLatch(int counter) {
            this.counter = counter;
        }

        public IObservable<Unit> AsObservable() {
            return Observable.FromEvent<Action>(h => { return h; }, h => {
                action += h;
            } , h => action -= (h));
        }

        public void setCount(int c) {
            counter = c;
        }

        public void await(Action a) {
            action = a;
            triggerAction();
        }

        public void CountDown() {
            counter--;
            triggerAction();
        }

        private void triggerAction() {
            if (counter == 0) {
                try {
                    action();
                } catch (Exception e) {
                    Debug.LogError(e);
                }
            }
            if (counter < 0) {
                throw new System.Exception("can not counter <0");
            }
        }



    }

}
