using System;
using UnityEngine;
using UnityEngine.UI;
using UI;

namespace CityView.UI {

    public class ClimateBuildingListSelectionInfoWidget : MonoBehaviour {

        [SerializeField] private Text nameTxt;
        [SerializeField] private Text descriptionTxt;
        [SerializeField] private ProductAmountItem productItemPrefab;
        [SerializeField] private GridLayoutGroup costGrid;
        [SerializeField] private Vector3 posOffset;

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
            if (data.GetType() != typeof(ClimateBuildingsData)) {
                Debug.Log("wrong type");
                return;
            }
            ClimateBuildingsData selectedBuildingData = data as ClimateBuildingsData;

            gameObject.SetActive(true);
            costGrid.transform.RemoveChildren();
            nameTxt.text = selectedBuildingData.Name;
            descriptionTxt.text = selectedBuildingData.Description;
            
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
        }

        private void OnDestroy() {
            BuildingSelectionWidgetItem.OnPointerEnterEvent -= UpdateInfo;
            BuildingSelectionWidgetItem.OnPointerExitEvent -= DisableGameObject;
        }
    }
}