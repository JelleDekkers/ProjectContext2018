using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace CityView.UI {

    public class MoneyWidgetItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

        [SerializeField]
        private Image img;
        [SerializeField]
        private Text amountText;

        private string tooltipText;
        private RectTransform rect;
        private readonly Vector3 tooltipOffset = new Vector3(0, -0.5f, 0);

        private void Start() {
            img.sprite = DataManager.ResourcePrefabs.MoneySprite;
            amountText.text = Mathf.RoundToInt(PlayerResources.Money).ToString();
            PlayerResources.OnMoneyChanged += UpdateAmount;
            tooltipText = "Money";
            rect = GetComponent<RectTransform>();
        }

        private void UpdateAmount(float newAmount) {
            amountText.text = Mathf.RoundToInt(newAmount).ToString();
        }

        public void OnPointerEnter(PointerEventData eventData) {
            TooltipWidget.Instance.Show(tooltipText, rect.position, tooltipOffset);
        }

        public void OnPointerExit(PointerEventData eventData) {
            TooltipWidget.Instance.Hide();
        }
    }
}
