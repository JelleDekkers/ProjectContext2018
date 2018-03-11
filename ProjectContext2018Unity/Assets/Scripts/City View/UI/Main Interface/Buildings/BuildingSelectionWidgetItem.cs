using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace CityView.UI {

    public class BuildingSelectionWidgetItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

        [SerializeField] private Image img;
        [SerializeField] private Button button;

        public static Action<int, System.Object> OnPointerEnterEvent;
        public static Action OnPointerExitEvent;

        public bool buildable;
        public bool hasmoney;
        public bool hasResources;
        public float cost;

        private int id;
        private System.Object data;

        public void Init(int id, BuildingPrefabs prefab, System.Object data) {
            this.data = data;
            img.sprite = prefab.GetBuildingSprite(id);
            button.onClick.AddListener(() => BuildingSelectionWidget.OnBuildingSelected(id));
            button.interactable = BuildingBase.IsBuildable(id);
            PlayerResources.OnResourceChanged += UpdateInteractableState;
            PlayerResources.OnMoneyChanged += UpdateInteractableState;
        }

        private void UpdateInteractableState(float money) {
            button.interactable = BuildingBase.IsBuildable(id);
        }

        private void UpdateInteractableState(int resourceId, int resourceAmount) {
            button.interactable = BuildingBase.IsBuildable(id);
        }
        
        public void OnPointerEnter(PointerEventData eventData) {
            OnPointerEnterEvent(id, data);
        }

        public void OnPointerExit(PointerEventData eventData) {
            OnPointerExitEvent();
        }
    }
}
