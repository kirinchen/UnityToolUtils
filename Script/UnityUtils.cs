using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

            takeCameraShot(camera,(int) resWidth, (int)resHeight, filename);
        }
    }
}
