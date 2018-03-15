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

        public void Init(int id, BuildingPrefabs prefab, BuildingsData data) {
            this.data = data;
            img.sprite = prefab.GetBuildingSprite(id);
            button.onClick.AddListener(() => BuildingSelectionWidget.OnBuildingSelected(id));
            button.interactable = Building.IsBuildingBuildable(id);
            PlayerResources.OnResourceChanged += UpdateInteractableState;
            PlayerResources.OnMoneyChanged += UpdateInteractableState;
        }

        public void Init(int id, BuildingPrefabs prefab, ClimateBuildingsData data) {
            this.data = data;
            img.sprite = prefab.GetBuildingSprite(id);
            button.onClick.AddListener(() => BuildingSelectionWidget.OnBuildingSelected(id));
            button.interactable = ClimateBuilding.IsBuildingBuildable(id);
            PlayerResources.OnResourceChanged += UpdateInteractableState;
            PlayerResources.OnMoneyChanged += UpdateInteractableState;
        }

        private void UpdateInteractableState(float money) {
            button.interactable = PlayerResources.Instance.HasMoneyAmount(money);
        }

        private void UpdateInteractableState(int resourceId, int resourceAmount) {
            button.interactable = PlayerResources.Instance.HasResourceAmount(resourceId, resourceAmount);
        }
        
        public void OnPointerEnter(PointerEventData eventData) {
            OnPointerEnterEvent(id, data);
        }

        public void OnPointerExit(PointerEventData eventData) {
            OnPointerExitEvent();
        }

        private void OnDestroy() {
            PlayerResources.OnResourceChanged -= UpdateInteractableState;
            PlayerResources.OnMoneyChanged -= UpdateInteractableState;
        }
    }
}
