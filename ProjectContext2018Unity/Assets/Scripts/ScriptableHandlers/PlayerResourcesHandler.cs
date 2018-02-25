﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResourcesHandler : MonoBehaviour {

    [SerializeField] private PlayerResources resources;

    private void Start() {
        CityView.Building.OnProductionCycleCompleted += resources.ProcessBuildingProductionResult;
    }

    private void OnDestroy() {
        CityView.Building.OnProductionCycleCompleted -= resources.ProcessBuildingProductionResult;
    }
}