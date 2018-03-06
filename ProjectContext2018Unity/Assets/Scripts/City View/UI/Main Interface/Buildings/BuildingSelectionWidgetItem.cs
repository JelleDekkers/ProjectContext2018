using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace CityView.UI {

    public class BuildingSelectionWidgetItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

        [SerializeField] private Image img;
        [SerializeField] private Button button;

        private int id;

        public static Action<int> OnPointerEnterEvent;
        public static Action OnPointerExitEvent;

        public bool buildable;
        public bool hasmoney;
        public bool hasResources;
        public float cost;

        public void Init(int id) {
            this.id = id;
            img.sprite = DataManager.BuildingPrefabs.GetBuildingSprite(id);
            button.onClick.AddListener(() => BuildingSelectionWidget.OnBuildingSelected(id));
            button.interactable = Building.IsBuildable(id);
            PlayerResources.OnResourceChanged += UpdateInteractableState;
            PlayerResources.OnMoneyChanged += UpdateInteractableState;
        }

        private void UpdateInteractableState(float money) {
            button.interactable = Building.IsBuildable(id);
        }

        private void UpdateInteractableState(int resourceId, int resourceAmount) {
            button.interactable = Building.IsBuildable(id);
        }
        
        public void OnPointerEnter(PointerEventData eventData) {
            OnPointerEnterEvent(id);
        }

        public void OnPointerExit(PointerEventData eventData) {
            OnPointerExitEvent();
        }
    }
}
