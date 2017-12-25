# if ListViewAdapter
using frame8.Logic.Misc.Visual.UI.ScrollRectItemsAdapter;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace surfm.tool {
    public class ListView : MonoBehaviour {
#if ListViewAdapter
        public BaseParams parms;
        public List<ItemView.D> prefabList;
        public BaseAdapter adapter { get; private set; }

        public void setAdapter(BaseAdapter ba) {
            adapter = ba;
            adapter.init(prefabList, parms);
            adapter.notifyDataChanage();
        }
#endif

    }
}
