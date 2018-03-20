using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EarthView {

    public class EarthCameraController : MonoBehaviour {

        [SerializeField] private bool canZoom = true;
        [SerializeField] private float zoomSpeed = 10;
        [SerializeField] private float zoomMin, zoomMax;

        private new Camera camera;
        private Ray ray;
        private RaycastHit hit;

        private void Awake() {
            camera = GetComponent<Camera>();
        }

        private void Update() {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (canZoom)
                Zoom();

            if (Input.GetMouseButtonDown(0))
                OnClick();
        }

        private void OnClick() {
            ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, float.MaxValue)) {
                if (hit.collider.gameObject.GetComponent<CityObject>())
                    hit.collider.gameObject.GetComponent<CityObject>().Select();
            }
        }

        private void Zoom() {
            if (Input.GetAxis("Mouse ScrollWheel") != 0) {
                float zCoordinate = Mathf.Clamp(transform.position.z - Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, zoomMin, zoomMax);
                transform.position = new Vector3(transform.position.x, transform.position.y, zCoordinate);
            }
        }
    }
}