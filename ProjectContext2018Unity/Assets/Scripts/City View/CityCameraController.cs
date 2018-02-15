using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityCameraController : MonoBehaviour {

    public float movementSpeed = 1f;

    [SerializeField]
    private float horizontalBorderMin, horizontalBorderMax;
    [SerializeField]
    private float verticalBorderMin, verticalBorderMax;

    private Ray ray;

    private void Update() {
        Movement();
    }

    private void Movement() {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
}
