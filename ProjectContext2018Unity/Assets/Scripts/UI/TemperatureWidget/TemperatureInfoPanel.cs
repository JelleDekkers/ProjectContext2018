using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureInfoPanel : MonoBehaviour {

    [SerializeField] private Text info;

    public void SetText(float temperature, float waterLevel) {
        info.text = "Next event at " + string.Format("{0:0.0}", temperature) + "C , water level will rise to " + waterLevel + " meter";
    }

    public void SetTextFinalEvent(float maxTemperature) {
        info.text = "Game Over at " + string.Format("{0:0.0}", maxTemperature);
    }
}
