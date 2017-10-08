using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace surfm.tool.demo {
    public class TestTriiger : MonoBehaviour {

        public ListView listview;

        void Start() {
            listview.setAdapter(new DemoAdapter());
        }
    }
}
