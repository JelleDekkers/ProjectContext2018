using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CityView.Construction;

namespace CityView {
    public class ClimateBuilding : BuildingBase {

        public ClimateBuildingsData data;

        public override void Init(object data, Tile[,] tilesStandingOn) {
            this.data = data as ClimateBuildingsData;
            this.tilesStandingOn = tilesStandingOn;
        }

        public override void Setup() { }

        public override void ToggleBuildingEffects(bool toggle) { }
    }
}