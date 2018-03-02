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

        [SerializeField] private ProductionCycle productionCycle;
        public ProductionCycle ProductionCycle { get { return productionCycle; } }

        private Animator animator;
        private ParticleSystem[] particles;

        private void Awake() {
            Setup();
        }

        public void Setup() {
            animator = GetComponent<Animator>();
            particles = GetComponentsInChildren<ParticleSystem>();
        }

        public void Init(BuildingsData data) {
            this.data = data;
            if (HasNecessaryResourcesForProductionCycle())
                StartNewProduction();
            else 
                OnProductionNotAvailable();
        }

        private void OnDisable() {
            ToggleBuildingEffects(false);
        }

        private void OnEnable() {
            if(ProductionCycle != null)
                ToggleBuildingEffects(true);
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

        private void StartNewProduction() {
            productionCycle = new ProductionCycle(data, OnProductionCycleCompletedHandler);
            PlayerResources.OnResourceChanged -= OnResourcesChanged;
            PlayerResources.OnMoneyChanged -= OnMoneyChanged;
            if (!enabled) {
                enabled = true;
                ToggleBuildingEffects(true);
            }
        }

        public void ToggleBuildingEffects(bool toggle) {
            if(animator != null)
                animator.enabled = toggle;
            foreach (ParticleSystem p in particles) {
                if (toggle)
                    p.Play();
                else
                    p.Stop();
            }
        }

        public void Update() {
            ProductionCycle.UpdateProduction();
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