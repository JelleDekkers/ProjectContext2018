using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ResourcesStockpile {

    public StockPileResourceDictionary resourcesDict;

    public void Init() {
        foreach (GameResourcesData d in DataManager.ResourcesData.dataArray) {
            resourcesDict.Add(d.Name, 0);
        }
    }

    #region add resource
    public void AddResources(Dictionary<string, float> resources) {
        foreach(KeyValuePair<string, float> pair in resources) {
            AddResource(pair.Key, pair.Value);
        }
    }

    public void AddResource(string resourceName, float amount) {
        resourcesDict[resourceName] += amount;
    }

    public void AddResources(ResourceContainer[] resources) {
        foreach (ResourceContainer product in resources) {
            AddResource(product);
        }
    }

    public void AddResource(ResourceContainer product) {
        string resourceName = DataManager.ResourcesData.dataArray[product.id].Name;
        resourcesDict[resourceName] += product.amount;
    }
    #endregion

    #region has resource amount
    public bool HasResourcesAmount(string[] resources, float amount) {
        foreach(string resource in resources) {
            if (!HasResourceAmount(resource, amount))
                return false;
        }
        return true;
    }

    public bool HasResourceAmount(string resourceName, float amount) {
        return resourcesDict[resourceName] >= amount;
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
        return resourcesDict[resourceName] >= product.amount;
    }
    #endregion

    #region remove resource
    public void RemoveResources(Dictionary<string, float> resources) {
        foreach (KeyValuePair<string, float> pair in resources) {
            RemoveResource(pair.Key, pair.Value);
        }
    }

    public void RemoveResource(string resourceName, float amount) {
        resourcesDict[resourceName] -= amount;
        if (resourcesDict[resourceName] < 0)
            resourcesDict[resourceName] = 0;
    }

    public void RemoveResources(ResourceContainer[] products) {
        foreach (ResourceContainer product in products) {
            RemoveResource(product);
        }
    }

    public void RemoveResource(ResourceContainer product) {
        string resourceName = DataManager.ResourcesData.dataArray[product.id].Name;
        resourcesDict[resourceName] -= product.amount;
        if (resourcesDict[resourceName] < 0)
            resourcesDict[resourceName] = 0;
    }
    #endregion
}
