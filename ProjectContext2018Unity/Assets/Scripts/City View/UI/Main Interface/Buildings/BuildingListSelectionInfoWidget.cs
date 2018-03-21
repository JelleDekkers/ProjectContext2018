using System;
using UnityEngine;
using UnityEngine.UI;
using UI;

namespace CityView.UI {

    public class BuildingListSelectionInfoWidget : MonoBehaviour {

        [SerializeField] private Text nameTxt;
        [SerializeField] private Text pollutionAmountTxt, climateTxt;
        [SerializeField] private ProductAmountItem productItemPrefab;
        [SerializeField] private GridLayoutGroup costGrid;
        [SerializeField] private GridLayoutGroup productionGrid;
        [SerializeField] private GridLayoutGroup inputGrid;
        [SerializeField] private Text productionTime;
        [SerializeField] private Vector3 posOffset;
        [SerializeField] private Color correctClimateColor, incorrectClimateColor;

        private void DisableGameObject() {
            gameObject.SetActive(false);
        }

        public void SubscribeToBuildingWidget() {
            BuildingSelectionWidgetItem.OnPointerEnterEvent += UpdateInfo;
            BuildingSelectionWidgetItem.OnPointerExitEvent += DisableGameObject;
        }

        public void UnSubscribeToBuildingWidget() {
            BuildingSelectionWidgetItem.OnPointerEnterEvent -= UpdateInfo;
            BuildingSelectionWidgetItem.OnPointerExitEvent -= DisableGameObject;
        }

        private void UpdateInfo(int id, System.Object data) {
            BuildingsData selectedBuildingData = data as BuildingsData;

            gameObject.SetActive(true);
            costGrid.transform.RemoveChildren();
            productionGrid.transform.RemoveChildren();
            inputGrid.transform.RemoveChildren();

            nameTxt.text = selectedBuildingData.Name;
            pollutionAmountTxt.text = selectedBuildingData.Pollution.ToString();
            productionTime.text = selectedBuildingData.Productiontime.ToString() + " seconds";

            climateTxt.gameObject.SetActive(selectedBuildingData.Climate != Climate.None);
            if (selectedBuildingData.Climate != Climate.None)
                climateTxt.text = selectedBuildingData.Climate.ToString();
            
            if (selectedBuildingData.Climate != Player.LocalPlayer.ClimateType)
                climateTxt.color = incorrectClimateColor;
            else
                climateTxt.color = correctClimateColor;

            // cost:
            Sprite sprite;
            if (selectedBuildingData.Moneycost > 0) {
                sprite = DataManager.ResourcePrefabs.MoneySprite;
                Instantiate(productItemPrefab, costGrid.transform).Init(sprite, selectedBuildingData.Moneycost);
            }

            for (int i = 0; i < selectedBuildingData.Resourcecost.Length; i++) {
                sprite = DataManager.ResourcePrefabs.GetResourceSprite(selectedBuildingData.Resourcecost[i]);
                Instantiate(productItemPrefab, costGrid.transform).Init(sprite, selectedBuildingData.Resourcecostamount[i]);
            }

            // input:
            if (selectedBuildingData.Moneyinput > 0) {
                sprite = DataManager.ResourcePrefabs.MoneySprite;
                Instantiate(productItemPrefab, inputGrid.transform).Init(sprite, selectedBuildingData.Moneyinput);
            }

            for (int i = 0; i < selectedBuildingData.Resourceinput.Length; i++) {
                sprite = DataManager.ResourcePrefabs.GetResourceSprite(selectedBuildingData.Resourceinput[i]);
                Instantiate(productItemPrefab, inputGrid.transform).Init(sprite, selectedBuildingData.Resourceinputamount[i]);
            }


            // production:
            if (selectedBuildingData.Moneyoutput > 0) {
                sprite = DataManager.ResourcePrefabs.MoneySprite;
                Instantiate(productItemPrefab, productionGrid.transform).Init(sprite, selectedBuildingData.Moneyoutput);
            }

            for (int i = 0; i < selectedBuildingData.Resourceoutput.Length; i++) {
                sprite = DataManager.ResourcePrefabs.GetResourceSprite(selectedBuildingData.Resourceoutput[i]);
                Instantiate(productItemPrefab, productionGrid.transform).Init(sprite, selectedBuildingData.Resourceoutputamount[i]);
            }

        }

        private void OnDestroy() {
            BuildingSelectionWidgetItem.OnPointerEnterEvent -= UpdateInfo;
            BuildingSelectionWidgetItem.OnPointerExitEvent -= DisableGameObject;
        }
    }
}