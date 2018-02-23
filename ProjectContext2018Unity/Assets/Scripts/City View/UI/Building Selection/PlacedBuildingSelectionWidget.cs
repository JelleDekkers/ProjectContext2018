﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CityView.UI {

    public class PlacedBuildingSelectionWidget : MonoBehaviour {

        [SerializeField] private Text nameTxt;
        [SerializeField] private Text pollutionAmountTxt;
        [SerializeField] private PlacedBuildingSelectionResourceGridItem resourceItemPrefab;
        [SerializeField] private GridLayoutGroup productionGrid;
        [SerializeField] private Vector3 posOffset;

        private Building selectedBuilding;

        private void Start() {
            gameObject.SetActive(false);
            CityCameraInputHandler.OnBuildingSelected += Activate;
        }

        private void Activate(Building building) {
            if (building == null || building == selectedBuilding) {
                gameObject.SetActive(false);
                selectedBuilding = null;
                return;
            }

            transform.position = building.transform.position + posOffset;
            gameObject.SetActive(true);
            selectedBuilding = building;
            FillValues();
        }

        private void FillValues() {
            nameTxt.text = selectedBuilding.data.Name;
            pollutionAmountTxt.text = selectedBuilding.data.Pollution.ToString();

            if (productionGrid.transform.childCount > 0) {
                foreach (Transform t in productionGrid.transform) 
                    Destroy(t.gameObject);
            }

            for(int i = 0; i < selectedBuilding.data.Incomeresources.Length; i++) {
                Sprite sprite = DataManager.ResourcePrefabs.GetSprite(selectedBuilding.data.Incomeresources[i]);
                Instantiate(resourceItemPrefab, productionGrid.transform).Init(sprite, selectedBuilding.data.Incomeresourcesamount[i]);
            }
        }

        public void ToggleProduction() {
            selectedBuilding.enabled = !selectedBuilding.enabled;
        }

        public void DestroySelectedBuilding() {
            // desrtoy gameobject
            // call destroy event
        }
    }
}