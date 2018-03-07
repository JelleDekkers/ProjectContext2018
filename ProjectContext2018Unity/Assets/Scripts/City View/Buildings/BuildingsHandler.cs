using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CityView.Construction;

namespace CityView {

    public class BuildingsHandler : MonoBehaviour {

        [SerializeField] private List<BuildingBase> buildings;
        public List<BuildingBase> Buildings { get { return buildings; } }

        private void Start() {
            BuildMode.OnBuildingPlaced += AddBuilding;
            BuildModeClimateBuildings.OnBuildingPlaced += AddBuilding;
            Building.OnDestroyed += RemoveBuilding;
        }

        private void AddBuilding(BuildingBase building, BuildingsData data) {
            buildings.Add(building);
        }

        private void AddBuilding(BuildingBase building, ClimateBuildingsData data) {
            buildings.Add(building);
        }

        private void RemoveBuilding(BuildingBase building) {
            buildings.Remove(building);
        }
    }
}