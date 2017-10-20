
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
# if Admob
using GoogleMobileAds.Api;
#endif
using System;

namespace surfm.tool.ad {
    public class Admober : MonoBehaviour {

#if Admob
        private InterstitialAd interstitialAd;

        void Start() {
            interstitialAd = requestInterstitial();
            requestBanner();
        }

        public static void requestBanner() {
            string adUnitId = AdConfig.getInstance().getAdmobKey();
            // Create a 320x50 banner at the top of the screen.
            BannerView bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the banner with the request.
            bannerView.LoadAd(request);
        }

        public static InterstitialAd requestInterstitial() {
            string adUnitId = AdConfig.getInstance().getInterstitialKey();

            // Initialize an InterstitialAd.
            InterstitialAd interstitial = new InterstitialAd(adUnitId);
            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the interstitial with the request.
            interstitial.LoadAd(request);
            return interstitial;
        }

        internal bool showInterstitialAd() {
            if (interstitialAd.IsLoaded()) {
                interstitialAd.Show();
                return true;
            } else {
                return false;
            }
        }
#else
        internal bool showInterstitialAd() { return false; }
#endif
    }
}
