using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if Firebase
using Firebase;
using Firebase.Storage;
#endif
using System.Text;

namespace surfm.tool {

    public class FireStorageUtils : MonoBehaviour {
#if Firebase
        public string MyStorageBucket = "gs://xxxx/";
        private static FireStorageUtils instance;
        private DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
        protected FirebaseStorage storage;

        void Awake() {
            instance = this;
        }

        void Start() {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
                dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available) {
                    InitializeFirebase();
                } else {
                    Debug.LogError(
                      "Could not resolve all Firebase dependencies: " + dependencyStatus);
                }
            });
        }

        private void InitializeFirebase() {
            var appBucket = FirebaseApp.DefaultInstance.Options.StorageBucket;
            storage = FirebaseStorage.DefaultInstance;
            if (!String.IsNullOrEmpty(appBucket)) {
                MyStorageBucket = String.Format("gs://{0}/", appBucket);
            }
        }

        public void upload(string path, byte[] bs, Action<StorageMetadata> doneCB, Action<Exception> ecb = null) {
            StartCoroutine(UploadToFirebaseStorage(path, bs, doneCB, ecb));
        }

        public IEnumerator UploadToFirebaseStorage(string path, byte[] bs, Action<StorageMetadata> doneCB, Action<Exception> ecb) {
            string firebaseStorageLocation = MyStorageBucket + path;
            StorageReference reference = FirebaseStorage.DefaultInstance
              .GetReferenceFromUrl(firebaseStorageLocation);
            var task = reference.PutBytesAsync(bs);
            yield return new WaitUntil(() => task.IsCompleted);
            if (task.IsFaulted) {
                if (ecb != null) {
                    ecb(task.Exception);
                } else {
                    throw task.Exception;
                }
            } else {
                doneCB(task.Result);
            }
        }

        public static FireStorageUtils getInstance() {
            return instance;
        }

#endif
    }
}
