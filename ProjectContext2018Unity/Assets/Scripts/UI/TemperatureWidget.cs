using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureWidget : MonoBehaviour {

    [SerializeField] private Slider slider;
    [SerializeField] private WorldTemperature temperature;
    [SerializeField] private Text temperatureText;

    private void Start() {
        slider.maxValue = temperature.MaxTemperature;
        slider.minValue = temperature.StartingTemperature;
    }

    private void Update() {
        slider.value = temperature.CurrentTemperature;
        temperatureText.text = ((int)(temperature.CurrentTemperature)).ToString() + "C";
    }
}
