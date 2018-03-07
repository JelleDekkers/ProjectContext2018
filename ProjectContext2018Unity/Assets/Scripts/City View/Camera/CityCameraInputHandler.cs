using System;
using UnityEngine;

namespace CityView {

    public class CityCameraInputHandler : MonoBehaviour {

        public static Action<Building> OnPlacedBuildingSelected;

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
                Building[] building = hit.collider.gameObject.GetComponents<Building>();  // Uses getComponents to include disabled components
                if (building.Length > 0)
                    OnPlacedBuildingSelected(building[0]);
            }
        }

        private void OnDestroy() {
            Construction.BuildMode.OnBuildStateToggled -= ToggleActiveState;
            Construction.BuildModeClimateBuildings.OnBuildStateToggled -= ToggleActiveState;
        }
    }
}