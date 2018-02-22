﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace CityView.UI {

    public class ResourcesWidgetItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler { 

        [SerializeField]
        private int resourceID;
        [SerializeField]
        private Image img;
        [SerializeField]
        private Text amountText;

        private string tooltipText;
        private RectTransform rect;
        private readonly Vector3 tooltipOffset = new Vector3(0, -0.5f, 0);

        private void Start() {
            img.sprite = DataManager.ResourcePrefabs.GetSprite(resourceID);
            string resourceName = DataManager.ResourcesData.dataArray[resourceID].Name;
            float amount = PlayerResources.Resources[resourceName];
            amountText.text = Mathf.RoundToInt(amount).ToString();
            PlayerResources.OnResourceChanged += UpdateAmount;
            tooltipText = resourceName;
            rect = GetComponent<RectTransform>();
        }

        private void UpdateAmount(int id, float newAmount) {
            if(id == resourceID)
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
