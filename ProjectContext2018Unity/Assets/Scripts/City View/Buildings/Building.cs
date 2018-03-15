using System;
using System.Collections;
using UnityEngine;
using CityView.Construction;

namespace CityView {

    public class Building : BuildingBase {

        public BuildingsData data;
        public static Action<Building, ProductionCycleResult> OnProductionCycleCompleted;
        public static Action<BuildingBase> OnDestroyedGlobal;
        public static Action<BuildingBase> OnDemolishInitiated;
        public static Action<Building> OnProductionStopped;
        public static Action<Building, BuildingsData> OnProductionInputProcessed;

        public Action OnProductionResumed;
        public Action OnDestroyed;

        private float timeBetweenProduction = 2;

        [SerializeField] private ProductionCycle productionCycle;
        public ProductionCycle ProductionCycle { get { return productionCycle; } }

        private Animator animator;
        private ParticleSystem[] particles;
        private bool effectsCached;

        public override void CacheEffects() {
            animator = GetComponent<Animator>();
            particles = GetComponentsInChildren<ParticleSystem>();
        }

        public override void Init(System.Object data, Tile[,] tilesStandingOn) {
            this.data = data as BuildingsData;
            this.tilesStandingOn = tilesStandingOn;
            if (HasNecessaryResourcesForProductionCycle())
                StartNewProduction();
            else
                OnProductionNotAvailable();

            foreach (Tile t in tilesStandingOn)
                t.OnWaterStateChanged += CheckWaterState;
        }

        protected virtual void OnEnable() {
            if (ProductionCycle != null)
                ToggleBuildingEffects(true);
            if (OnProductionResumed != null)
                OnProductionResumed();
        }

        protected virtual void OnDisable() {
            ToggleBuildingEffects(false);
            if(OnProductionStopped != null)
                OnProductionStopped(this);
        }

        private void CheckWaterState(bool water) {
            if (water == true)
                enabled = false;
            else 
                enabled = !TilesStandingOnAreUnderWater();
        }

        private void OnProductionNotAvailable() {
            PlayerResources.OnResourceChanged += OnResourcesChanged;
            PlayerResources.OnMoneyChanged += OnMoneyChanged;
            enabled = false;
            ToggleBuildingEffects(false);
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

        private IEnumerator WaitForNewProductionStart() {
            WaitForSeconds wait = new WaitForSeconds(timeBetweenProduction);
            yield return wait;
            StartNewProduction();
        }

        private void StartNewProduction() {
            productionCycle = new ProductionCycle(data, OnProductionCycleCompletedHandler);
            PlayerResources.OnResourceChanged -= OnResourcesChanged;
            PlayerResources.OnMoneyChanged -= OnMoneyChanged;
            OnProductionInputProcessed(this, data);

            if (!enabled) {
                enabled = true;
                ToggleBuildingEffects(true);
            }
        }

        public override void ToggleBuildingEffects(bool toggle) {
            if (!effectsCached)
                CacheEffects();

            if (animator != null) 
                animator.enabled = toggle;

            if (particles != null) {
                foreach (ParticleSystem p in particles) {
                    if (toggle)
                        p.Play();
                    else
                        p.Stop();
                }
            }
        }

        public void Update() {
            ProductionCycle.UpdateProduction();
        }
        
        private void OnProductionCycleCompletedHandler(ProductionCycleResult result) {
            OnProductionCycleCompleted(this, result);
            if (HasNecessaryResourcesForProductionCycle())
                StartCoroutine(WaitForNewProductionStart());
            else
                OnProductionNotAvailable();
        }

        public bool TilesStandingOnAreUnderWater() {
            foreach (Tile tile in tilesStandingOn) { 
                if (tile.IsUnderWater)
                    return true;
            }
            return false;
        }

        private void OnDestroy() {
            if (tilesStandingOn != null) {
                foreach (Tile t in tilesStandingOn)
                    t.OnWaterStateChanged -= CheckWaterState;
            }

            OnDestroyedGlobal(this);
        }

        public override bool IsBuildable(int dataID) {
            if (!PlayerResources.Instance.HasMoneyAmount(DataManager.BuildingData.dataArray[dataID].Moneycost))
                return false;
            if (!PlayerResources.Instance.HasResourcesAmount(DataManager.BuildingData.dataArray[dataID].Resourcecost, DataManager.BuildingData.dataArray[dataID].Resourcecostamount))
                return false;
            return true;
        }

        public static bool IsBuildingBuildable(int dataID) {
            if (!PlayerResources.Instance.HasMoneyAmount(DataManager.BuildingData.dataArray[dataID].Moneycost))
                return false;
            if (!PlayerResources.Instance.HasResourcesAmount(DataManager.BuildingData.dataArray[dataID].Resourcecost, DataManager.BuildingData.dataArray[dataID].Resourcecostamount))
                return false;
            return true;
        }
    }
}