# if ListViewAdapter
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace surfm.tool.demo {
    public class DemoAdapter : BaseAdapter {

        public List<string> data = new List<string>();

        public DemoAdapter() {
            for (int i = 0; i < 999; i++) {
                data.Add("AA " + i);
            }
        }

        public override int getCount() {
            return data.Count;
        }

        public override List<object> listData() {
            return data.ConvertAll(o => { return (object)o; });
        }

        internal override ItemHolder genHolder(object d, int itemIndex) {
            return new Holder();
        }

        public class Holder : ItemHolder {

            public Text text;

            public override Type getKeyType() {
                return typeof(string);
            }

            internal override void injectViews(RectTransform root) {
                text = root.Find("Text").GetComponent<Text>();
            }

            internal override void UpdateViews(object model) {
                text.text = (string)model;
            }
        }
    }
}
#endif
