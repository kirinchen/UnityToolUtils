using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace surfm.tool {
    public class ChooseGridItem : MonoBehaviour {

        public Image titleImg;
        public Text text;
        public ChooseDialog.RowData lastData { get; private set; } 

        internal void setData(ChooseDialog.RowData d) {
            lastData = d;
            gameObject.SetActive(true);
            titleImg.sprite = d.sprite;
            text.text = d.text;
        }

        internal void setEmplty() {
            gameObject.SetActive(false);
        }
    }
}
