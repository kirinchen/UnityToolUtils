using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace surfm.tool {
    public class ItemView {

        [System.Serializable]
        public struct D {

            public string type;
            public RectTransform prefab;

        }

        public float width { get; private set; }
        public float height { get; private set; }
        public Type type { get; private set; }
        public RectTransform prefab { get; private set; }

        public ItemView(D d) {
            type = Type.GetType(d.type);
            prefab = d.prefab;
            width = prefab.rect.width;
            height = prefab.rect.height;
        }

    }
}
