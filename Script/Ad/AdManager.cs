
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace surfm.tool.ad {
    public class AdManager : MonoBehaviour {

        public static AdManager instance { get; private set; }
        private VideoAd unityAds;

        void Awake() {
            if (instance != null) {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(this);
            initUnityAds();
        }

        private void initUnityAds() {
            if (!AdConfig.getInstance().isEnableunityAd()) return;
            unityAds = gameObject.AddComponent<UnityAds>();
        }


        public void showVideo(string key = "", Action<bool, object> cb = null) {
            unityAds.showAd(key, cb);
        }

        public bool showInterstitialAd() {

            return false;
        }

        public interface VideoAd {
            void showAd(string key = "", Action<bool, object> cb = null);
        }

    }
}
