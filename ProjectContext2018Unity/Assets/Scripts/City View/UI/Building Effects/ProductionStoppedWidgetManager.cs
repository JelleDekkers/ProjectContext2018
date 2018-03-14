using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.UI {

    public class ProductionStoppedWidgetManager : MonoBehaviour {

        [SerializeField] private AnimationCurve movementCurve;
        [SerializeField] private AnimationCurve scaleCurve;
        [SerializeField] private float spawnHeight = 0;
        [SerializeField] private ProductionStoppedWidget itemPrefab;

        private void Start() {
            Building.OnProductionStopped += CreateNewItem;
        }

        private void CreateNewItem(Building building) {
            Vector3 spawnPos = building.transform.position;
            spawnPos.y += spawnHeight;
            ProductionStoppedWidget item = Instantiate(itemPrefab, spawnPos, Quaternion.identity, transform);
        }
    }
}