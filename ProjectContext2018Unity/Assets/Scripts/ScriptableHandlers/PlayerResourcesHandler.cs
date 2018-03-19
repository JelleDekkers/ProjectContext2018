using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResourcesHandler : MonoBehaviour {

    private static PlayerResourcesHandler instance;
    public static PlayerResourcesHandler Instance { get { return instance; } }

    [SerializeField] private PlayerResources playerResources;
    public PlayerResources Resources { get { return playerResources; } }

    private void Awake() {
        instance = this;
        playerResources.Init();
        CityView.Building.OnProductionCycleCompleted += playerResources.ProcessBuildingProductionResult;
        MarketPlace.OnTradeOfferBought += playerResources.TradeOfferBought;
        MarketPlace.OnTradeOfferSold += playerResources.TradeOfferSold;
    }

    private void OnDestroy() {
        CityView.Building.OnProductionCycleCompleted -= playerResources.ProcessBuildingProductionResult;
        MarketPlace.OnTradeOfferBought -= playerResources.TradeOfferBought;
        MarketPlace.OnTradeOfferSold -= playerResources.TradeOfferSold;
    }

    public void UpdateResource(int id, int amount) {
        if (amount > 0)
            Resources.AddResource(id, amount);
        else
            Resources.RemoveResource(id, amount);
    }
}
