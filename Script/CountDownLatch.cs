using System.Threading;
using UnityEngine;

namespace surfm.tool {

    public class CountDownLatch {
        private object lockObj = new Object();
        private int counter;

        public CountDownLatch(int counter) {
            this.counter = counter;
        }

        public void Await() {
            lock (lockObj) {
                while (counter > 0) {
                    Monitor.Wait(lockObj);
                }
            }
        }

        public void CountDown() {
            lock (lockObj) {
                counter--;
                Monitor.PulseAll(lockObj);
            }
        }
    }

}
