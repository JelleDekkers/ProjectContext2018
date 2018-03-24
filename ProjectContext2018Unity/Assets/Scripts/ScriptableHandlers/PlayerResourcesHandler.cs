using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CityView;

public class PlayerResourcesHandler : MonoBehaviour {

    private static PlayerResourcesHandler instance;
    public static PlayerResourcesHandler Instance { get { return instance; } }

    [SerializeField] private PlayerResources playerResources;
    public PlayerResources Resources { get { return playerResources; } }

    private int energyIndex = 0;

    private void Awake() {
        instance = this;
        playerResources.Init();
        Building.OnProductionCycleCompleted += playerResources.ProcessBuildingProductionResult;
        MarketPlace.OnTradeOfferBought += playerResources.TradeOfferBought;
        MarketPlace.OnTradeOfferSold += playerResources.TradeOfferSold;
        Building.OnBuildingEnabled += HandleBuildingEnabled;
        Building.OnBuildingDisabled += HandeBuildingDisabled;
    }

    private void OnDestroy() {
        Building.OnProductionCycleCompleted -= playerResources.ProcessBuildingProductionResult;
        MarketPlace.OnTradeOfferBought -= playerResources.TradeOfferBought;
        MarketPlace.OnTradeOfferSold -= playerResources.TradeOfferSold;
        Building.OnBuildingEnabled -= HandleBuildingEnabled;
        Building.OnBuildingDisabled -= HandeBuildingDisabled;
   }

    private void HandleBuildingEnabled(Building building) {
        // Store energy as capacity
        foreach (int i in building.data.Resourceoutput) {
            if (i == energyIndex) {
                Resources.AddResource(i, building.data.Resourceoutputamount[i]);
                return;
            }
        }
    }

    private void HandeBuildingDisabled(Building building) {
        // Remove stored energy
        foreach (int i in building.data.Resourceoutput) {
            if (i == energyIndex) {
                Resources.RemoveResource(i, building.data.Resourceoutputamount[i]);
                return;
            }
        }
    }

    public void UpdateResource(int id, int amount) {
        if (amount > 0)
            Resources.AddResource(id, amount);
        else
            Resources.RemoveResource(id, amount * -1);
    }
}
