using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulationCounterWidget : MonoBehaviour {

    [SerializeField] private Text counter;

    private void Start() {
        counter.text = Population.Instance.TotalPopulation.ToString();
        Population.OnPopulationCountChanged += UpdateCounter;
    }

    private void OnDestroy() {
        Population.OnPopulationCountChanged -= UpdateCounter;
    }

    private void UpdateCounter(int amount) {
        counter.text = amount.ToString();
    }
}
