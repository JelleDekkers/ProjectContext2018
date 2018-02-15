using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SerializerExtension {

    public static int GetResourceID(string resourceName, out bool isResourceType) {
        try {
            int i = 0;
            foreach (GameResourcesData resource in DataManager.Instance.gameResources.dataArray) {
                if (resourceName.ToLower() == resource.Name.ToLower()) {
                    isResourceType = true;
                    return resource.ID;
                }
                i++;
            }

            isResourceType = false;
            return -1;
        }catch(System.Exception ex) {
            Debug.LogError("Something went wrong: " + ex);
            isResourceType = false;
            return -1;
        }
    }
}
