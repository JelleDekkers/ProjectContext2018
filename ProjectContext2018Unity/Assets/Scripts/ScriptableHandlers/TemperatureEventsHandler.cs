using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureEventsHandler : MonoBehaviour {

    [SerializeField] private TemperatureEventsManager temperatureEventsManager;

    private void Start() {
        temperatureEventsManager.Reset();
        TemperatureEventsManager.OnEventTriggered += (float x) => Debug.Log("event triggered raise water: " + x);
    }

    private void Update() {
        if (temperatureEventsManager.CheckIfNextEventConditionIsMet()) {
            temperatureEventsManager.TriggerNextEvent();

            if (temperatureEventsManager.currentEventIndex >= temperatureEventsManager.events.Length - 1)
                enabled = false;
        }
    }
}
