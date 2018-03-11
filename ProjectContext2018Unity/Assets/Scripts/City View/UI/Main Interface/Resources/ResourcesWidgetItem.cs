using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace CityView.UI {

    public class ResourcesWidgetItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler { 

        [SerializeField] protected int resourceID;
        [SerializeField] protected Image img;
        [SerializeField] protected Text amountText;
        [SerializeField] protected Vector3 tooltipOffset = new Vector3(0, -0.5f, 0);

        protected string tooltipText;
        protected RectTransform rect;
        
        protected virtual void Start() {
            img.sprite = DataManager.ResourcePrefabs.GetResourceSprite(resourceID);
            string resourceName = DataManager.ResourcesData.dataArray[resourceID].Name;
            float amount = PlayerResources.Resources[resourceName];
            amountText.text = Mathf.RoundToInt(amount).ToString();
            PlayerResources.OnResourceChanged += UpdateAmount;
            tooltipText = resourceName;
            rect = GetComponent<RectTransform>();
        }

        protected void UpdateAmount(int id, int newAmount) {
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
