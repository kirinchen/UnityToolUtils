using UnityEngine;


namespace surfm.tool {

    public class CameraEffect : MonoBehaviour {

        private Transform camT;

        void Awake() {

            camT = transform;

        }


        private float camShakeMagnitude;     
        public float shakeMultiplier = 0.5f;		//check the inspector

        //delagates of TDS.onCameraShakeE
        public void CameraShake(float magnitude = 1) {
            if (magnitude == 0) return;
            camShakeMagnitude = magnitude * 0.5f;
        }
        //called from Update()
        public void Shake() {
            if (Time.timeScale == 0) return;    //dont execute if the game is paused

            //randomize the camera transform x and y local position, create a shaking effect
            float x = 2 * (Random.value - 0.5f) * camShakeMagnitude * shakeMultiplier;
            float y = 2 * (Random.value - 0.5f) * camShakeMagnitude * shakeMultiplier;
            camT.localPosition = new Vector3(x, y, camT.localPosition.z);

            //reduce the shake magnitude overtime
            camShakeMagnitude *= (1 - Time.deltaTime * 5);
        }
    }
}
