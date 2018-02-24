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

        public CityType Type {
            get {
                return type;
            }

            private set {
                type = value;
            }
        }

        public CityGrid grid;
        public List<Building> buildings;
        private CityType type;

        private void Awake() {
            instance = this;
            PlayerResources.Init();
            BuildMode.OnBuildingPlaced += AddBuilding;
            Building.OnProductionCycleCompleted += ProcessProductionResult;
            Building.OnDestroyed += RemoveBuilding;

            // Climate type is still randomly assigned, it still needs to check whether certain "Climates" have already been claimed by other players.
            Type = new CityType((CityType.Climate)UnityEngine.Random.Range(0, (Enum.GetNames(typeof(CityType.Climate)).Length)));
            //Type.DebugCall();
        }

        private void AddBuilding(Building building, BuildingsData data) {
            buildings.Add(building);
            building.Init(data, this);
            PlayerResources.RemoveMoney(data.Costmoney);
            for(int i = 0; i < data.Resourcecost.Length; i++)
                PlayerResources.RemoveResource(data.Resourcecost[i], data.Resourcecostamount[i]); 
        }

        private void RemoveBuilding(Building building) {
            buildings.Remove(building);
        }

        private void ProcessProductionResult(Building building, ProductionCycleResult result) {
            if (result.money != 0)
                PlayerResources.AddMoney(result.money);
            if (result.producedResources.Length != 0)
                PlayerResources.AddResources(result.producedResources);
            if (result.pollutionPoints != 0)
                AddPollution(result.pollutionPoints);
        }

        private void AddPollution(float amount) {

        }
    }
}