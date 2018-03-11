using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulationCounterWidget : MonoBehaviour {

    [SerializeField] private Text counter;

    private void Start() {
        counter.text = Population.Instance.Inhabitants.ToString();
        Population.OnInhabitantsCountChanged += UpdateCounter;
    }

    private void OnDestroy() {
        Population.OnInhabitantsCountChanged -= UpdateCounter;
    }

    private void UpdateCounter(float amount) {
        counter.text = amount.ToString();
    }
}
