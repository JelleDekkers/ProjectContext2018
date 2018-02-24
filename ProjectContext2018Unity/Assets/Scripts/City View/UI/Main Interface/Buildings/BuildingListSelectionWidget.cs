using System.Collections;
using System.Collections.Generic;
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

        private void Start() {
            BuildingSelectionWidget.OnBuildingSelected += UpdateInfo;
        }
        
        private void UpdateInfo(int id) {
            costGrid.transform.RemoveChildren();
            productionGrid.transform.RemoveChildren();

            BuildingsData selectedBuildingData = DataManager.BuildingData.dataArray[id];
            nameTxt.text = selectedBuildingData.Name;
            pollutionAmountTxt.text = selectedBuildingData.Pollution.ToString();
            productionTime.text = selectedBuildingData.Productiontime.ToString();
            
            
            // cost:
            Sprite sprite;
            if (selectedBuildingData.Costmoney > 0) {
                sprite = DataManager.ResourcePrefabs.MoneySprite;
                Instantiate(productItemPrefab, costGrid.transform).Init(sprite, selectedBuildingData.Costmoney);
            }

            for (int i = 0; i < selectedBuildingData.Resourcecost.Length; i++) {
                sprite = DataManager.ResourcePrefabs.GetResourceSprite(selectedBuildingData.Resourcecost[i]);
                Instantiate(productItemPrefab, costGrid.transform).Init(sprite, selectedBuildingData.Resourcecostamount[i]);
            }

            // production:
            if (selectedBuildingData.Incomemoney > 0) {
                sprite = DataManager.ResourcePrefabs.MoneySprite;
                Instantiate(productItemPrefab, productionGrid.transform).Init(sprite, selectedBuildingData.Incomemoney);
            }

            for (int i = 0; i < selectedBuildingData.Incomeresources.Length; i++) {
                sprite = DataManager.ResourcePrefabs.GetResourceSprite(selectedBuildingData.Incomeresources[i]);
                Instantiate(productItemPrefab, productionGrid.transform).Init(sprite, selectedBuildingData.Incomeresourcesamount[i]);
            }

        }
    }
}