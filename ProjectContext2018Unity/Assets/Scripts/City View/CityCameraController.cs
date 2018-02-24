using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView {

    public class CityCameraController : MonoBehaviour {

        public float movementSpeed = 1f;

        [SerializeField]
        private float horizontalBorderMin, horizontalBorderMax;
        [SerializeField]
        private float verticalBorderMin, verticalBorderMax;
        [SerializeField]
        private float zoomMin, zoomMax;

        private void Update() {
            Movement();
            Zoom();
        }

        private void Movement() {
            if (Input.GetMouseButton(1)) {
                float horizontal = Input.GetAxis("Mouse X") / 2 + Input.GetAxis("Mouse Y") / 2;
                float vertical = Input.GetAxis("Mouse X") / 2 - Input.GetAxis("Mouse Y") / 2;
                transform.Translate(-horizontal * movementSpeed * Time.deltaTime, 0, -vertical * movementSpeed * -Time.deltaTime, Space.World);

                // border:
                float horizontalPositionClamped = Mathf.Clamp(transform.position.x, horizontalBorderMin, horizontalBorderMax);
                float verticalPositionClamped = Mathf.Clamp(transform.position.z, verticalBorderMin, verticalBorderMax);
                transform.position = new Vector3(horizontalPositionClamped, transform.position.y, verticalPositionClamped);
            }
        }

        private void Zoom() {
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - Input.GetAxis("Mouse ScrollWheel"), zoomMin, zoomMax);
        }
    }
}