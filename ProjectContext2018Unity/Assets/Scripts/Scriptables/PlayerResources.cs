using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Resources", menuName = "Scriptables/Player Resources", order = 0)]
public class PlayerResources : ScriptableObjectSingleton<PlayerResources> {

    [SerializeField] private float startingMoney;
    public static float StartingMoney { get { return Instance.startingMoney; } }

    [SerializeField] private float money;
    public static float Money { get { return Instance.money; } }

    [SerializeField] private ResourceAmountDictionary startingResources;
    public static ResourceAmountDictionary StartingResources { get { return Instance.startingResources; } }

    [SerializeField] private ResourceAmountDictionary resources;
    public static ResourceAmountDictionary Resources { get { return Instance.resources; } }

    public static Action<int, int> OnResourceChanged;
    public static Action<float> OnMoneyChanged;

    public void Init() {
        money = startingMoney;

        resources = new ResourceAmountDictionary();
        foreach(KeyValuePair<string, int> pair in startingResources) {
            resources.Add(pair.Key, pair.Value);
        }
    }

    /// <summary>
    /// Updates the dictionary to correspond with the GameResources database
    /// </summary>
    /// <param name="data"></param>
    public void SetResources() {
        startingResources = new ResourceAmountDictionary();
        for (int i = 0; i < DataManager.ResourcesData.dataArray.Length; i++)
            startingResources.Add(DataManager.ResourcesData.dataArray[i].Name, 0);
    }

    public void ProcessBuildingProductionResult(CityView.Building building, ProductionCycleResult result) {
        if (result.money != 0)
            AddMoney(result.money);
        if (result.producedResources.Length != 0)
            AddResources(result.producedResources);
    }

    public void TradeOfferBought(TradeOffer tradeOffer) {
        RemoveMoney(tradeOffer.totalValue);
        AddResource(tradeOffer.productId, tradeOffer.amount);
    }

    public void TradeOfferSold(TradeOffer tradeOffer) {
        AddMoney(tradeOffer.totalValue);
    }

    #region add resource
    public void AddResources(Dictionary<int, int> resources) {
        foreach (KeyValuePair<int, int> pair in resources) {
            AddResource(pair.Key, pair.Value);
        }
    }

    public void AddResource(int resourceID, int amount) {
        string resourceName = DataManager.ResourcesData.dataArray[resourceID].Name;
        resources[resourceName] += amount;
        if (OnResourceChanged != null)
            OnResourceChanged(resourceID, resources[resourceName]);
    }

    public void AddResources(ResourceContainer[] resources) {
        foreach (ResourceContainer product in resources) {
            AddResource(product);
        }
    }

    public void AddResource(ResourceContainer product) {
        string resourceName = DataManager.ResourcesData.dataArray[product.id].Name;
        resources[resourceName] += product.amount;
        if (OnResourceChanged != null)
            OnResourceChanged(product.id, resources[resourceName]);
    }

    public void AddMoney(float amount) {
        money += amount;
        if (OnMoneyChanged != null)
            OnMoneyChanged(Money);
    }
    #endregion

    #region has resource amount
    public bool HasResourcesAmount(int[] resources, int[] amount) {
        for (int i = 0; i < resources.Length; i++) {
            if (!HasResourceAmount(resources[i], amount[i]))
                return false;
        }
        return true;
    }

    public bool HasResourceAmount(int resourceID, int amount) {
        return HasResourceAmount(DataManager.ResourcesData.dataArray[resourceID].Name, amount);
    }

    public bool HasResourceAmount(string resourceName, int amount) {
        return resources[resourceName] >= amount;
    }

    public bool HasResourcesAmount(ResourceContainer[] products) {
        foreach (ResourceContainer product in products) {
            if (!HasResourceAmount(product))
                return false;
        }
        return true;
    }

    public bool HasResourceAmount(ResourceContainer product) {
        string resourceName = DataManager.ResourcesData.dataArray[product.id].Name;
        return resources[resourceName] >= product.amount;
    }

    public bool HasMoneyAmount(float amount) {
        return Money >= amount;
    }
    #endregion

    #region remove resource
    public void RemoveResources(int[] IDs, int[] amount) {
        for(int i = 0; i < IDs.Length; i++) { 
            RemoveResource(IDs[i], amount[i]);
        }
    }

    public void RemoveResource(int resourceID, int amount) {
        string resourceName = DataManager.ResourcesData.dataArray[resourceID].Name;
        resources[resourceName] -= amount;
        if (resources[resourceName] < 0)
            resources[resourceName] = 0;
        OnResourceChanged(resourceID, resources[resourceName]);
    }

    public void RemoveResources(ResourceContainer[] products) {
        foreach (ResourceContainer product in products) {
            RemoveResource(product);
        }
    }

    public void RemoveResource(ResourceContainer product) {
        string resourceName = DataManager.ResourcesData.dataArray[product.id].Name;
        resources[resourceName] -= product.amount;
        if (resources[resourceName] < 0)
            resources[resourceName] = 0;
        OnResourceChanged(product.id, resources[resourceName]);
    }

    public void RemoveMoney(float amount) {
        money -= amount;
        if (Money < 0)
            money = 0;
        OnMoneyChanged(Money);
    }
    #endregion
}
