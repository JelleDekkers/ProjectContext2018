using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationHandler : MonoBehaviour {

    [SerializeField] private Population population;

    private void Start() {
        population.Init();
    }

    private void OnDestroy() {
        population.Reset();
    }

}
