using frame8.Logic.Misc.Visual.UI.ScrollRectItemsAdapter;
using frame8.ScrollRectItemsAdapter.MultiplePrefabsExample.ViewHolders;
using frame8.ScrollRectItemsAdapter.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace surfm.tool {
    public abstract class BaseAdapter : ScrollRectItemsAdapter8<BaseParams, ItemHolder> {



        private Dictionary<Type, ItemView> prefabMap = new Dictionary<Type, ItemView>();

        public abstract List<object> listData();

        internal void init(List<ItemView.D> l, BaseParams p) {
            Init(p);
            l.ForEach(b => {
                ItemView iv = new ItemView(b);
                prefabMap.Add(iv.type, iv);
            });
        }


        public void notifyDataChanage() {
            ChangeItemCountTo(getCount());
        }

        protected override ItemHolder CreateViewsHolder(int itemIndex) {
            object d = listData()[itemIndex];
            ItemHolder ans = genHolder(d, itemIndex);
            RectTransform rect = prefabMap[d.GetType()].prefab;
            ans.Init(rect, itemIndex);
            return ans;
        }


        public abstract int getCount();
        internal abstract ItemHolder genHolder(object d, int itemIndex);

        protected override float GetItemHeight(int index) {
            return prefabMap[listData()[index].GetType()].height;
        }

        protected override float GetItemWidth(int index) {
            return prefabMap[listData()[index].GetType()].width;
        }

        protected override bool IsRecyclable(ItemHolder potentiallyRecyclable, int indexOfItemThatWillBecomeVisible, float heightOfItemThatWillBecomeVisible) {
            return potentiallyRecyclable.CanPresentModelType(listData()[indexOfItemThatWillBecomeVisible]);
        }

        protected override void UpdateViewsHolder(ItemHolder newOrRecycled) {
            object model = listData()[newOrRecycled.itemIndex];
            newOrRecycled.UpdateViews(model/*, _Sizes[newOrRecycled.itemIndex]*/);
        }
    }
}
