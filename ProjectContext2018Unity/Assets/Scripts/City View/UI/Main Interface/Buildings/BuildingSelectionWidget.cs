using System;
using UnityEngine;

namespace CityView.UI {

    public class BuildingSelectionWidget : MonoBehaviour {

        public static Action<int> OnBuildingSelected;

        [SerializeField] private BuildingSelectionWidgetItem itemPrefab;
        [SerializeField] private Transform contentParent;
        [SerializeField] private BuildingPrefabs buildings, climateBuildings;

        private bool cached;

        private void OnEnable() {
            SwitchView.OnSceneViewSwitched += Disable;
        }

        private void OnDisable() {
            SwitchView.OnSceneViewSwitched -= Disable;
        }

        private void Disable() {
            gameObject.SetActive(false);
        }

        public void FillGridBuildingData() {
            contentParent.RemoveChildren();
            gameObject.SetActive(true);
            for (int i = 0; i < DataManager.BuildingData.dataArray.Length; i++) {
                BuildingsData data = DataManager.BuildingData.dataArray[i];
                if(data.Climate == Climate.None || data.Climate == City.Instance.ClimateType)
                    Instantiate(itemPrefab, contentParent).Init(i, buildings, data);
            }
        }

        public void FillGridClimateBuildingData() {
            contentParent.RemoveChildren();
            gameObject.SetActive(true);
            for (int i = 0; i < DataManager.ClimateBuildingData.dataArray.Length; i++) {
                ClimateBuildingsData data = DataManager.ClimateBuildingData.dataArray[i];
                if(data.Climate == Climate.None || data.Climate == City.Instance.ClimateType)
                    Instantiate(itemPrefab, contentParent).Init(i, climateBuildings, data);
            }
        }
    }
}