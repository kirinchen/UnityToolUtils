using frame8.Logic.Misc.Visual.UI.MonoBehaviours;
using frame8.Logic.Misc.Visual.UI.ScrollRectItemsAdapter;
using frame8.ScrollRectItemsAdapter.Util;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace surfm.tool {
    /// <summary>
    /// <para>The main example implementation demonstrating common (not all) functionalities: </para>
    /// <para>- using both a horizontal and a vertical ScrollRect with a complex prefab, </para>
    /// <para>- changing the item count, </para>
    /// <para>- expanding/collapsing an item, </para>
    /// <para>- smooth scrolling to an item &amp; optionally doing an action after the animation is done, </para>
    /// <para>- random item sizes,</para>
    /// <para>- comparing the performance to the default implementation of a ScrollView,</para>
    /// <para>- the use of <see cref="ScrollbarFixer8"/></para>
    /// <para>At the core, everything's the same as in other example implementations, so if something's not clear, check them (SimpleTutorial is a good start)</para>
    /// </summary>
    public class ListView : MonoBehaviour {
#pragma warning disable 0649
        [SerializeField]
        //MyParams _ScrollRectAdapterParams;
#pragma warning restore 0649

        //MyScrollRectItemsAdapter _ScrollRectItemsAdapter;
        //List<SampleObjectModel> _Data;

        public RectOffset contentPadding = new RectOffset();
        public float contentSpacing;
        public BaseParams Params { get; private set; } = new BaseParams();
        public AdapterHandler Adapter { get; private set; }
        //public List<SampleObjectModel> Data { get { return _Data; } }


        void Awake() {
            Params.scrollRect = GetComponent<ScrollRect>();
            Params.viewport = Params.scrollRect.viewport;
            Params.content = Params.scrollRect.content;
            Params.contentPadding = contentPadding;
            Params.contentSpacing = contentSpacing;
        }

        public AdapterCtrl<DATA,VH> setAdapter<DATA,VH>(BaseAdapter<DATA,VH> ba) where VH : BaseItemViewsHolder {
            // Wait for Unity's layout (UI scaling etc.)

            Adapter = new AdapterCtrl<DATA, VH>(ba, Params);
            //_ScrollRectAdapterParams.updateItemsButton.onClick.AddListener(UpdateItems);

            // Initially set the number of items to the number in the input field
            UpdateItems();
            return (AdapterCtrl<DATA, VH>)Adapter;
        }

        void OnDestroy() {
            if (Adapter != null)
                Adapter.Dispose();
        }

        // This is your data model
        // this one will generate 5 random colors
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
            public ExpandCollapseOnClick expandOnCollapseComponent;

            public override void CollectViews() {
                base.CollectViews();

                a = root.GetChild(0).GetChild(0).GetComponent<Image>();
                b = root.GetChild(0).GetChild(1).GetComponent<Image>();
                c = root.GetChild(0).GetChild(2).GetComponent<Image>();
                d = root.GetChild(0).GetChild(3).GetComponent<Image>();
                e = root.GetChild(0).GetChild(4).GetComponent<Image>();

                objectTitle = root.GetChild(0).GetChild(5).GetComponentInChildren<Text>();

                expandOnCollapseComponent = root.GetComponent<ExpandCollapseOnClick>();
            }
        }

        public void UpdateItems() {

            Adapter.updateItems();
        }



        public interface AdapterHandler {
            void Dispose();
            void updateItems();
        }

        /// <summary>
        /// <para>At the core, it's the same as <see cref="SimpleTutorial"/>, but it also re-generates the <see cref="_ItemsSizessToUse"/></para>
        /// <para>each time the count changes, optionally inserting random sizes if the "Randomize sizes" toggle is checked. And the data lsit is passed directly in the constructor rather than having it in <see cref="Params"/></para>
        /// </summary>
        public sealed class AdapterCtrl<DATA,VH> : ScrollRectItemsAdapter8<BaseParams, VH>,  AdapterHandler
            where VH : BaseItemViewsHolder {


            private BaseAdapter<DATA,VH> baseAdapter;

            //bool _RandomizeSizes;
            float _PrefabSize;
            //float[] _ItemsSizessToUse;
            List<DATA> _Data { get { return baseAdapter.getData(); } }


            public AdapterCtrl(BaseAdapter<DATA, VH> b, BaseParams parms) {
                baseAdapter = b;
                if (parms.scrollRect.horizontal)
                    _PrefabSize = b.getRect().width;
                else
                    _PrefabSize = b.getRect().height;


                //parms.randomizeSizesToggle.onValueChanged.AddListener((value) => _RandomizeSizes = value);

                // Need to call Init(Params) AFTER we init our stuff, because both GetItem[Height|Width]() and UpdateViewsHolder() will be called in this method
                Init(parms);
            }

            // Remember, only GetItemHeight (for vertical scroll) or GetItemWidth (for horizontal scroll) will be called
            protected override float GetItemHeight(int index) {
                Debug.Log("GetItemHeight:" + _PrefabSize);
                return _PrefabSize;
            }

            // Remember, only GetItemHeight (for vertical scroll) or GetItemWidth (for horizontal scroll) will be called
            protected override float GetItemWidth(int index) {
                Debug.Log("GetItemWidth:" + _PrefabSize);
                return _PrefabSize;
            }

            protected override VH CreateViewsHolder(int itemIndex) {
                return baseAdapter.createViewsHolder(_Params, itemIndex);
            }

            protected override void UpdateViewsHolder(VH newOrRecycled) {
                // Populating with data from associated model
                DATA dataModel = _Data[newOrRecycled.itemIndex];
                baseAdapter.updateViewsHolder(dataModel, newOrRecycled, newOrRecycled.itemIndex);
            }

            public void updateItems() {
                int size = _Data.Count;
                _Data.Capacity = size;
                ChangeItemCountTo(size);
            }
        }
    }
}
