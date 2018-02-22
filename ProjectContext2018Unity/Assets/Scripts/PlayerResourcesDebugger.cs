using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResourcesDebugger : MonoBehaviour {

    public bool useDebugOptions;
    public StockPileResourceDictionary resourcesDict;

    private List<string> keys = new List<string>();

    private int cheatID;
    private int cheatAmount;
    
    private void Start() {
        resourcesDict = new StockPileResourceDictionary();
        foreach (KeyValuePair<string, float> pair in PlayerResources.Resources) {
            resourcesDict.Add(pair.Key, pair.Value);
            keys.Add(pair.Key);
        }
    }

    private void Update() {
        foreach (string key in keys) {
            resourcesDict[key] = PlayerResources.Resources[key];
        }
    }

    private void OnGUI() {
        if (!useDebugOptions)
            return;

        cheatID = int.Parse(GUI.TextField(new Rect(10, 400, 40, 20), cheatID.ToString()));
        cheatAmount = int.Parse(GUI.TextField(new Rect(50, 400, 100, 20), cheatAmount.ToString()));

        if(GUI.Button(new Rect(150, 400, 50, 20), "Add")) {
            if (cheatAmount > 0)
                PlayerResources.AddResource(cheatID, cheatAmount);
            else 
                PlayerResources.RemoveResource(cheatID, cheatAmount);
        }
    }
}
