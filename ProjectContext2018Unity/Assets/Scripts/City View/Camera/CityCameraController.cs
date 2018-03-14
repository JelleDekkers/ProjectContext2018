using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView {

    public class CityCameraController : MonoBehaviour {

        [SerializeField] private bool canZoom, canRotate;
        [SerializeField] private float movementSpeed = 30;
        [SerializeField] private float horizontalBorderMin, horizontalBorderMax;
        [SerializeField] private float verticalBorderMin, verticalBorderMax;
        [SerializeField] private float zoomMin, zoomMax;
        [SerializeField] private float zoomSpeed = 1;

        private Vector3 startPosition;

        private void Start() {
            startPosition = transform.position;
        }

        private void Update() {
            Movement();

            if(canZoom)
                Zoom();

            if(canRotate)
                Rotate();
        }

        private void Movement() {
            if (Input.GetMouseButton(1)) {
                float horizontal = Input.GetAxis("Mouse X") / 2 + Input.GetAxis("Mouse Y") / 2;
                float vertical = Input.GetAxis("Mouse X") / 2 - Input.GetAxis("Mouse Y") / 2;
                transform.Translate(-horizontal * movementSpeed * Time.deltaTime, 0, -vertical * movementSpeed * -Time.deltaTime, Space.Self);

                // border:
                float horizontalPositionClamped = Mathf.Clamp(transform.position.x, startPosition.x + horizontalBorderMin, startPosition.x + horizontalBorderMax);
                float verticalPositionClamped = Mathf.Clamp(transform.position.z, startPosition.z + verticalBorderMin, startPosition.z + verticalBorderMax);
                transform.position = new Vector3(horizontalPositionClamped, transform.position.y, verticalPositionClamped);
            }
        }

        private void Zoom() {
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
                Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, zoomMin, zoomMax);
        }

        private void Rotate() {
            if (Input.GetKeyDown(KeyCode.Q)) 
                transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y + 90, 0);
            else if (Input.GetKeyDown(KeyCode.E)) 
                transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y - 90, 0);
        }
    }
}