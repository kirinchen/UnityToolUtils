﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace surfm.tool {
    public class DialogKit : MonoBehaviour {
        private static DialogKit _instance;

        private List<Dialog> dialogs = new List<Dialog>();

        void Awake() {
            dialogs = new List<Dialog>(GetComponentsInChildren<Dialog>());
        }

        public D show<D>() where D : Dialog {
            close();
            Type t= typeof(D);
            D ans= (D)dialogs.Find(d=> d.GetType().Equals(t));
            ans.show(true);
            return ans;
        }

        public void close() {
            dialogs.ForEach(d=>d.show(false));
        }

        public static DialogKit instance
        {
            get
            {
                if (!_instance) {
                    _instance = GameObject.Instantiate(Resources.Load<DialogKit>("@DialogKit"), new Vector3(0, 999, 0), Quaternion.identity);
                }
                return _instance;
            }
        }

        public abstract class Dialog : PlayInOut {

            public virtual void show(bool b) {
                play(b);
            }

            public virtual void close() {
                instance.close();
            }

        }

    }
}