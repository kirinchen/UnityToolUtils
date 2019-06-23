using frame8.Logic.Misc.Visual.UI.ScrollRectItemsAdapter;
using System.Collections.Generic;
using UnityEngine;

namespace surfm.tool {

    public interface BaseAdapter<DATA,VH> where VH : BaseItemViewsHolder {

        Rect getRect();
        List<DATA> getData();
        VH createViewsHolder(BaseParams _Params, int itemIndex);
        void updateViewsHolder(DATA dataModel, VH newOrRecycled, int itemIndex) ;
    }
}
