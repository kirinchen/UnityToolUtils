using UnityEngine;
using static surfm.tool.ListView;

namespace surfm.tool.test {

    public class TestListView : MonoBehaviour {
        public RectTransform itemPrefab;
        public ListView listView;
        public DemoAdapter demoAdapter;

        void Start() {
            //listView.setAdapter
            demoAdapter = new DemoAdapter(itemPrefab);
            AdapterCtrl<SampleObjectModel, MyItemViewsHolder> ac = listView.setAdapter(demoAdapter);
        }

    }
}