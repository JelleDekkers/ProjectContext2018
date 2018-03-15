using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CityView {

    public class CityCameraInputHandler : MonoBehaviour {

        public static Action<Building> OnPlacedBuildingSelected;
        public static Action<ClimateBuilding> OnPlacedClimateBuildingSelected;

        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float onHoverOutlineWidth;

        private Ray ray;
        private RaycastHit hit;

        private BuildingBase buildingHoveringOver;

        private void Start() {
            Construction.BuildMode.OnBuildStateToggled += ToggleActiveState;
            Construction.BuildModeClimateBuildings.OnBuildStateToggled += ToggleActiveState;
        }

        private void ToggleActiveState(bool toggle) {
            enabled = !toggle;
        }

        private void Update() {
            if (EventSystem.current.IsPointerOverGameObject()) {
                buildingHoveringOver = null;
                return;
            }

            if (Input.GetMouseButtonDown(0))
                OnClick();
            else
                OnHover();
        }

        private void OnHover() {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
                BuildingBase[] building = hit.collider.gameObject.GetComponents<BuildingBase>();  // Uses getComponents to include disabled components
                if (building.Length > 0) 
                    SetBuildingHoveringOver(building[0]);
            } else {
                SetBuildingHoveringOver(null);
            }
        }

        private void OnClick() {
            if (buildingHoveringOver == null)
                return;

            if (buildingHoveringOver.GetType() == typeof(Building))
                OnPlacedBuildingSelected(buildingHoveringOver as Building);
            else if(buildingHoveringOver.GetType() == typeof(Dike))
                OnPlacedClimateBuildingSelected(buildingHoveringOver as Dike);
        }

        private void SetBuildingHoveringOver(BuildingBase building) {
            if (buildingHoveringOver != null)
                buildingHoveringOver.OnHoverExit();
            buildingHoveringOver = building;
            if (buildingHoveringOver != null)
                buildingHoveringOver.OnHoverEnter(onHoverOutlineWidth);
        }

        private void OnDisable() {
            SetBuildingHoveringOver(null);
        }

        private void OnDestroy() {
            Construction.BuildMode.OnBuildStateToggled -= ToggleActiveState;
            Construction.BuildModeClimateBuildings.OnBuildStateToggled -= ToggleActiveState;
        }
    }
}