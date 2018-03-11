using System;
using CityView.Terrain;

namespace CityView {
    public class Dike : ClimateBuilding {

        public Action OnDestroyEvent;

        public override void OnDemolishStart() {
            base.OnDemolishStart();
            foreach (TerrainBlock block in blocksStandingOn)
                block.SetExtraHeight(0);
        }
    }
}