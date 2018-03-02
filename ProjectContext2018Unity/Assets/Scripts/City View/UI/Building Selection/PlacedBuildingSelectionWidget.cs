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
        [SerializeField] private GridLayoutGroup outputGrid;
        [SerializeField] private GridLayoutGroup inputGrid;
        [SerializeField] private Vector3 posOffset;
        [SerializeField] private Selectable selectable;
        [SerializeField] private Image productionCycleImg;

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

        private void Update() {
            productionCycleImg.fillAmount = 1 - selectedBuilding.ProductionCycle.Timer / selectedBuilding.data.Productiontime;
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

            InitInput();
            InitOutput();
        }

        private void InitInput() {
            inputGrid.transform.RemoveChildren();
            Sprite sprite;
            if (selectedBuilding.data.Moneyinput > 0) {
                sprite = DataManager.ResourcePrefabs.MoneySprite;
                Instantiate(resourceItemPrefab, inputGrid.transform).Init(sprite, selectedBuilding.data.Moneyinput);
            }

            for (int i = 0; i < selectedBuilding.data.Resourceinput.Length; i++) {
                sprite = DataManager.ResourcePrefabs.GetResourceSprite(selectedBuilding.data.Resourceinput[i]);
                Instantiate(resourceItemPrefab, inputGrid.transform).Init(sprite, selectedBuilding.data.Resourceinputamount[i]);
            }
        }

        private void InitOutput() {
            outputGrid.transform.RemoveChildren();
            Sprite sprite;
            if (selectedBuilding.data.Moneyoutput > 0) {
                sprite = DataManager.ResourcePrefabs.MoneySprite;
                Instantiate(resourceItemPrefab, outputGrid.transform).Init(sprite, selectedBuilding.data.Moneyoutput);
            }

            for (int i = 0; i < selectedBuilding.data.Resourceoutput.Length; i++) {
                sprite = DataManager.ResourcePrefabs.GetResourceSprite(selectedBuilding.data.Resourceoutput[i]);
                Instantiate(resourceItemPrefab, outputGrid.transform).Init(sprite, selectedBuilding.data.Resourceoutputamount[i]);
            }
        }

        public void ToggleProduction() {
            Debug.Log("toggle");
            selectedBuilding.enabled = !selectedBuilding.enabled;
        }

        public void DestroySelectedBuilding() {
            Building.OnDemolishInitiated(selectedBuilding);
            selectedBuilding.enabled = false;
            gameObject.SetActive(false);
        }
    }
}