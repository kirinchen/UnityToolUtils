
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace surfm.tool.ad {
    public class AdManager : MonoBehaviour {

        public static AdManager instance { get; private set; }


        void Awake() {
            if (instance != null) {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(this);
        }

        void Start() {

        }

        public bool showInterstitialAd() {

            return false;
        }

    }
}
