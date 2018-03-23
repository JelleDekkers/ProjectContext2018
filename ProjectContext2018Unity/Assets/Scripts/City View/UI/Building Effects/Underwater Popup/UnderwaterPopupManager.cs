using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.UI {

    public class UnderwaterPopupManager : MonoBehaviour {

        [SerializeField] private float spawnHeight = 0;
        [SerializeField] private UnderwaterPopup popupPrefab;

        private void Start() {
            Building.OnWaterReachedBuilding += CreateNewItem;
        }

        private void OnDestroy() {
            Building.OnWaterReachedBuilding -= CreateNewItem;
        }

        private void CreateNewItem(Building building) {
            Vector3 spawnPos = building.transform.position;
            spawnPos.y += spawnHeight;
            UnderwaterPopup widget = Instantiate(popupPrefab);
            widget.transform.SetParent(transform, false);
            widget.transform.position = new Vector3(building.transform.position.x, building.transform.position.y + spawnHeight, building.transform.position.z);
            widget.Init(building);
        }
    }
}