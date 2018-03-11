using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CityView.UI {

    public class PlacedClimateBuildingSelectionWidget : MonoBehaviour {

        [SerializeField] private Text nameTxt;
        [SerializeField] private Text descriptionTxt;
        [SerializeField] private Vector3 posOffset;
        [SerializeField] private Selectable selectable;
        [SerializeField] private Material overlayMaterial;

        private ClimateBuilding selectedBuilding;

        private void Start() {
            gameObject.SetActive(false);
            CityCameraInputHandler.OnPlacedClimateBuildingSelected += Activate;
        }

        private void OnEnable() {
            selectable.Select();
        }

        private void OnDisable() {
            selectedBuilding = null;
        }

        private void Activate(ClimateBuilding building) {
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
            descriptionTxt.text = selectedBuilding.data.Description;
        }

        public void DestroySelectedBuilding() {
            Building.OnDemolishInitiated(selectedBuilding);
            selectedBuilding.enabled = false;
            gameObject.SetActive(false);
        }
    }
}