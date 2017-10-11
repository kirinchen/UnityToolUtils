using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

namespace surfm.tool.ad {
    public class UnityAds : MonoBehaviour, AdManager.VideoAd {

        [System.Serializable]
        public class KeyPlacementId {
            public string key;
            public string id;
            public string desc;
        }

        void Awake() {
            string gid = AdConfig.getInstance().getUnityAdsGameId();
            Advertisement.Initialize(gid, true);
        }

        public void showAd(string key = "", Action<bool, object> cb = null) {
            StartCoroutine(ShowAdWhenReady(key, cb));
        }

        IEnumerator ShowAdWhenReady(string key = "", Action<bool, object> cb = null) {

            string zone = AdConfig.getInstance().getUnityKeyPlacement(key);
            float stT = Time.time;
            while (!Advertisement.IsReady(zone) && (Time.time-stT) <= AdConfig.getInstance().unityAdsWaitAdTime) {
                yield return new WaitForSeconds(0.35f);
            }

            ShowOptions options = new ShowOptions();
            if (cb != null) {
                options.resultCallback = r => {
                    cb(r == ShowResult.Finished, r);
                };
            }
            Advertisement.Show(zone, options);
        }


    }
}
