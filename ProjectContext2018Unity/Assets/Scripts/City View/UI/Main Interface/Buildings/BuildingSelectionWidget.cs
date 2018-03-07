using System;
using UnityEngine;

namespace CityView.UI {

    public class BuildingSelectionWidget : MonoBehaviour {

        public static Action<int> OnBuildingSelected;

        [SerializeField] private BuildingSelectionWidgetItem itemPrefab;
        [SerializeField] private Transform contentParent;
        [SerializeField] private BuildingPrefabs buildings, climateBuildings;

        private bool cached;

        public void FillGridBuildingData() {
            contentParent.RemoveChildren();
            gameObject.SetActive(true);
            for (int i = 0; i < DataManager.BuildingData.dataArray.Length; i++)
                Instantiate(itemPrefab, contentParent).Init(i, buildings, DataManager.BuildingData.dataArray[i]);
        }

        public void FillGridClimateBuildingData() {
            contentParent.RemoveChildren();
            gameObject.SetActive(true);
            for (int i = 0; i < DataManager.ClimateBuildingData.dataArray.Length; i++)
                Instantiate(itemPrefab, contentParent).Init(i, climateBuildings, DataManager.ClimateBuildingData.dataArray[i]);
        }
    }
}