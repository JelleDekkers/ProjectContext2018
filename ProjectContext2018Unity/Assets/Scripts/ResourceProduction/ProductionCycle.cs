﻿using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProductionCycle {

    public float ProductionTime { get; private set; }
    public float Money { get; private set; }
    public int[] ResourcesID { get; private set; }
    public int[] ResourcesAmount { get; private set; }
    public float Pollution { get; private set; }

    [SerializeField]
    private float timer = 0;
    public float Timer { get { return timer; } }

    private Action<ProductionCycleResult> OnComplete;

    public ProductionCycle(BuildingsData data, Action<ProductionCycleResult> OnComplete) {
        ProductionTime = data.Productiontime;
        Money = data.Moneyoutput;
        ResourcesID = data.Resourceoutput;
        ResourcesAmount = data.Resourceoutputamount;
        Pollution = data.Pollution;

        PlayerResources.Instance.RemoveResources(data.Resourceinput, data.Resourceinputamount);
        PlayerResources.Instance.RemoveMoney(data.Moneyinput);

        this.OnComplete = OnComplete;
        timer = ProductionTime;
    }

    public void UpdateProduction() {
        if(timer > 0) {
            timer -= Time.deltaTime;
        } else {
            OnCycleCompleted();
            timer = ProductionTime;
        }
    }

    private void OnCycleCompleted() {
        ProductionCycleResult result = new ProductionCycleResult {
            money = Money,
            pollutionPoints = Pollution
        };

        ResourceContainer[] producedResources = new ResourceContainer[ResourcesID.Length];
        for (int i = 0; i < producedResources.Length; i++) 
            producedResources[i] = new ResourceContainer(ResourcesID[i], ResourcesAmount[i]);
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