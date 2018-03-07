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

        public override void Setup() { }

        public override void ToggleBuildingEffects(bool toggle) { }
    }
}