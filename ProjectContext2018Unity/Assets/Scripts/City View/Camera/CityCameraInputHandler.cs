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

        public BuildingBase BuildingHoveringOver { get; private set; }
        private BuildingBase[] buildingsHoveringOver;

        private void Start() {
            Construction.BuildMode.OnBuildStateToggled += ToggleActiveState;
            Construction.BuildModeClimateBuildings.OnBuildStateToggled += ToggleActiveState;
        }

        private void ToggleActiveState(bool toggle) {
            enabled = !toggle;
        }

        private void Update() {
            if (EventSystem.current.IsPointerOverGameObject()) {
                BuildingHoveringOver = null;
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
                buildingsHoveringOver = hit.collider.gameObject.GetComponents<BuildingBase>();  // Uses getComponents to include disabled components
                if (buildingsHoveringOver.Length > 0) 
                    SetBuildingHoveringOver(buildingsHoveringOver[0]);
            } else {
                SetBuildingHoveringOver(null);
            }
        }

        private void OnClick() {
            if (BuildingHoveringOver == null)
                return;

            if (BuildingHoveringOver.GetType() == typeof(Building) || BuildingHoveringOver.GetType() == typeof(House))
                OnPlacedBuildingSelected(BuildingHoveringOver as Building);
            else if(BuildingHoveringOver.GetType() == typeof(Dike))
                OnPlacedClimateBuildingSelected(BuildingHoveringOver as Dike);
        }

        private void SetBuildingHoveringOver(BuildingBase building) {
            if (BuildingHoveringOver != null)
                BuildingHoveringOver.OnHoverExit();
            BuildingHoveringOver = building;
            if (BuildingHoveringOver != null)
                BuildingHoveringOver.OnHoverEnter(onHoverOutlineWidth);
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