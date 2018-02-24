using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResourcesDebugger : MonoBehaviour {

    public bool useDebugOptions;
    public float money;
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
        money = PlayerResources.Money;
        foreach (string key in keys) 
            resourcesDict[key] = PlayerResources.Resources[key];
    }

    private void OnGUI() {
        if (!useDebugOptions)
            return;

        ResourceCheat();
        MoneyCheat();
    }

    private void ResourceCheat() {
        cheatID = int.Parse(GUI.TextField(new Rect(10, 100, 40, 20), cheatID.ToString()));
        cheatAmount = int.Parse(GUI.TextField(new Rect(50, 100, 100, 20), cheatAmount.ToString()));

        if (GUI.Button(new Rect(150, 100, 50, 20), "Add")) {
            if (cheatAmount > 0)
                PlayerResources.AddResource(cheatID, cheatAmount);
            else
                PlayerResources.RemoveResource(cheatID, cheatAmount);
        }
    }

    private void MoneyCheat() {
        GUI.Label(new Rect(10, 125, 40, 20), "Cash: ");
        cheatAmount = int.Parse(GUI.TextField(new Rect(50, 125, 100, 20), cheatAmount.ToString()));

        if (GUI.Button(new Rect(150, 125, 50, 20), "Add")) {
            if (cheatAmount > 0)
                PlayerResources.AddMoney(cheatAmount);
            else
                PlayerResources.RemoveMoney(cheatAmount);
        }
    }
}
