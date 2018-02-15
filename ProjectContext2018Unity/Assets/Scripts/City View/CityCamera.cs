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

        public CityCameraController controller;
        public AudioSource audioSource;
        public CameraShake cameraShaker;

        private void Awake() {
            instance = this;
        }
    }
}