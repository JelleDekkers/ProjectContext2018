using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using CityView.Construction;

namespace CityView.UI {

    public class BuildingSelectionWidgetItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

        [SerializeField] private Image img;
        [SerializeField] private Button button;
        [SerializeField] private Color incorrectClimateColor = Color.red;

        public static Action<int, System.Object> OnPointerEnterEvent;
        public static Action OnPointerExitEvent;

        private int id;
        private System.Object data;
        private bool isCorrectClimate;
        
        private void OnDisable() {
            PlayerResources.OnResourceChanged -= UpdateInteractableStateBuilding;
            PlayerResources.OnMoneyChanged -= UpdateInteractableStateBuilding;
            Construction.BuildMode.OnBuildingPlaced -= UpdateInteractableStateBuilding;

            PlayerResources.OnResourceChanged -= UpdateInteractableStateClimateBuilding;
            PlayerResources.OnMoneyChanged -= UpdateInteractableStateClimateBuilding;
            Construction.BuildModeClimateBuildings.OnBuildingPlaced -= UpdateInteractableStateClimateBuilding;
        }

        public void Init(int id, BuildingPrefabs prefab, BuildingsData data, bool isCorrectClimate, BuildModeBase buildMode) {
            this.id = id;
            this.data = data;
            this.isCorrectClimate = isCorrectClimate;
            img.sprite = prefab.GetBuildingSprite(id);
            button.onClick.AddListener(() => buildMode.SelectBuilding(id));
            button.interactable = Building.IsBuildingBuildable(id);
            PlayerResources.OnResourceChanged += UpdateInteractableStateBuilding;
            PlayerResources.OnMoneyChanged += UpdateInteractableStateBuilding;
            Construction.BuildMode.OnBuildingPlaced += UpdateInteractableStateBuilding;

            if (!isCorrectClimate) {
                button.interactable = false;
                img.color = incorrectClimateColor;
            }
        }

        public void Init(int id, BuildingPrefabs prefab, ClimateBuildingsData data, bool isCorrectClimate, BuildModeBase buildMode) {
            this.id = id;
            this.data = data;
            this.isCorrectClimate = isCorrectClimate;
            img.sprite = prefab.GetBuildingSprite(id);
            button.onClick.AddListener(() => buildMode.SelectBuilding(id));
            button.interactable = ClimateBuilding.IsBuildingBuildable(id);
            PlayerResources.OnResourceChanged += (x, y) => UpdateInteractableStateClimateBuilding();
            PlayerResources.OnMoneyChanged += (x) => UpdateInteractableStateClimateBuilding();
            Construction.BuildModeClimateBuildings.OnBuildingPlaced += (x, y) => UpdateInteractableStateClimateBuilding();

            if(!isCorrectClimate) {
                button.interactable = false;
                img.color = incorrectClimateColor;
            }
        }

        private void UpdateInteractableStateBuilding() {
            if(isCorrectClimate)
                button.interactable = Building.IsBuildingBuildable(id);
        }

        private void UpdateInteractableStateBuilding(float money) {
            UpdateInteractableStateBuilding();
        }

        private void UpdateInteractableStateBuilding(int resourceId, int amount) {
            UpdateInteractableStateBuilding();
        }

        private void UpdateInteractableStateBuilding(BuildingBase building, BuildingsData data) {
            UpdateInteractableStateBuilding();
        }

        private void UpdateInteractableStateClimateBuilding() {
            if(button != null)
                button.interactable = ClimateBuilding.IsBuildingBuildable(id);
        }

        private void UpdateInteractableStateClimateBuilding(float money) {
            UpdateInteractableStateClimateBuilding();
        }

        private void UpdateInteractableStateClimateBuilding(int resourceId, int amount) {
            UpdateInteractableStateClimateBuilding();
        }

        private void UpdateInteractableStateClimateBuilding(BuildingBase building, ClimateBuildingsData data) {
            UpdateInteractableStateClimateBuilding();
        }


        public void OnPointerEnter(PointerEventData eventData) {
            OnPointerEnterEvent(id, data);
        }

        public void OnPointerExit(PointerEventData eventData) {
            OnPointerExitEvent();
        }
    }
}
