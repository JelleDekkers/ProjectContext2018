using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using CityView.Construction;

namespace CityView {

    public class Building : BuildingBase {

        public BuildingsData data;
        public static Action<Building> OnBuildingEnabled, OnBuildingDisabled;
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
        private bool sentEnergyCapacity;

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
        }

        protected virtual void OnDisable() {
            ToggleBuildingEffects(false);
            if (sentEnergyCapacity) {
                if (OnBuildingDisabled != null)
                    OnBuildingDisabled(this);
                sentEnergyCapacity = false;
            }
        }

        private void CheckWaterState(bool hasWater) {
            if (hasWater) {
                if (isUnderWater == false) {
                    if (OnWaterReachedBuilding != null)
                        OnWaterReachedBuilding(this);
                    EventLogManager.AddNewUnderwaterLog(this);
                }
                isUnderWater = true;
                enabled = false;
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
            int energyIndex = 0;
            if (data.Resourceinput.Contains(energyIndex) && !sentEnergyCapacity) {
                return (PlayerResources.Instance.HasResourcesAmountExcludingEnergy(data.Resourceinput, data.Resourceinputamount) &&
                        PlayerResources.Instance.HasMoneyAmount(data.Moneyinput) &&
                        PlayerResources.Instance.HasEnergyAmount(data.Resourceinputamount[energyIndex]));
            } else {
                return (PlayerResources.Instance.HasResourcesAmountExcludingEnergy(data.Resourceinput, data.Resourceinputamount) &&
                        PlayerResources.Instance.HasMoneyAmount(data.Moneyinput));
            }
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

        private BuildingsData GetDataWithoutEnergyResource(BuildingsData data) {
            BuildingsData dataWithoutEnergy = data.Clone() as BuildingsData;
            List<int> input = new List<int>();
            List<int> inputAmount = new List<int>();
            for (int i = 0; i < data.Resourceinput.Length; i++) {
                if (data.Resourceinput[i] != 0) {
                    input.Add(data.Resourceinput[i]);
                    inputAmount.Add(data.Resourceinputamount[i]);
                }
            }
            dataWithoutEnergy.Resourceinput = input.ToArray();
            dataWithoutEnergy.Resourceinputamount = inputAmount.ToArray();

            List<int> output = new List<int>();
            List<int> outputAmount = new List<int>();
            for (int i = 0; i < data.Resourceoutput.Length; i++) {
                if (data.Resourceoutput[i] != 0) {
                    output.Add(data.Resourceoutput[i]);
                    outputAmount.Add(data.Resourceoutputamount[i]);
                }
            }
            dataWithoutEnergy.Resourceoutput = output.ToArray();
            dataWithoutEnergy.Resourceoutputamount = outputAmount.ToArray();
            return dataWithoutEnergy;
        }

        private void StartNewProduction() {
            BuildingsData dataWithoutEnergy = GetDataWithoutEnergyResource(data);
            productionCycle = new ProductionCycle(dataWithoutEnergy, OnProductionCycleCompletedHandler);
            PlayerResources.OnResourceChanged -= CheckIfNecessaryInputResourcesAreAvailable;
            //PlayerResources.OnMoneyChanged -= (x) => CheckIfNecessaryInputResourcesAreAvailable();
            OnProductionInputProcessed(this, dataWithoutEnergy);

            if (!enabled)
                enabled = true;
            ToggleBuildingEffects(true);

            if (!sentEnergyCapacity) {
                if (OnBuildingEnabled != null)
                    OnBuildingEnabled(this);
                sentEnergyCapacity = true;
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
            BuildingsData data = DataManager.BuildingData.dataArray[dataID];
            if (!PlayerResources.Instance.HasMoneyAmount(data.Moneycost))
                return false;
            if (!PlayerResources.Instance.HasResourcesAmount(data.Resourcecost, data.Resourcecostamount))
                return false;
            //int energyIndex = 0;
            //if (data.Resourceinput.Contains(energyIndex)) {
            //    if (!PlayerResources.Instance.HasEnergyAmount(data.Resourceinputamount[energyIndex]))
            //        return false;
            //}
            return true;
        }

        public static bool IsBuildingBuildable(int dataID) {
            BuildingsData data = DataManager.BuildingData.dataArray[dataID];
            if (!PlayerResources.Instance.HasMoneyAmount(data.Moneycost))
                return false;
            if (!PlayerResources.Instance.HasResourcesAmount(data.Resourcecost, data.Resourcecostamount))
                return false;
            //int energyIndex = 0;
            //if (data.Resourceinput.Contains(energyIndex)) {
            //    if (!PlayerResources.Instance.HasEnergyAmount(data.Resourceinputamount[energyIndex]))
            //        return false;
            //}
            return true;
        }
    }
}