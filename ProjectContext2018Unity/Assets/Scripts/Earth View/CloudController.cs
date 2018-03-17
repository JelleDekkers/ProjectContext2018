using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour {

    [SerializeField] private float rotateSpeed = 10;
    [SerializeField] private Vector3 direction = Vector3.right;

    private void Update() {
        transform.Rotate(direction * rotateSpeed * Time.deltaTime);
    }
}
