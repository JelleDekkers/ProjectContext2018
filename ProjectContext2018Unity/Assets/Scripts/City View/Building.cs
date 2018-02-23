using System;
using System.Collections.Generic;
using UnityEngine;

namespace CityView {

    public class Building : MonoBehaviour {

        private Vector2Int? size;
        public Vector2Int Size { get {
                if (!size.HasValue)
                    size = CalculateTileSize();
                return size.Value;
            }
        }

        public BuildingsData data;
        public static Action<Building, ProductionCycleResult> OnProductionCycleCompleted;

        public ProductionCycle productionCycle;

        public void Init(BuildingsData data, City city) {
            this.data = data;
            productionCycle = new ProductionCycle(data, OnProductionCycleCompletedHandler);
        }

        public void Update() {
            productionCycle.UpdateProduction();
        }

        private void OnProductionCycleCompletedHandler(ProductionCycleResult result) {
            OnProductionCycleCompleted(this, result);
        }

        public Vector2Int CalculateTileSize() {
            Vector2Int calcSize = Vector2Int.zero;
            Renderer r = transform.GetChild(0).GetComponent<Renderer>();
            calcSize.x = (int)Mathf.Round(r.bounds.size.x);
            calcSize.y = (int)Mathf.Round(r.bounds.size.z);
            return calcSize;
        }
    }
}