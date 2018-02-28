using System;
using UnityEngine;

[Serializable]
public class ProductionCycle {

    [SerializeField]
    private float timer = 0;

    private Action<ProductionCycleResult> OnComplete;

    private float productionTime;
    private float money;
    private int[] resourcesID;
    private float[] resourcesAmount;
    private float pollution;

	public ProductionCycle(BuildingsData data, Action<ProductionCycleResult> OnComplete) {
        productionTime = data.Productiontime;
        money = data.Incomemoney;
        resourcesID = data.Incomeresources;
        resourcesAmount = data.Incomeresourcesamount;
        pollution = data.Pollution;

        this.OnComplete = OnComplete;
        timer = productionTime;
    }

    public void UpdateProduction() {
        if(timer > 0) {
            timer -= Time.deltaTime;
        } else {
            OnCycleCompleted();
            timer = productionTime;
        }
    }

    private void OnCycleCompleted() {
        ProductionCycleResult result = new ProductionCycleResult {
            money = money,
            pollutionPoints = pollution
        };

        ResourceContainer[] producedResources = new ResourceContainer[resourcesID.Length];
        for (int i = 0; i < producedResources.Length; i++) 
            producedResources[i] = new ResourceContainer(resourcesID[i], resourcesAmount[i]);
        result.producedResources = producedResources;

        OnComplete(result);
    }
}

public class ProductionCycleResult {
    public ResourceContainer[] producedResources;
    public float money;
    public float researchPoints;
    public float pollutionPoints;
}