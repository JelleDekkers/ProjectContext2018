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

        [SerializeField] private GameTime gameTime;

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

        public float GetPollutionPerYear() {
            float amount = 0;
            foreach (BuildingBase building in buildings) {
                if (building.GetType() == typeof(Building)) {
                    Building b = building as Building;
                    float pollutionPerSecond = b.data.Pollution / b.data.Productiontime;
                    amount += pollutionPerSecond * gameTime.TimePerYear;
                }
            }
            return amount;
        }

        public static float ConvertToPollutionPerYear(float pollution, float productionTime) {
            float pollutionPerSecond = pollution / productionTime;
            return pollutionPerSecond * instance.gameTime.TimePerYear;
        }
    }
}