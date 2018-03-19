using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResourcesHandler : MonoBehaviour {

    [SerializeField] private PlayerResources playerResources;
    public PlayerResources Resources { get { return playerResources; } }

    private void Awake() {
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
}
