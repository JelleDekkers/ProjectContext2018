using System;
using UnityEngine;
using UnityEngine.UI;

namespace CityView.UI {

    public class BuildingListSelectionWidget : MonoBehaviour {

        [SerializeField] private Text nameTxt;
        [SerializeField] private Text pollutionAmountTxt;
        [SerializeField] private ProductAmountItem productItemPrefab;
        [SerializeField] private GridLayoutGroup costGrid;
        [SerializeField] private GridLayoutGroup productionGrid;
        [SerializeField] private Text productionTime;
        [SerializeField] private Vector3 posOffset;

        private Action DisableGameObject;

        private void Start() {
            DisableGameObject = delegate () { gameObject.SetActive(false); };
            BuildingSelectionWidgetItem.OnPointerEnterEvent += UpdateInfo;
            BuildingSelectionWidgetItem.OnPointerExitEvent += DisableGameObject;
            DisableGameObject();
        }

        private void UpdateInfo(int id) {
            gameObject.SetActive(true);
            costGrid.transform.RemoveChildren();
            productionGrid.transform.RemoveChildren();

            BuildingsData selectedBuildingData = DataManager.BuildingData.dataArray[id];
            nameTxt.text = selectedBuildingData.Name;
            pollutionAmountTxt.text = selectedBuildingData.Pollution.ToString();
            productionTime.text = selectedBuildingData.Productiontime.ToString();
            
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