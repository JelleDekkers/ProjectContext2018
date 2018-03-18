using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RaycastHelper {

    public const float MAX_RAY_DISTANCE = 100000f;

    public static Vector3 GetMousePositionInScene(out bool isHitting, float y = 0) {
        Plane plane = new Plane(Vector3.up, y);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance = 0f;
        if (plane.Raycast(ray, out distance)) {
            Vector3 hitPoint = ray.GetPoint(distance);
            isHitting = true;
            return hitPoint;
        }

        isHitting = false;
        return Vector3.zero;
    }
}
