using BestHTTP;
using Newtonsoft.Json;
using surfm.tool;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace com.surfm.rest {
    public class URestApi : MonoBehaviour {

        public class Result {

            public bool succeed { get; internal set; }
            public string errorMsg { get; internal set; }
            public HTTPRequestStates states { get; internal set; }
            public HTTPResponse response { get; internal set; }
            public Exception exception { get; internal set; }

            internal Result onError(string error, HTTPRequestStates s, HTTPResponse resp, Exception e) {
                errorMsg = error;
                states = s;
                response = resp;
                exception = e;
                succeed = false;
                return this;
            }

            internal Result onOk(HTTPResponse resp) {
                response = resp;
                succeed = true;
                return this;
            }

            public T getBody<T>() {
                return JsonConvert.DeserializeObject<T>(response.DataAsText, ObscuredValueConverter.DEFAULT);
            }


            public override string ToString() {
                return JsonConvert.SerializeObject(this, ObscuredValueConverter.DEFAULT);
            }

        }


        public string host;
        public string port;
        public float timeOut = 5f;
        private int genId;
        private Dictionary<int, RequestBundle> map = new Dictionary<int, RequestBundle>();
        public Func<string> authorizationFunc = () => { return ""; };



        void Start() { }

        public void abortAll() {
            foreach (RequestBundle rb in map.Values) {
                rb.www.Abort();
                rb.www.Dispose();
            }
            map.Clear();
        }

        public int get<T>(string url, Action<TypeResult<T>> cb) {
            return get(url, r => { cb(new TypeResult<T>(r)); });
        }

        public int get(string url, Action<Result> cb) {
            Uri u = new Uri(getUrl(url));
            Debug.Log("get = " + u);
            HTTPRequest hr = new HTTPRequest(u, HTTPMethods.Get);
            return runWWWW(hr, cb);
        }

        private int runWWWW(HTTPRequest hr, Action<Result> onOk) {
            setupHeaders(hr);
            OnFinishedHandler oh = new OnFinishedHandler(onOk);
            hr.Callback = oh.onFinished;
            hr.ConnectTimeout = new TimeSpan((long)(10000000 * timeOut));
            int id = getId();
            RequestBundle rb = new RequestBundle(hr);
            map.Add(id, rb);
            hr.Send();
            return id;
        }

        class OnFinishedHandler {
            private Result result = new Result();
            private Action<Result> callback = null;
            public OnFinishedHandler(Action<Result> cb) { callback = cb; }

            public void onFinished(HTTPRequest req, HTTPResponse resp) {
                Debug.Log("onFinished");
                // Increase the finished count regardless of the state of our request
                string msg = "";
                switch (req.State) {
                    // The request finished without any problem.
                    case HTTPRequestStates.Finished:
                        if (resp.IsSuccess) {
                            try {
                                callback(result.onOk(resp));
                            } catch (Exception e) {
                                callback(result.onError(msg, req.State, resp, e));
                            }
                        } else {
                            msg =
                           (string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                           resp.StatusCode,
                                                           resp.Message,
                                                           resp.DataAsText));
                            callback(result.onError(msg, req.State, resp, new RestException(resp, msg)));
                        }
                        break;
                    // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                    case HTTPRequestStates.Error:
                        msg = ("Request Finished with Error! " + (req.Exception != null ? (req.Exception.Message + "\n" + req.Exception.StackTrace) : "No Exception"));
                        callback(result.onError(msg, req.State, resp, new RestException(resp, msg)));
                        break;

                    // The request aborted, initiated by the user.
                    case HTTPRequestStates.Aborted:
                        msg = ("Request Aborted!");
                        callback(result.onError(msg, req.State, resp, new RestException(resp, msg)));
                        break;

                    // Ceonnecting to the server is timed out.
                    case HTTPRequestStates.ConnectionTimedOut:
                        msg = ("Connection Timed Out!");
                        callback(result.onError(msg, req.State, resp, new RestException(resp, msg)));
                        break;

                    // The request didn't finished in the given time.
                    case HTTPRequestStates.TimedOut:
                        msg = ("Processing the request Timed Out!");
                        callback(result.onError(msg, req.State, resp, new RestException(resp, msg)));
                        break;
                }
            }

        }



        public void abort(int id) {
            if (map.ContainsKey(id)) {
                RequestBundle rb = map[id];
                rb.www.Abort();
                rb.www.Dispose();
                map.Remove(id);
            }
        }

        private int getId() {
            return genId++;
        }

        public string getUrl(string urlorPath) {
            if (host != null && host.Length > 0) {
                if (string.IsNullOrEmpty(port)) {
                    return "http://" + host + "/" + urlorPath;
                } else {
                    return "http://" + host + ":" + port + "/" + urlorPath;
                }
            } else {
                return urlorPath;
            }
        }

        public int postJson<T>(string url, object data, Action<TypeResult<T>> cb) {
            return postJson(url, data, r => { cb(new TypeResult<T>(r)); });
        }

        public int postJson(string url, object data, Action<Result> cb) {
            Uri u = new Uri(getUrl(url));
            Debug.Log("postJson = " + u);
            HTTPRequest hr = new HTTPRequest(u, HTTPMethods.Post);
            if (data != null) {
                string ourPostData = JsonConvert.SerializeObject(data, ObscuredValueConverter.DEFAULT);
                byte[] pData = Encoding.UTF8.GetBytes(ourPostData.ToCharArray());
                hr.RawData = pData;
            }
            return runWWWW(hr, cb);
        }



        private void setupHeaders(HTTPRequest hr) {
            hr.AddHeader("charset", "utf-8");
            hr.AddHeader("Content-Type", "application/json");
            string auth = authorizationFunc();
            if (!string.IsNullOrWhiteSpace(auth)) {
                hr.AddHeader("Authorization", auth);
            }
        }




        //public static URestApi newInstance(B b) {
        //    if (b.noned) return null;
        //    string s = string.Format("h={0} p={1} t={2} a={3}", b.host, b.port, b.timeOut, b.authorization);
        //    GameObject go = new GameObject(s);
        //    URestApi ans = go.AddComponent<URestApi>();
        //    ans.host = b.host;
        //    ans.port = b.port;
        //    ans.timeOut = b.timeOut;
        //    ans.authorization = b.authorization;
        //    return ans;
        //}

        public class RequestBundle {
            public HTTPRequest www;

            public RequestBundle(HTTPRequest www) {
                this.www = www;
            }
        }
    }
}