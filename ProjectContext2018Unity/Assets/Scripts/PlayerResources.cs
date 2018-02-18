using System;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerResources {

    public static Dictionary<string, float> Resources { get; private set; }

    public static void Init() {
        Resources = new Dictionary<string, float>();
        foreach (GameResourcesData d in DataManager.ResourcesData.dataArray) {
            Resources.Add(d.Name, 0);
        }
    }

    #region add resource
    public static void AddResources(Dictionary<string, float> resources) {
        foreach(KeyValuePair<string, float> pair in resources) {
            AddResource(pair.Key, pair.Value);
        }
    }

    public static void AddResource(string resourceName, float amount) {
        Resources[resourceName] += amount;
    }

    public static void AddResources(ResourceContainer[] resources) {
        foreach (ResourceContainer product in resources) {
            AddResource(product);
        }
    }

    public static void AddResource(ResourceContainer product) {
        string resourceName = DataManager.ResourcesData.dataArray[product.id].Name;
        Resources[resourceName] += product.amount;
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
    #endregion

    #region remove resource
    public static void RemoveResources(Dictionary<string, float> resources) {
        foreach (KeyValuePair<string, float> pair in resources) {
            RemoveResource(pair.Key, pair.Value);
        }
    }

    public static void RemoveResource(string resourceName, float amount) {
        Resources[resourceName] -= amount;
        if (Resources[resourceName] < 0)
            Resources[resourceName] = 0;
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
    }
    #endregion
}
