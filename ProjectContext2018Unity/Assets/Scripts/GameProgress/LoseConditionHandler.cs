using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseConditionHandler : MonoBehaviour {
    private float polutionTemperatureLimit = 40;

	// Use this for initialization
	private void Start () {
        WorldTemperature.OnWorldTemperatureChanged += CheckLoseCondition;
    }

    // Update is called once per frame
    private void Update () {
		
	}

    private void CheckLoseCondition(float temperature) {
        if(temperature >= polutionTemperatureLimit) {
            Debug.Log("GAME LOST");
            // IMPLEMENT LOSE GAME METHOD HERE
        }
    }
}
