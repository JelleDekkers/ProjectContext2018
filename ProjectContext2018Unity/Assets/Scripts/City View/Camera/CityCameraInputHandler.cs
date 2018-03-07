using System;
using UnityEngine;

namespace CityView {

    public class CityCameraInputHandler : MonoBehaviour {

        public static Action<Building> OnPlacedBuildingSelected;
        public static Action<ClimateBuilding> OnPlacedClimateBuildingSelected;

        [SerializeField]
        private LayerMask layerMask;

        private Ray ray;
        private RaycastHit hit;

        private void Start() {
            Construction.BuildMode.OnBuildStateToggled += ToggleActiveState;
            Construction.BuildModeClimateBuildings.OnBuildStateToggled += ToggleActiveState;
        }

        private void ToggleActiveState(bool toggle) {
            enabled = !toggle;
        }

        private void Update() {
            if (Input.GetMouseButtonDown(0))
                OnClick();
        }

        private void OnClick() {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
                BuildingBase[] building = hit.collider.gameObject.GetComponents<BuildingBase>();  // Uses getComponents to include disabled components
                // TODO: netter:
                if (building.Length > 0) {
                    if (building[0].GetType() == typeof(Building))
                        OnPlacedBuildingSelected(building[0] as Building);
                    else if(building[0].GetType() == typeof(Dike))
                        OnPlacedClimateBuildingSelected(building[0] as Dike);
                }
            }
        }

        private void OnDestroy() {
            Construction.BuildMode.OnBuildStateToggled -= ToggleActiveState;
            Construction.BuildModeClimateBuildings.OnBuildStateToggled -= ToggleActiveState;
        }
    }
}