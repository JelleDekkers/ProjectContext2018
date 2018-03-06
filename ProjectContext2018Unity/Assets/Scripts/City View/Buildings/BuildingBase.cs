using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CityView.Construction;

namespace CityView {
    public class BuildingBase : MonoBehaviour {

        protected IntVector2? size;
        public IntVector2 Size {
            get {
                if (!size.HasValue)
                    size = CalculateTileSize();
                return size.Value;
            }
        }

        public Tile[,] tilesStandingOn;

        public IntVector2 CalculateTileSize() {
            IntVector2 calcSize = IntVector2.Zero;
            Renderer r = transform.GetChild(0).GetComponent<Renderer>();
            calcSize.x = (int)Mathf.Round(r.bounds.size.x);
            calcSize.z = (int)Mathf.Round(r.bounds.size.z);
            return calcSize;
        }

        public static bool IsBuildable(int id) {
            BuildingsData data = DataManager.BuildingData.dataArray[id];
            if (!PlayerResources.Instance.HasMoneyAmount(data.Moneycost))
                return false;
            if (!PlayerResources.Instance.HasResourcesAmount(data.Resourcecost, data.Resourcecostamount))
                return false;
            return true;
        }
    }
}