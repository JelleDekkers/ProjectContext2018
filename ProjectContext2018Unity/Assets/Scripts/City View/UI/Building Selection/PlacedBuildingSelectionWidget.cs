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
        [SerializeField] private Material overlayMaterial;

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

            FillGridValues(inputGrid, selectedBuilding.data.Moneyinput, selectedBuilding.data.Resourceinput, selectedBuilding.data.Resourceinputamount);
            FillGridValues(outputGrid, selectedBuilding.data.Moneyoutput, selectedBuilding.data.Resourceoutput, selectedBuilding.data.Resourceoutputamount);
        }

        private void FillGridValues(GridLayoutGroup grid, float money, int[] resources, int[] resourcesAmount) {
            grid.transform.RemoveChildren();
            Sprite sprite;
            if (money > 0) {
                sprite = DataManager.ResourcePrefabs.MoneySprite;
                ProductAmountItem item = Instantiate(resourceItemPrefab, grid.transform);
                item.Init(sprite, money);
                item.SetMaterial(overlayMaterial);
            }

            for (int i = 0; i < resources.Length; i++) {
                sprite = DataManager.ResourcePrefabs.GetResourceSprite(resources[i]);
                ProductAmountItem item = Instantiate(resourceItemPrefab, grid.transform);
                item.Init(sprite, resourcesAmount[i]);
                item.SetMaterial(overlayMaterial);
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