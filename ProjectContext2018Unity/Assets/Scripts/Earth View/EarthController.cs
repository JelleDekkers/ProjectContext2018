using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EarthView {

    public class EarthController : MonoBehaviour {

        public float rotationSpeed = 2;

        private void Update() {
            if (EventSystem.current.IsPointerOverGameObject() == false && Input.GetMouseButton(0)) {
                transform.Rotate(0, (Input.GetAxis("Mouse X") * rotationSpeed * -Time.deltaTime), 0, Space.World);
            }
        }
    }
}