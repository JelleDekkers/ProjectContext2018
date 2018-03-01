using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseConditionHandler : MonoBehaviour {
    float polutionLimit = 40;

	// Use this for initialization
	void Start () {
        PlayerResources.OnResourceChanged += Lose;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Lose(int a, int b)
    {

    }
}
