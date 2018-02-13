using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView {

    public class CityCamera : MonoBehaviour {
        private static CityCamera instance;
        public static CityCamera Instance {
            get {
                if (instance == null)
                    instance = FindObjectOfType<CityCamera>();
                return instance;
            }
        }

        public AudioSource audioSource;
        public CameraShake cameraShaker;

        private void Awake() {
            instance = this;
        }

        private void Update() {
            if(Input.GetKeyDown(KeyCode.Space))
                cameraShaker.Shake();
        }
    }
}