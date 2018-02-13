using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    public float shakeStrength = 1;
    public float duration = 0.4f;

    public AnimationCurve verticalCurve;
   
    [SerializeField]
    private Camera cam;

    private void Start() {
        cam = GetComponent<Camera>();
    }

    public void Shake() {
        StopAllCoroutines();
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine() {
        float timer = 0;
        Vector3 originalPosition = cam.transform.position;
        while(timer < duration) {
            float verticalAmount = verticalCurve.Evaluate(timer / duration) * shakeStrength;

            Vector3 pos = cam.transform.position;
            pos.y = originalPosition.y + verticalAmount;
            cam.transform.position = pos;
            timer += Time.deltaTime;
            yield return null;
        }
        cam.transform.position = originalPosition;
    }
}