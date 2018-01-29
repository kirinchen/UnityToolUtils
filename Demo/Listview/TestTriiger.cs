using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace surfm.tool.demo {
    public class TestTriiger : MonoBehaviour {

# if ListViewAdapter
        public ListView listview;
#endif

        void Start() {
#if ListViewAdapter
            listview.setAdapter(new DemoAdapter());
#endif
        }
    }
}
