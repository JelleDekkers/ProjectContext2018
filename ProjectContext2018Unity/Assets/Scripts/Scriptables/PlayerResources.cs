using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Resources", menuName = "Scriptables/Player Resources", order = 0)]
public class PlayerResources : ScriptableObjectSingleton<PlayerResources> {

    [SerializeField] private float money;
    public static float Money { get { return Instance.money; } }

    [SerializeField] private ResourceAmountDictionary resources;
    public static ResourceAmountDictionary Resources { get { return Instance.resources; } }

    public static Action<int, float> OnResourceChanged;
    public static Action<float> OnMoneyChanged;

    //private float cheatAmount = 200;

    public void Init() {
        //Resources = new Dictionary<string, float>();
        //foreach (GameResourcesData d in DataManager.ResourcesData.dataArray) 
        //    Resources.Add(d.Name, cheatAmount); // debug
        //Money = cheatAmount;
    }

    /// <summary>
    /// Updates the dictionary to correspond with the GameResources database
    /// </summary>
    /// <param name="data"></param>
    public void SetResources() {
        resources = new ResourceAmountDictionary();
        for (int i = 0; i < DataManager.ResourcesData.dataArray.Length; i++)
            resources.Add(DataManager.ResourcesData.dataArray[i].Name, 0);
    }

    public void ProcessBuildingProductionResult(CityView.Building building, ProductionCycleResult result) {
        if (result.money != 0)
            AddMoney(result.money);
        if (result.producedResources.Length != 0)
            AddResources(result.producedResources);
    }

    #region add resource
    public void AddResources(Dictionary<int, float> resources) {
        foreach (KeyValuePair<int, float> pair in resources) {
            AddResource(pair.Key, pair.Value);
        }
    }

    public void AddResource(int resourceID, float amount) {
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
    public bool HasResourcesAmount(int[] resources, float[] amount) {
        for (int i = 0; i < resources.Length; i++) {
            if (!HasResourceAmount(resources[i], amount[i]))
                return false;
        }
        return true;
    }

    public bool HasResourceAmount(int resourceID, float amount) {
        return HasResourceAmount(DataManager.ResourcesData.dataArray[resourceID].Name, amount);
    }

    public bool HasResourceAmount(string resourceName, float amount) {
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
    public void RemoveResources(Dictionary<int, float> resources) {
        foreach (KeyValuePair<int, float> pair in resources) {
            RemoveResource(pair.Key, pair.Value);
        }
    }

    public void RemoveResource(int resourceID, float amount) {
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
