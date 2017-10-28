using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace surfm.tool {
    public class ChooseGridList : GridList<ChooseDialog.RowData, ChooseGridItem> {
        private ChooseDialog dialog;
      

        new  void Awake() {
            base.Awake();
            dialog = GetComponentInParent<ChooseDialog>();
        
        }

        public override List<ChooseDialog.RowData> listData() {
            return dialog.listData();
        }

        internal override void refleshTile(ChooseDialog.RowData d, ChooseGridItem e) {
            e.setData(d);
        }

        internal override void setEmplty(ChooseGridItem row) {
            row.setEmplty();
        }
    }
}
