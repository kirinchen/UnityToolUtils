using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace surfm.tool {
    public class UnityUtils : MonoBehaviour {
        public static void takeCameraShot(Camera camera, int resWidth, int resHeight, string filename) {

            RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
            camera.targetTexture = rt;
            Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            camera.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            camera.targetTexture = null;
            RenderTexture.active = null; // JC: added to avoid errors
            Destroy(rt);
            byte[] bytes = screenShot.EncodeToPNG();
            System.IO.FileInfo file = new System.IO.FileInfo(filename);
            file.Directory.Create();
            System.IO.File.WriteAllBytes(filename, bytes);
            Debug.Log(string.Format("Took screenshot to: {0}  exit={1} w={2} h={3} ", filename, System.IO.File.Exists(filename), resWidth, resHeight));

        }

        public static void takeCameraShot(Camera camera, string filename) {
            float resHeight = Screen.currentResolution.height;
            float resWidth = Screen.currentResolution.width;

            takeCameraShot(camera, (int)resWidth, (int)resHeight, filename);
        }

        public static EventTrigger.Entry addEventTrigger(EventTrigger eventTrigger, UnityAction<BaseEventData> action, EventTriggerType triggerType) {
            // Create a nee TriggerEvent and add a listener
            EventTrigger.TriggerEvent trigger = new EventTrigger.TriggerEvent();
            trigger.AddListener(action); // you can capture and pass the event data to the listener

            // Create and initialise EventTrigger.Entry using the created TriggerEvent
            EventTrigger.Entry entry = new EventTrigger.Entry() { callback = trigger, eventID = triggerType };

            // Add the EventTrigger.Entry to delegates list on the EventTrigger
            eventTrigger.triggers.Add(entry);
            return entry;
        }

        public static Coroutine delay(MonoBehaviour mb, System.Action a, float it) {
            return mb.StartCoroutine(_delay(a, it));
        }

        public static Coroutine loop(MonoBehaviour mb, System.Action a, float start, float it) {
            return mb.StartCoroutine(_loop(a, start, it));
        }

        private static IEnumerator _loop(System.Action a, float start, float it) {
            yield return new WaitForSeconds(start);
            while (true) {
                a();
                yield return new WaitForSeconds(it);
            }
        }

        private static IEnumerator _delay(System.Action a, float it) {
            yield return new WaitForSeconds(it);
            a();
        }

        public static void setAsync(MonoBehaviour mb, Task task, System.Action cb) {
            mb.StartCoroutine(setAsync(task, cb));
        }

        public static IEnumerator setAsync(Task task, System.Action cb) {
            yield return new WaitUntil(() => task.IsCompleted);
            if (task.IsFaulted) {
                throw task.Exception;
            } else {
                cb();
            }
        }

    }
}
