﻿using System;
using System.Collections.Generic;
using UnityEngine;
using CityView.Construction;

namespace CityView {

    public class Building : BuildingBase {

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

        public void Init(BuildingsData data, Tile[,] tilesStandingOn) {
            this.data = data;
            this.tilesStandingOn = tilesStandingOn;
            if (HasNecessaryResourcesForProductionCycle())
                StartNewProduction();
            else 
                OnProductionNotAvailable();

            foreach (Tile t in tilesStandingOn)
                t.OnWaterStateChanged += CheckWaterState;
        }

        private void OnDisable() {
            ToggleBuildingEffects(false);
        }

        private void OnEnable() {
            if(ProductionCycle != null)
                ToggleBuildingEffects(true);
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

            OnDestroyed(this);
        }
    }
}