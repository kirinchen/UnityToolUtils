using frame8.Logic.Misc.Visual.UI.ScrollRectItemsAdapter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace surfm.tool {
    public class ListView : MonoBehaviour {

        public BaseParams parms;
        public List<ItemView.D> prefabList;


        public void setAdapter<T>(BaseAdapter<T> ba) {
            ba.init(prefabList, parms);
            ba.notifyDataChanage();
        }

    }
}
