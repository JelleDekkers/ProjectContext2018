using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CityView.UI {

    public class ClimateBuildingSelectionWidget : MonoBehaviour {

        [SerializeField] private BuildingSelectionWidgetItem itemPrefab;
        [SerializeField] private GameObject contentWidget;
        [SerializeField] private Transform contentGrid;
        [SerializeField] private BuildingPrefabs climateBuildings;
        [SerializeField] private ClimateBuildingListSelectionInfoWidget infoWidget;
        [SerializeField] private Construction.BuildModeClimateBuildings buildMode;
        [SerializeField] private Toggle toggle;

        private void OnEnable() {
            SwitchView.OnSceneViewSwitched += Disable;
            infoWidget.Init();
        }

        private void OnDisable() {
            SwitchView.OnSceneViewSwitched -= Disable;
        }

        private void Disable() {
            gameObject.SetActive(false);
        }

        public void OnToggled() {
            FillGridBuildingData();
            contentWidget.gameObject.SetActive(toggle);
            buildMode.enabled = toggle.isOn;
        }

        private void FillGridBuildingData() {
            contentGrid.RemoveChildren();
            contentWidget.gameObject.SetActive(true);
            for (int i = 0; i < DataManager.ClimateBuildingData.dataArray.Length; i++) {
                ClimateBuildingsData data = DataManager.ClimateBuildingData.dataArray[i];
                bool correctClimate = (data.Climate == Climate.None || data.Climate == City.Instance.ClimateType);
                Instantiate(itemPrefab, contentGrid).Init(i, climateBuildings, data, correctClimate, buildMode);
            }
        }
    }
}