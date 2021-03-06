﻿/*
Copyright 2015 Pim de Witte All Rights Reserved.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace surfm.tool {


    public class UnityMainThreadDispatcher : MonoBehaviour {

        private static readonly Queue<Action> _executionQueue = new Queue<Action>();

        public void Update() {
            lock (_executionQueue) {
                while (_executionQueue.Count > 0) {
                    _executionQueue.Dequeue().Invoke();
                }
            }
        }

        /// <summary>
        /// Locks the queue and adds the IEnumerator to the queue
        /// </summary>
        /// <param name="action">IEnumerator function that will be executed from the main thread.</param>
        public void Enqueue(IEnumerator action) {
            lock (_executionQueue) {
                _executionQueue.Enqueue(() => {
                    StartCoroutine(action);
                });
            }
        }

        /// <summary>
        /// Locks the queue and adds the Action to the queue
        /// </summary>
        /// <param name="action">function that will be executed from the main thread.</param>
        public void Enqueue(Action action) {
            Enqueue(ActionWrapper(action));
        }
        IEnumerator ActionWrapper(Action a) {
            a();
            yield return null;
        }


        private static UnityMainThreadDispatcher _instance = null;


        public static UnityMainThreadDispatcher instance() {
            if (_instance == null) {
                _instance = new GameObject("@UnityMainThreadDispatcher").AddComponent<UnityMainThreadDispatcher>();
            }
            return _instance;
        }

        public static void uniRxRun(Action a) {
            var heavyMethod2 = Observable.Start(() => {
                return 0;
            });
            // Join and await two other thread values
            Observable.WhenAll(heavyMethod2)
                .ObserveOnMainThread() // return to main thread
                .Subscribe(xs => {
                    a();
                });
        }



        void OnDestroy() {
            _instance = null;
        }


    }
}