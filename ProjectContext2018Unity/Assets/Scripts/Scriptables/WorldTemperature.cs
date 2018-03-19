using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "World Temperature", menuName = "Scriptables/World Temperature", order = 1)]
public class WorldTemperature : ScriptableObject {

    [SerializeField] private float temperatureIncreasePerPollution = 1f;
    public float TemperatureIncreasePerPollution { get { return temperatureIncreasePerPollution; } }

    [SerializeField] private float startingTemperature = 22f;
    public float StartingTemperature { get { return startingTemperature; } }

    [SerializeField] private float currentTemperature;
    public float CurrentTemperature { get { return currentTemperature; } }

    [SerializeField] private float maxTemperature;
    public float MaxTemperature { get { return maxTemperature; } }

    public static Action<float> OnWorldTemperatureChanged;
    public static Action OnWorldTemperatureMaxReached;

    public void Init() {
        currentTemperature = startingTemperature;
    }

    public void AddPollution(float pollution) {
        currentTemperature += pollution * temperatureIncreasePerPollution;
        if (OnWorldTemperatureChanged != null) {
            OnWorldTemperatureChanged(currentTemperature);
        }

        if (currentTemperature >= MaxTemperature)
            OnWorldTemperatureMaxReached();
    }
}
