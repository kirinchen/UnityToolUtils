using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace surfm.tool.demo {
    public class DemoAdapter : BaseAdapter<string> {

        public List<string> data = new List<string>();

        public DemoAdapter() {
            for (int i = 0; i < 999; i++) {
                data.Add("AA " + i);
            }
        }

        public override int getCount() {
            return data.Count;
        }

        public override List<string> listData() {
            return data;
        }

        internal override ItemHolder<string> genHolder(string d, int itemIndex) {
            return new Holder();
        }

        public class Holder : ItemHolder<string> {

            public Text text;

            public override Type getKeyType() {
                return typeof(string);
            }

            internal override void injectViews(RectTransform root) {
                text = root.Find("Text").GetComponent<Text>();
            }

            internal override void UpdateViews(string model) {
                text.text = model;
            }
        }
    }
}
