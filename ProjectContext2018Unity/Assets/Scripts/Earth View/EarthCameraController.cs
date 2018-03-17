using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EarthView {

    public class EarthCameraController : MonoBehaviour {

        [SerializeField] private bool canZoom = true;
        [SerializeField] private float zoomSpeed = 10;
        [SerializeField] private float zoomMin, zoomMax;

        private void Update() {
            if (canZoom)
                Zoom();
        }

        private void Zoom() {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (Input.GetAxis("Mouse ScrollWheel") != 0) {
                float zCoordinate = Mathf.Clamp(transform.position.z - Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, zoomMin, zoomMax);
                transform.position = new Vector3(transform.position.x, transform.position.y, zCoordinate);
            }
        }
    }
}