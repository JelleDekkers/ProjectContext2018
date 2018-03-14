﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.UI {

    public class ProductionStoppedWidgetManager : MonoBehaviour {

        [SerializeField] private float spawnHeight = 0;
        [SerializeField] private ProductionStoppedWidget widgetPrefab;

        private void Start() {
            Building.OnProductionStopped += CreateNewItem;
        }

        private void OnDestroy() {
            Building.OnProductionStopped -= CreateNewItem;
        }

        private void CreateNewItem(Building building) {
            Vector3 spawnPos = building.transform.position;
            spawnPos.y += spawnHeight;
            ProductionStoppedWidget widget = Instantiate(widgetPrefab);
            widget.transform.SetParent(transform, false);
            widget.transform.position = new Vector3(building.transform.position.x, building.transform.position.y + spawnHeight, building.transform.position.z);
            widget.Init(building);
        }
    }
}