using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Temperature Events Manager", menuName = "Scriptables/Temperature Events Manager", order = 6)]
public class TemperatureEventsManager : ScriptableObject {

    public static Action<float> OnEventTriggered;

    public WorldTemperature temperature;
    public TemperatureEvent[] events;
    public int currentEventIndex = 0;

    public bool CheckIfNextEventConditionIsMet() {
        return temperature.CurrentTemperature >= events[currentEventIndex].temperatureTrigger;
    }

    public void TriggerNextEvent() {
        OnEventTriggered(events[currentEventIndex].waterLevel);
        currentEventIndex++;
    }

    public void Reset() {
        currentEventIndex = 0;
    }
}
