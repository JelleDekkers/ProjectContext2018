using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureEventSlider : MonoBehaviour {

	public Text temperatureTxt;
    public Slider slider;

    public void Init(float temperature, float min, float max) {
        temperatureTxt.text = string.Format("{0:0.0}", temperature);
        slider.minValue = min;
        slider.maxValue = max;
        slider.value = temperature;
    }
}
