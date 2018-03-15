using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CityView.Construction;
using CityView.Terrain;

namespace CityView {
    public class ClimateBuilding : BuildingBase {

        public ClimateBuildingsData data;

        protected TerrainBlock[] blocksStandingOn;

        public override void Init(object data, Tile[,] tilesStandingOn) {
            this.data = data as ClimateBuildingsData;
            this.tilesStandingOn = tilesStandingOn;

            blocksStandingOn = new TerrainBlock[tilesStandingOn.Length];
            int index = 0;
            foreach (Tile tile in tilesStandingOn) {
                blocksStandingOn[index] = City.Instance.Terrain.GetTerrainBlock(tile.Coordinates);
                index++;
            }
        }

        public override void CacheEffects() { }

        public override void ToggleBuildingEffects(bool toggle) { }

        public override bool IsBuildable(int dataID) {
            if (!PlayerResources.Instance.HasMoneyAmount(DataManager.ClimateBuildingData.dataArray[dataID].Moneycost))
                return false;
            if (!PlayerResources.Instance.HasResourcesAmount(DataManager.ClimateBuildingData.dataArray[dataID].Resourcecost, DataManager.ClimateBuildingData.dataArray[dataID].Resourcecostamount))
                return false;
            return true;
        }

        public static bool IsBuildingBuildable(int dataID) {
            if (!PlayerResources.Instance.HasMoneyAmount(DataManager.ClimateBuildingData.dataArray[dataID].Moneycost))
                return false;
            if (!PlayerResources.Instance.HasResourcesAmount(DataManager.ClimateBuildingData.dataArray[dataID].Resourcecost, DataManager.ClimateBuildingData.dataArray[dataID].Resourcecostamount))
                return false;
            return true;
        }
    }
}