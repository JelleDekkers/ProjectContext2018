using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Population", menuName = "Scriptables/Population", order = 5)]
public class Population : ScriptableObjectSingleton<Population> {

    [SerializeField] private float inhabitants = 1f;
    public float Inhabitants { get { return inhabitants; } }

    [SerializeField] private float inhabitantsPerHouse = 10;
    public float InhabitantsPerHouse { get { return inhabitantsPerHouse; } }

    public static Action<float> OnInhabitantsCountChanged;

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
