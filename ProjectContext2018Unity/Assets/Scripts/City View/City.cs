using System;
using System.Collections.Generic;
using UnityEngine;
using CityView.Construction;

namespace CityView {

    public class City : MonoBehaviour {
        private static City instance;
        public static City Instance {
            get {
                if (instance == null)
                    instance = FindObjectOfType<City>();
                return instance; }
        }

        public CityGrid grid;
        public List<Building> buildings;

        private void Awake() {
            instance = this;
            PlayerResources.Init();
            PlaceMode.OnBuildingPlaced += AddBuilding;
            DestroyMode.OnBuildingRemoved += RemoveBuilding;
            Building.OnProductionCycleCompleted += ProcessProductionResult;
        }

        private void AddBuilding(Building building, BuildingsData data) {
            buildings.Add(building);
            building.Init(data, this);
        }

        private void RemoveBuilding(Building building) {
            buildings.Remove(building);
        }

        // placeholder?
        private void ProcessProductionResult(Building building, ProductionCycleResult result) {
            if (result.money != 0)
                AddMoney(result.money);// of via event?
            if (result.researchPoints != 0)
                AddResearchPoints(result.researchPoints);
            if (result.producedResources.Length != 0)
                PlayerResources.AddResources(result.producedResources);
            if (result.pollutionPoints != 0)
                AddPollution(result.pollutionPoints);
        }

        private void AddMoney(float amount) {

        }

        private void AddResearchPoints(float amount) {

        }

        private void AddPollution(float amount) {

        }
    }
}