using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResourcesHandler : MonoBehaviour {

    [SerializeField] private PlayerResources resources;
    public PlayerResources Resources { get { return resources; } }

    private void Awake() {
        resources.Init();
        CityView.Building.OnProductionCycleCompleted += resources.ProcessBuildingProductionResult;
        MarketPlace.OnTradeOfferBought += resources.ProcessTradeOffer;
    }

    private void OnDestroy() {
        CityView.Building.OnProductionCycleCompleted -= resources.ProcessBuildingProductionResult;
        MarketPlace.OnTradeOfferBought -= resources.ProcessTradeOffer;
    }
}
