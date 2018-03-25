using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Temperature Event", menuName = "Scriptables/Temperature Event", order = 7)]
public class TemperatureEvent : ScriptableObject {

    public int temperatureTrigger;
    public float waterLevel;
    public float waterLevelTextMultiplier = 4;
}
