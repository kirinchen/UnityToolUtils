#if DOTween
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace surfm.tool {
    public class ChooseDialog : DialogKit.Dialog {

        public Text titleText;
        public ListView listView { get; private set; }
        public Action<Data> onItemChoose;

        protected override void Awake() {
            base.Awake();
            listView = UnityUtils.getComponentByName<Transform>(gameObject, "Content").gameObject.AddComponent<ListView>();
            listView.init(this);
        }

        public ChooseDialog setTitle(string t) {
            titleText.text = t;
            return this;
        }

        public ChooseDialog setData(List<Data> ds) {
            listView.setData(ds);
            return this;
        }

        public ChooseDialog setItemChoose(Action<Data> o) {
            onItemChoose = o;
            return this;
        }

        public class ListView : GridList<Data, Item> {
            private ChooseDialog dialog;
            public List<Data> datas { get; private set; }

            internal void init(ChooseDialog d) {
                dialog = d;
            }

            public override void Awake() {
                UnityUtils.getComponentByName<Transform>(gameObject, "Item").gameObject.AddComponent<Item>();
                base.Awake();
            }

            public override List<Data> listData() {
                return datas;
            }

            internal override void refleshTile(Data d, Item e, int idx) {
                if (!e.gameObject.activeInHierarchy) e.gameObject.SetActive(true);
                e.setData(dialog, d);
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
            public Text labelText { get; private set; }
            public Button button { get; private set; }
            public Data data { get; private set; }
            private ChooseDialog dialog;

            void Awake() {
                labelText = GetComponentInChildren<Text>();
                button = GetComponent<Button>();
            }

            internal void setData(ChooseDialog cd, Data d) {
                dialog = cd;
                data = d;
                labelText.text = d.getLabel();
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(onClick);
            }

            private void onClick() {
                dialog.close();
                dialog.onItemChoose?.Invoke(data);
            }
        }

        public interface Data {
            string getLabel();
        }

    }
}
#endif