using System;
using UnityEngine;

public class WaterLevel : MonoBehaviour {

    public static Action<float> OnLevelIncreased;

    public float increaseAmount = 0.3f;

    public void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
            OnLevelIncreased(increaseAmount);
    }
}
