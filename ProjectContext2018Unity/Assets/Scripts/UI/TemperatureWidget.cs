using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureWidget : MonoBehaviour {

    [SerializeField] private TemperatureEventsManager eventsManager;
    [SerializeField] private Slider slider;
    [SerializeField] private WorldTemperature temperature;
    [SerializeField] private Text temperatureText;
    [SerializeField] private TemperatureEventSlider eventSlider;
    [SerializeField] private Transform eventSlidersParent;

    private TemperatureEventSlider[] sliders;

    private void Start() {
        slider.maxValue = temperature.MaxTemperature;
        slider.minValue = temperature.StartingTemperature;

        sliders = new TemperatureEventSlider[eventsManager.events.Length];
        for(int i = 0; i < eventsManager.events.Length; i++) {
            sliders[i] = InstantiateEventSliders(eventsManager.events[i]);
        }

        TemperatureEventsManager.OnEventTriggered += RemoveSlider;
    }

    private void OnDestroy() {
        TemperatureEventsManager.OnEventTriggered -= RemoveSlider;
    }

    private void Update() {
        slider.value = temperature.CurrentTemperature;
        temperatureText.text = ((int)(temperature.CurrentTemperature)).ToString() + "C";
    }

    private void RemoveSlider(float newWaterLevel) {
        Destroy(sliders[eventsManager.currentEventIndex].gameObject);
    }

    private TemperatureEventSlider InstantiateEventSliders(TemperatureEvent e) {
        TemperatureEventSlider slider = Instantiate(eventSlider);
        slider.transform.SetParent(eventSlidersParent, false);
        slider.transform.SetAsFirstSibling();
        slider.Init(e.temperatureTrigger, temperature.StartingTemperature, temperature.MaxTemperature);
        return slider;
    }
}
