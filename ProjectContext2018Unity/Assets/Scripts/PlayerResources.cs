using System;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerResources {

    public static Dictionary<string, float> Resources { get; private set; }
    public static float Money { get; private set; }

    public static Action<int, float> OnResourceChanged;
    public static Action<float> OnMoneyChanged;

    private static float cheatAmount = 200;

    public static void Init() {
        Resources = new Dictionary<string, float>();
        foreach (GameResourcesData d in DataManager.ResourcesData.dataArray) {
            Resources.Add(d.Name, cheatAmount); // debug
        }
        Money = cheatAmount;
    }

    #region add resource
    public static void AddResources(Dictionary<int, float> resources) {
        foreach(KeyValuePair<int, float> pair in resources) {
            AddResource(pair.Key, pair.Value);
        }
    }

    public static void AddResource(int resourceID, float amount) {
        string resourceName = DataManager.ResourcesData.dataArray[resourceID].Name;
        Resources[resourceName] += amount;
        if(OnResourceChanged != null)
            OnResourceChanged(resourceID, Resources[resourceName]);
    }

    public static void AddResources(ResourceContainer[] resources) {
        foreach (ResourceContainer product in resources) {
            AddResource(product);
        }
    }

    public static void AddResource(ResourceContainer product) {
        string resourceName = DataManager.ResourcesData.dataArray[product.id].Name;
        Resources[resourceName] += product.amount;
        if(OnResourceChanged != null)
            OnResourceChanged(product.id, Resources[resourceName]);
    }

    public static void AddMoney(float amount) {
        Money += amount;
        if(OnMoneyChanged != null)
            OnMoneyChanged(Money);
    }
    #endregion

    #region has resource amount
    public static bool HasResourcesAmount(string[] resources, float amount) {
        foreach(string resource in resources) {
            if (!HasResourceAmount(resource, amount))
                return false;
        }
        return true;
    }

    public static bool HasResourceAmount(string resourceName, float amount) {
        return Resources[resourceName] >= amount;
    }

    public static bool HasResourcesAmount(ResourceContainer[] products) {
        foreach (ResourceContainer product in products) {
            if (!HasResourceAmount(product))
                return false;
        }
        return true;
    }

    public static bool HasResourceAmount(ResourceContainer product) {
        string resourceName = DataManager.ResourcesData.dataArray[product.id].Name;
        return Resources[resourceName] >= product.amount;
    }

    public static bool HasMoneyAmount(float amount) {
        return Money >= amount;
    }
    #endregion

    #region remove resource
    public static void RemoveResources(Dictionary<int, float> resources) {
        foreach (KeyValuePair<int, float> pair in resources) {
            RemoveResource(pair.Key, pair.Value);
        }
    }

    public static void RemoveResource(int resourceID, float amount) {
        string resourceName = DataManager.ResourcesData.dataArray[resourceID].Name;
        Resources[resourceName] -= amount;
        if (Resources[resourceName] < 0)
            Resources[resourceName] = 0;
        OnResourceChanged(resourceID, Resources[resourceName]);
    }

    public static void RemoveResources(ResourceContainer[] products) {
        foreach (ResourceContainer product in products) {
            RemoveResource(product);
        }
    }

    public static void RemoveResource(ResourceContainer product) {
        string resourceName = DataManager.ResourcesData.dataArray[product.id].Name;
        Resources[resourceName] -= product.amount;
        if (Resources[resourceName] < 0)
            Resources[resourceName] = 0;
        OnResourceChanged(product.id, Resources[resourceName]);
    }

    public static void RemoveMoney(float amount) {
        Money -= amount;
        if (Money < 0)
            Money = 0;
        OnMoneyChanged(Money);
    }
    #endregion
}
