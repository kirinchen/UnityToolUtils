using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace surfm.tool {
    public class DynamicBoxDemo : MonoBehaviour {

        public InputField inputField;
        public Text text;


        void Update() {
            text.text = inputField.text;
        }
    }
}
