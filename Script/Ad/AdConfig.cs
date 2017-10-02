using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace surfm.tool.ad {
    public class AdConfig : MonoBehaviour {
        private static AdConfig instance;

        public string admobBannerEditorKey;
        public string admobBannerAndroidKey;
        public string admobBannerIosKey;
        public string admobBannerExKey;

        public string admobInterstitialEditorKey;
        public string admobInterstitialAndroidKey;
        public string admobInterstitialIosKey;
        public string admobInterstitialExKey;

        public string getAdmobKey() {
#if UNITY_EDITOR
            return admobBannerEditorKey;
#elif UNITY_ANDROID
        return admobBannerAndroidKey;
#elif UNITY_IPHONE
        return admobBannerIosKey;
#else
        return admobBannerExKey;
#endif
        }

        public string getInterstitialKey() {
#if UNITY_EDITOR
            return admobInterstitialEditorKey;
#elif UNITY_ANDROID
        return admobInterstitialAndroidKey;
#elif UNITY_IPHONE
        return admobInterstitialIosKey;
#else
        return admobInterstitialExKey;
#endif
        }

        public static AdConfig getInstance() {
            if (instance == null) {
                instance = Resources.Load<AdConfig>("AdConfig");
            }
            return instance;
        }

    }
}
