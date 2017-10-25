using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
namespace surfm.tool {
    public class NistService : MonoBehaviour {
        private static NistService instance;

        public enum State {
            Padding, OK, Fail
        }
        private State state = State.Padding;
        private URestApi rest;
        private float _lastCheckAt;
        private DateTime _lastCheckTime;
        private List<Action> initedListeners = new List<Action>();


        private void init() {
            rest = GetComponent<URestApi>();
            fetch(d => {
                Debug.Log("NistService init=" + d + " it`s " + state);
                initedListeners.ForEach(a => { a(); });
                initedListeners = null;
            });
        }

        private void fetch(Action<DateTime> cb) {
            rest.get("/actualtime.cgi?lzbc=siqm9b", html => {
                string time = Regex.Match(html, @"(?<=\btime="")[^""]*").Value;
                double milliseconds = Convert.ToInt64(time) / 1000.0;
                _lastCheckTime = new DateTime(1970, 1, 1).AddMilliseconds(milliseconds);
                _lastCheckAt = Time.time;
                state = State.OK;
                cb(_lastCheckTime);
            }, (a, b, c, d) => {
                state = State.Fail;
                _lastCheckTime = DateTime.UtcNow;
                cb(_lastCheckTime);
            });
        }

        private void fixCurrentUtcTime() {
            float dt = Time.time - _lastCheckAt;
            _lastCheckAt = Time.time;
            _lastCheckTime.AddSeconds(dt);
        }


        public DateTime getCurrentUtcTime() {
            if (state == State.OK) {
                fixCurrentUtcTime();
                return _lastCheckTime;
            } else {
                if (state == State.Fail) {
                    fetch(d => { });
                }
                return DateTime.UtcNow;
            }
        }

        public void fetchCurrentTime(Action<DateTime> cb) {
            if (state == State.OK) {
                fixCurrentUtcTime();
                cb(_lastCheckTime);
            } else {
                fetch(cb);
            }

        }

        public void addInitListener(Action a) {
            if (initedListeners == null) {
                a();
            } else {
                initedListeners.Add(a);
            }
        }

        public static NistService getInstance() {
            if (instance == null) {
                instance = Resources.Load<NistService>("@NistService");
                instance.init();
            }
            return instance;
        }

    }
}
