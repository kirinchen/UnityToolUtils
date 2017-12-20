# if ListViewAdapter
using frame8.Logic.Misc.Visual.UI.ScrollRectItemsAdapter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace surfm.tool {
    public abstract class ItemHolder : BaseItemViewsHolder {

        public abstract Type getKeyType();



        public override void CollectViews() {
            base.CollectViews();
            injectViews(root);
        }


        internal abstract void injectViews(RectTransform root);
        /// <summary>Called to update the views from the specified model. Overriden by inheritors to update their own views after casting the model to its known type</summary>
        internal abstract void UpdateViews(object model);

        internal bool CanPresentModelType(object p) {
            return p.GetType().Equals(getKeyType());
        }

    }
}
#endif
