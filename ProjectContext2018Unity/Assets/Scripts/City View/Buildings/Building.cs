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
        public static Action<Building> OnNotEnoughInputResourcesAvailable;
        public static Action<Building, BuildingsData> OnProductionInputProcessed;
        public static Action<Building> OnWaterReachedBuilding;

        public Action OnWaterIsGone;
        public Action OnProductionResumed;
        public Action OnDestroyed;

        private float timeBetweenProduction = 2;
        private bool isUnderWater;

        private ProductionCycle productionCycle = null;
        public ProductionCycle ProductionCycle { get { return productionCycle; } }

        protected bool initialized;
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
            StartCoroutine(WaitForNewProductionStart());

            foreach (Tile t in tilesStandingOn)
                t.OnWaterStateChanged += CheckWaterState;

            initialized = true;
        }

        protected virtual void OnEnable() {
            if (ProductionCycle != null)
                ToggleBuildingEffects(true);
            if (OnProductionResumed != null)
                OnProductionResumed();

            //if (isUnderWater) {
            //    isUnderWater = false;
            //    if (OnWaterIsGone != null)
            //        OnWaterIsGone();
            //}
        }

        protected virtual void OnDisable() {
            ToggleBuildingEffects(false);
        }

        private void CheckWaterState(bool hasWater) {
            if (hasWater) {
                if (isUnderWater == false) {
                    enabled = false;
                    isUnderWater = true;
                    if (OnWaterReachedBuilding != null)
                        OnWaterReachedBuilding(this);
                    EventLogManager.AddNewUnderwaterLog(this);
                }
            } else {
                enabled = !TilesStandingOnAreUnderWater();
                if (enabled) {
                    isUnderWater = false;
                    if (OnWaterIsGone != null)
                        OnWaterIsGone();
                }
            }
        }
        
        private void OnProductionNotAvailable() {
            PlayerResources.OnResourceChanged += CheckIfNecessaryInputResourcesAreAvailable;
            //PlayerResources.OnMoneyChanged += (x) => CheckIfNecessaryInputResourcesAreAvailable();
            enabled = false;
            ToggleBuildingEffects(false);
            if (OnNotEnoughInputResourcesAvailable != null)
                OnNotEnoughInputResourcesAvailable(this);

            int missingID;
            PlayerResources.Instance.HasResourcesAmount(data.Resourceinput, data.Resourceinputamount, out missingID);
            EventLogManager.AddNewResourceUnavailableLog(this, missingID);
        }

        private void CheckIfNecessaryInputResourcesAreAvailable(int x, int y) {
            if (HasNecessaryResourcesForProductionCycle())
                StartCoroutine(WaitForNewProductionStart());
        }

        private bool HasNecessaryResourcesForProductionCycle() {
            return (PlayerResources.Instance.HasResourcesAmount(data.Resourceinput, data.Resourceinputamount) &&
                    PlayerResources.Instance.HasMoneyAmount(data.Moneyinput));
        }

        private IEnumerator WaitForNewProductionStart() {
            WaitForSeconds wait = new WaitForSeconds(timeBetweenProduction);
            yield return wait;
            if (productionCycle == null) {
                if (HasNecessaryResourcesForProductionCycle())
                    StartNewProduction();
                else
                    OnProductionNotAvailable();
            }
        }

        private void StartNewProduction() {
            productionCycle = new ProductionCycle(data, OnProductionCycleCompletedHandler);
            PlayerResources.OnResourceChanged -= CheckIfNecessaryInputResourcesAreAvailable; 
            //PlayerResources.OnMoneyChanged -= (x) => CheckIfNecessaryInputResourcesAreAvailable();
            OnProductionInputProcessed(this, data);

            if (!enabled) 
                enabled = true;
            ToggleBuildingEffects(true);
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
            if(productionCycle != null)
                ProductionCycle.UpdateProduction();
        }
        
        private void OnProductionCycleCompletedHandler(ProductionCycleResult result) {
            productionCycle = null;
            OnProductionCycleCompleted(this, result);
            StartCoroutine(WaitForNewProductionStart());
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
                OnDestroyedGlobal(this);
            }
            if (OnDestroyed != null)
                OnDestroyed();
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