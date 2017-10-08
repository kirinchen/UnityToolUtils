using frame8.Logic.Misc.Visual.UI.ScrollRectItemsAdapter;
using frame8.ScrollRectItemsAdapter.MultiplePrefabsExample.ViewHolders;
using frame8.ScrollRectItemsAdapter.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace surfm.tool {
    public abstract class BaseAdapter<DATA> : ScrollRectItemsAdapter8<BaseParams, ItemHolder<DATA>> {



        private Dictionary<Type, ItemView> prefabMap = new Dictionary<Type, ItemView>();

        public abstract List<DATA> listData();

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

        protected override ItemHolder<DATA> CreateViewsHolder(int itemIndex) {
            DATA d = listData()[itemIndex];
            ItemHolder<DATA> ans = genHolder(d, itemIndex);
            RectTransform rect = prefabMap[d.GetType()].prefab;
            ans.Init(rect, itemIndex);
            return ans;
        }


        public abstract int getCount();
        internal abstract ItemHolder<DATA> genHolder(DATA d, int itemIndex);

        protected override float GetItemHeight(int index) {
            return prefabMap[listData()[index].GetType()].height;
        }

        protected override float GetItemWidth(int index) {
            return prefabMap[listData()[index].GetType()].width;
        }

        protected override bool IsRecyclable(ItemHolder<DATA> potentiallyRecyclable, int indexOfItemThatWillBecomeVisible, float heightOfItemThatWillBecomeVisible) {
            return potentiallyRecyclable.CanPresentModelType(listData()[indexOfItemThatWillBecomeVisible]);
        }

        protected override void UpdateViewsHolder(ItemHolder<DATA> newOrRecycled) {
            DATA model = listData()[newOrRecycled.itemIndex];
            newOrRecycled.UpdateViews(model/*, _Sizes[newOrRecycled.itemIndex]*/);
        }
    }
}
