using frame8.Logic.Misc.Visual.UI.ScrollRectItemsAdapter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace surfm.tool {
    public class ListView : MonoBehaviour {

        public BaseParams parms;
        public List<ItemView.D> prefabList;
        public BaseAdapter adapter { get; private set; }

        public void setAdapter(BaseAdapter ba) {
            adapter = ba;
            adapter.init(prefabList, parms);
            adapter.notifyDataChanage();
        }

    }
}
