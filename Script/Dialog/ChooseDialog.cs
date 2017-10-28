using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace surfm.tool {
    public class ChooseDialog : Dialog {

        public static ChooseDialog instance { get; private set; }
        public struct RowData {
            public Sprite sprite;
            public string text;
        }

        private ChooseGridList gridList;
        private List<RowData> data = new List<RowData>();
        private GridLayoutGroup group;
        private RectTransform groupRectT;
        private Tabset tabset;

        public override void Awake() {
            base.Awake();
            instance = this;
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

    }
}
