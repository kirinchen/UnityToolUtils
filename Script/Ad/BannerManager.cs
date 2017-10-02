using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace surfm.tool.ad {
    public class BannerManager : MonoBehaviour {

        private static BannerManager instance;

        void Awake() {
            if (instance != null) {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(this);
        }

        void Start() {
            Admober.requestBanner();
        }

    }
}
