using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Population", menuName = "Scriptables/Population", order = 5)]
public class Population : ScriptableObjectSingleton<Population> {

    [SerializeField] private int inhabitants = 1;
    public int Inhabitants { get { return inhabitants; } }

    [SerializeField] private int inhabitantsPerHouse = 9;
    public int InhabitantsPerHouse { get { return inhabitantsPerHouse; } }

    public static Action<int> OnInhabitantsCountChanged;

    public void Init() {
        CityView.House.OnHouseHabited += OnHouseBuilt;
        CityView.House.OnHouseUninhabitated += OnHouseUninhabitable;
    }

    public void Reset() {
        inhabitants = 0;
        CityView.House.OnHouseHabited -= OnHouseBuilt;
        CityView.House.OnHouseUninhabitated -= OnHouseUninhabitable;
    }

    public void OnHouseBuilt() {
        inhabitants += inhabitantsPerHouse;

        if (OnInhabitantsCountChanged != null)
            OnInhabitantsCountChanged(Inhabitants);
    }

    public void OnHouseUninhabitable() {
        inhabitants -= inhabitantsPerHouse;

        if (OnInhabitantsCountChanged != null)
            OnInhabitantsCountChanged(Inhabitants);
    }
}
