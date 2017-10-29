using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace surfm.tool {
    public class ChooseDialog : Dialog {

      
        public struct RowData {
            public Sprite sprite;
            public string text;
            public object target;
        }

        private ChooseGridList gridList;
        private List<RowData> data = new List<RowData>();
        private GridLayoutGroup group;
        private RectTransform groupRectT;
        private Tabset tabset;
        private Action<bool, RowData> onSelect;

        public override void Awake() {
            base.Awake();
            
            gridList = GetComponentInChildren<ChooseGridList>();
            group = GetComponentInChildren<GridLayoutGroup>();
            tabset = GetComponentInChildren<Tabset>();
            groupRectT = group.GetComponent<RectTransform>();
        }

        internal List<RowData> listData() {
            return data;
        }

        public ChooseDialog setData(List<RowData> data) {
            this.data = data;
            float dy = group.cellSize.y + group.spacing.y;
            float tY = dy * data.Count * 1.1f;
            groupRectT.sizeDelta = new Vector2(groupRectT.sizeDelta.x, tY);
            gridList.reflesh();
            tabset.initTabs();
            return this;
        }

        public ChooseDialog setCallback(Action<bool, RowData> s) {
            onSelect = s;
            return this;
        }

        public void onCancelClick() {
            show(false);
            onSelect(false,default(RowData));
        }

        public void onConfirmClick() {
            show(false);
            ChooseGridItem ci= tabset.currentSelect.GetComponent<ChooseGridItem>();
            onSelect(true, ci.lastData);
        }

    }
}
