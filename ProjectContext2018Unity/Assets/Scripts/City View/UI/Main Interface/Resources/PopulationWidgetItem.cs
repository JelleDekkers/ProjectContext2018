using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace CityView.UI {

    public class PopulationWidgetItem : ResourcesWidgetItem {

        protected override void Start() {
            img.sprite = DataManager.ResourcePrefabs.PopulationSprite;
            amountText.text = Population.Instance.TotalPopulation.ToString();
            tooltipText = "Population";
            rect = GetComponent<RectTransform>();
            Population.OnPopulationCountChanged += UpdateAmount;
        }

        private void UpdateAmount(int newAmount) {
            amountText.text = newAmount.ToString();
        }

        private void OnDestroy() {
            Population.OnPopulationCountChanged -= UpdateAmount;
        }

    }
}