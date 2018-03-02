using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CityView.Construction;

namespace CityView {

    public class BuildingsHandler : MonoBehaviour {

        [SerializeField] private List<Building> buildings;
        public List<Building> Buildings { get { return buildings; } }

        private void Start() {
            BuildMode.OnBuildingPlaced += AddBuilding;
            Building.OnDestroyed += RemoveBuilding;
        }

        private void AddBuilding(Building building, BuildingsData data) {
            buildings.Add(building);
        }

        private void RemoveBuilding(Building building) {
            buildings.Remove(building);
         
        }
    }
}