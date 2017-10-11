using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace surfm.tool.ad {
    public class AdConfig : MonoBehaviour {
        private static AdConfig instance;
        [Header("Admob Settings")]
        public string admobBannerEditorKey;
        public string admobBannerAndroidKey;
        public string admobBannerIosKey;
        public string admobBannerExKey;

        public string admobInterstitialEditorKey;
        public string admobInterstitialAndroidKey;
        public string admobInterstitialIosKey;
        public string admobInterstitialExKey;


        [Header("UnityADS Settings")]
        public string unityAdsIosGameId;
        public string unityAdsAndroidGameId;
        public List<UnityAds.KeyPlacementId> unityKeyPlacementIds = new List<UnityAds.KeyPlacementId>();
        public float unityAdsWaitAdTime = 2f;

        internal string getUnityKeyPlacement(string key) {
            if (string.IsNullOrEmpty(key)) return null;
            if (unityKeyPlacementIds.Count <= 0) return null;
            return unityKeyPlacementIds.Find(k => { return string.Equals(k.key, key); }).id;
        }

        internal bool isEnableunityAd() {
#if UNITY_EDITOR
            return !string.IsNullOrEmpty(unityAdsAndroidGameId);
#elif UNITY_ANDROID
        return !string.IsNullOrEmpty(unityAdsAndroidGameId);
#elif UNITY_IPHONE
        return !string.IsNullOrEmpty(unityAdsIosGameId);
#else
        return null;
#endif
        }

        internal string getUnityAdsGameId() {
#if UNITY_EDITOR
            return unityAdsAndroidGameId;
#elif UNITY_ANDROID
        return unityAdsAndroidGameId;
#elif UNITY_IPHONE
        return unityAdsIosGameId;
#else
             return null;
#endif
        }

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

        //public UnityAds.KeyPlacementId get

        public static AdConfig getInstance() {
            if (instance == null) {
                instance = Resources.Load<AdConfig>("AdConfig");
            }
            return instance;
        }

    }
}
