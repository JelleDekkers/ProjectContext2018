using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TemperatureWidget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    [SerializeField] private TemperatureEventsManager eventsManager;
    [SerializeField] private Slider slider;
    [SerializeField] private WorldTemperature temperature;
    [SerializeField] private Text temperatureText;
    [SerializeField] private TemperatureEventSlider eventSlider;
    [SerializeField] private Transform eventSlidersParent;
    [SerializeField] private TemperatureInfoPanel infoPanel;

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

    public void OnPointerEnter(PointerEventData eventData) {
        infoPanel.gameObject.SetActive(true);
        if (eventsManager.currentEventIndex < eventsManager.events.Length - 1)
            infoPanel.SetText(eventsManager.events[eventsManager.currentEventIndex + 1].temperatureTrigger, eventsManager.events[eventsManager.currentEventIndex + 1].waterLevel);
        else
            infoPanel.SetTextFinalEvent(temperature.MaxTemperature);
    }

    public void OnPointerExit(PointerEventData eventData) {
        infoPanel.gameObject.SetActive(false);
    }
}
