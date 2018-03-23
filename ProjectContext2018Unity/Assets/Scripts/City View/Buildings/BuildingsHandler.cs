using System;
using System.Collections.Generic;
using UnityEngine;
using CityView.Construction;

namespace CityView {

    public class BuildingsHandler : MonoBehaviour {

        private static BuildingsHandler instance;
        public static BuildingsHandler Instance { get { return instance; } }

        public static Action OnBuildingListChanged;

        [SerializeField] private List<BuildingBase> buildings;
        public List<BuildingBase> Buildings { get { return buildings; } }

        private void Awake() {
            instance = this;
        }

        private void Start() {
            BuildMode.OnBuildingPlaced += AddBuilding;
            BuildModeClimateBuildings.OnBuildingPlaced += AddBuilding;
            Building.OnDestroyedGlobal += RemoveBuilding;
        }

        private void AddBuilding(BuildingBase building, BuildingsData data) {
            buildings.Add(building);
            if(OnBuildingListChanged != null)
                OnBuildingListChanged();
        }

        private void AddBuilding(BuildingBase building, ClimateBuildingsData data) {
            buildings.Add(building);
            if(OnBuildingListChanged != null)
                OnBuildingListChanged();
        }

        private void RemoveBuilding(BuildingBase building) {
            buildings.Remove(building);
            if(OnBuildingListChanged != null)
                OnBuildingListChanged();
        }

        public float GetPollutionPerMinute() {
            float amount = 0;
            foreach (BuildingBase building in buildings) {
                if (building.GetType() == typeof(Building)) {
                    Building b = building as Building;
                    amount += b.data.Pollution / b.data.Productiontime;
                }
            }
            return amount * 60;
        }
    }
}