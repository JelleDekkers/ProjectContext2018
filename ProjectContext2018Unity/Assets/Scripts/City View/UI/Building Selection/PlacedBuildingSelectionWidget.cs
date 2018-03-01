using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CityView.UI {

    public class PlacedBuildingSelectionWidget : MonoBehaviour {

        [SerializeField] private Text nameTxt;
        [SerializeField] private Text pollutionAmountTxt;
        [SerializeField] private ProductAmountItem resourceItemPrefab;
        [SerializeField] private GridLayoutGroup productionGrid;
        [SerializeField] private Vector3 posOffset;
        [SerializeField] private Selectable selectable;

        private Building selectedBuilding;

        private void Start() {
            gameObject.SetActive(false);
            CityCameraInputHandler.OnPlacedBuildingSelected += Activate;
        }

        private void OnEnable() {
            selectable.Select();
        }

        private void OnDisable() {
            selectedBuilding = null;
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
            productionGrid.transform.RemoveChildren();

            nameTxt.text = selectedBuilding.data.Name;
            pollutionAmountTxt.text = selectedBuilding.data.Pollution.ToString();

            Sprite sprite = DataManager.ResourcePrefabs.MoneySprite;
            Instantiate(resourceItemPrefab, productionGrid.transform).Init(sprite, selectedBuilding.data.Moneyoutput);

            for (int i = 0; i < selectedBuilding.data.Resourceoutput.Length; i++) {
                sprite = DataManager.ResourcePrefabs.GetResourceSprite(selectedBuilding.data.Resourceoutput[i]);
                Instantiate(resourceItemPrefab, productionGrid.transform).Init(sprite, selectedBuilding.data.Resourceoutput[i]);
            }
        }

        public void ToggleProduction() {
            selectedBuilding.enabled = !selectedBuilding.enabled;
        }

        public void DestroySelectedBuilding() {
            Building.OnDemolishInitiated(selectedBuilding);
            selectedBuilding.enabled = false;
            gameObject.SetActive(false);
        }
    }
}