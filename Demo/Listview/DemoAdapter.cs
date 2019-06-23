using System.Collections.Generic;
using frame8.Logic.Misc.Visual.UI.ScrollRectItemsAdapter;
using UnityEngine;
using UnityEngine.UI;

namespace surfm.tool.test {

    public class DemoAdapter : BaseAdapter<SampleObjectModel, MyItemViewsHolder> {

        private RectTransform itemPrefab;
        private List<SampleObjectModel> data = new List<SampleObjectModel>();

        public DemoAdapter( RectTransform rt) {
            itemPrefab = rt;
            for (int i=0;i<100;i++) {
                data.Add(new SampleObjectModel(i+"."));
            }
        }

        public MyItemViewsHolder createViewsHolder(BaseParams _Params, int itemIndex) {
            var instance = new MyItemViewsHolder();
            instance.Init(itemPrefab, itemIndex);

            return instance;
        }

        public List<SampleObjectModel> getData() {
            return data;
        }

        public Rect getRect() {
            return itemPrefab.rect;
        }

        public void updateViewsHolder(SampleObjectModel dataModel, MyItemViewsHolder newOrRecycled, int itemIndex) {
            newOrRecycled.objectTitle.text = dataModel.objectName;
            newOrRecycled.a.color = dataModel.aColor;
            newOrRecycled.b.color = dataModel.bColor;
            newOrRecycled.c.color = dataModel.cColor;
            newOrRecycled.d.color = dataModel.dColor;
            newOrRecycled.e.color = dataModel.eColor;
        }
    }


    public class SampleObjectModel {
        public string objectName;
        public Color aColor, bColor, cColor, dColor, eColor;
        public bool expanded;

        public SampleObjectModel(string name) {
            objectName = name;
            aColor = GetRandomColor();
            bColor = GetRandomColor();
            cColor = GetRandomColor();
            dColor = GetRandomColor();
            eColor = GetRandomColor();
        }

        Color GetRandomColor() {
            return new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
        }
    }

    public sealed class MyItemViewsHolder : BaseItemViewsHolder {
        public Text objectTitle;
        public Image a, b, c, d, e;

        public override void CollectViews() {
            base.CollectViews();

            a = root.GetChild(0).GetChild(0).GetComponent<Image>();
            b = root.GetChild(0).GetChild(1).GetComponent<Image>();
            c = root.GetChild(0).GetChild(2).GetComponent<Image>();
            d = root.GetChild(0).GetChild(3).GetComponent<Image>();
            e = root.GetChild(0).GetChild(4).GetComponent<Image>();

            objectTitle = root.GetChild(0).GetChild(5).GetComponentInChildren<Text>();

        }
    }

}
