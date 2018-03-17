using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Population", menuName = "Scriptables/Population", order = 5)]
public class Population : ScriptableObjectSingleton<Population> {

    [SerializeField] private int totalPopulation = 1;
    public int TotalPopulation { get { return totalPopulation; } }

    public static Action<int> OnPopulationCountChanged;

    public void Init() {
        CityView.House.OnHousePaused += OnHousePaused;
        CityView.House.OnNewInhabitant += OnNewInhabitant;
    }

    public void Reset() {
        totalPopulation = 0;
        CityView.House.OnHousePaused -= OnHousePaused;
        CityView.House.OnNewInhabitant -= OnNewInhabitant;
    }

    public void OnNewInhabitant(CityView.Building building, int amount) {
        totalPopulation += amount; 
        if (OnPopulationCountChanged != null)
            OnPopulationCountChanged(TotalPopulation);
    }

    public void OnHousePaused(CityView.Building building, int inhabitants) {
        totalPopulation -= inhabitants;
    }
}
