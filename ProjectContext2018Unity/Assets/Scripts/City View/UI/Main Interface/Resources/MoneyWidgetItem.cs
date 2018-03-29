using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace CityView.UI {

    public class MoneyWidgetItem : ResourcesWidgetItem {

        protected override void Start() {
            img.sprite = DataManager.ResourcePrefabs.MoneySprite;
            amountText.text = Mathf.RoundToInt(PlayerResources.Money).ToString();
            PlayerResources.OnMoneyChanged += UpdateAmount;
            tooltipText = "Money";
            rect = GetComponent<RectTransform>();
        }

        private void UpdateAmount(float newAmount) {
            amountText.text = Mathf.RoundToInt(newAmount).ToString();
        }

        private void OnDestroy() {
            PlayerResources.OnMoneyChanged -= UpdateAmount;
        }
    }
}
