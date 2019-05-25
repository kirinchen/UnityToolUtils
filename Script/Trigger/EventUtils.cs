using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Events;

namespace surfm.tool {
    public class EventUtils  {

        public static void setButtonClick(Button button, UnityAction unityAction) {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(unityAction);
        }

    }
}