using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace surfm.tool.ad {
    public class AdManager : MonoBehaviour {

        public static AdManager instance { get; private set; }
        private InterstitialAd interstitialAd;

        void Awake() {
            if (instance != null) {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(this);
        }

        void Start() {
            interstitialAd = Admober.requestInterstitial();
            Admober.requestBanner();
        }

        public bool showInterstitialAd() {
            if (interstitialAd.IsLoaded()) {
                interstitialAd.Show();
                return true;
            } else {
                return false;
            }
        }

    }
}
