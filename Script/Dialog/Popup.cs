﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace surfm.tool {
    public class Popup : MonoBehaviour {

        public GAui gaui;
        public bool showed { get; private set; }

        public virtual void Awake() {
            if (gaui == null) {
                gaui = GetComponent<GAui>();
            }
        }

        public void show(bool b) {
            if (b != showed) {
                showed = b;
                if (showed) {
                    gaui.MoveIn(GUIAnimSystem.eGUIMove.Self);
                } else {
                    gaui.MoveOut(GUIAnimSystem.eGUIMove.Self);
                }
            }
        }
    }
}