using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace surfm.tool {
    public class ChooseDialog : DialogKit.Dialog {

        public Text titleText;
         public ListView listView { get; private set; }

        protected override void Awake() {
            base.Awake();
            listView = UnityUtils.getComponentByName<Transform>(gameObject,"Content").gameObject.AddComponent<ListView>();
        }

        public ChooseDialog setTitle(string t) {
            titleText.text = t;
            return this;
        }

        public ChooseDialog setData(List<Data> ds) {
            listView.setData(ds);
            return this;
        }

        public class ListView : GridList<Data, Item> {

            public List<Data> datas { get; private set; }

            public override void Awake() {
                UnityUtils.getComponentByName<Transform>(gameObject, "Item").gameObject.AddComponent<Item>();
                base.Awake();
            }

            public override List<Data> listData() {
                return datas;
            }

            internal override void refleshTile(Data d, Item e, int idx) {
                if (!e.gameObject.activeInHierarchy) e.gameObject.SetActive(true);
                e.setData(d);
            }

            internal void setData(List<Data> ds) {
                datas = ds;
                reflesh();
            }

            internal override void setEmplty(Item row) {
                row.gameObject.SetActive(false);
            }
        }

        public class Item : MonoBehaviour {
            public Text labelText;

            void Awake() {
                labelText = GetComponentInChildren<Text>();
            }

            internal void setData(Data d) {
                labelText.text = d.getLabel();
            }
        }

        public interface Data {
            string getLabel();
        }

    }
}