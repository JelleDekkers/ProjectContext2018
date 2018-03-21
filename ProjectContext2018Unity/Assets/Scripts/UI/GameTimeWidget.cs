using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimeWidget : MonoBehaviour {

    [SerializeField] private Slider slider;
    [SerializeField] private GameTime time;
    [SerializeField] private Text yearTxt;

    private void Update() {
        slider.value = (time.Counter / time.TimePerYear) - (time.CurrentYear - time.StartingYear);
        yearTxt.text = time.CurrentYear.ToString();
    }
}
