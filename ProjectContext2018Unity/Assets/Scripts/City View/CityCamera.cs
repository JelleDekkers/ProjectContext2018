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

        public float movementSpeed = 1f;
        public AudioSource audioSource;
        public CameraShake cameraShaker;

        private Ray r;

        private void Awake() {
            instance = this;
        }

        private void Update() {
            Movement();
        }

        private void Movement() {
            r = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Input.GetMouseButton(0)) {
                float horizontal = Input.GetAxis("Mouse X") / 2 + Input.GetAxis("Mouse Y") / 2;
                float vertical = Input.GetAxis("Mouse X") / 2 - Input.GetAxis("Mouse Y") / 2;
                transform.Translate(-horizontal * movementSpeed * Time.deltaTime, 0, -vertical * movementSpeed * -Time.deltaTime, Space.World);
            }
        }
    }
}