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
        public static Action<Building> OnDestroyed;
        public static Action<Building> OnDemolishInitiated;

        public ProductionCycle productionCycle;

        public void Init(BuildingsData data, City city) {
            this.data = data;
            if (HasNecessaryResourcesForProductionCycle())
                StartNewProduction();
            else 
                OnProductionNotAvailable();
        }

        private void OnProductionNotAvailable() {
            PlayerResources.OnResourceChanged += OnResourcesChanged;
            PlayerResources.OnMoneyChanged += OnMoneyChanged;
            enabled = false;
        }

        private void OnResourcesChanged(int id, int amount) {
            if (HasNecessaryResourcesForProductionCycle())
                StartNewProduction();
        }

        private void OnMoneyChanged(float amount) {
            if (HasNecessaryResourcesForProductionCycle())
                StartNewProduction();
        }

        private bool HasNecessaryResourcesForProductionCycle() {
            return (PlayerResources.Instance.HasResourcesAmount(data.Resourceinput, data.Resourceinputamount) &&
                    PlayerResources.Instance.HasMoneyAmount(data.Moneyinput));
        }

        private void StartNewProduction() {
            productionCycle = new ProductionCycle(data, OnProductionCycleCompletedHandler);
            PlayerResources.OnResourceChanged -= OnResourcesChanged;
            PlayerResources.OnMoneyChanged -= OnMoneyChanged;
            enabled = true;
        }

        public void Update() {
            productionCycle.UpdateProduction();
        }
        
        private void OnProductionCycleCompletedHandler(ProductionCycleResult result) {
            OnProductionCycleCompleted(this, result);
            if (HasNecessaryResourcesForProductionCycle())
                StartNewProduction();
            else
                OnProductionNotAvailable();
        }

        public Vector2Int CalculateTileSize() {
            Vector2Int calcSize = Vector2Int.zero;
            Renderer r = transform.GetChild(0).GetComponent<Renderer>();
            calcSize.x = (int)Mathf.Round(r.bounds.size.x);
            calcSize.y = (int)Mathf.Round(r.bounds.size.z);
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

        private void OnDestroy() {
            OnDestroyed(this);
        }
    }
}