using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace CityView.Construction {

    public class DestroyMode : MonoBehaviour {

        [SerializeField] private CityCameraInputHandler cityCam;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float onHoverOutlineWidth;
        [SerializeField] private Texture2D demolishMouseCursor;
        [SerializeField] private Vector2 cursorHotspot = Vector2.zero;

        private Ray ray;
        private RaycastHit hit;

        public BuildingBase BuildingHoveringOver { get; private set; }
        private BuildingBase[] buildingsHoveringOver;

        private void OnEnable() {
            cityCam.enabled = false;
            Cursor.SetCursor(demolishMouseCursor, cursorHotspot, CursorMode.Auto);
        }

        private void OnDisable() {
            SetBuildingHoveringOver(null);
            if(cityCam != null)
                cityCam.enabled = true;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        private void Update() {
            if (Input.GetMouseButtonDown(0))
                OnClick();
            else
                OnHover();
        }

        private void OnClick() {
            //if (EventSystem.current.IsPointerOverGameObject()) {
            //    BuildingHoveringOver = null;
            //    enabled = false;
            //    return;
            //}

            if (BuildingHoveringOver == null)
                return;

            DemolishBuilding(BuildingHoveringOver);
        }

        private void DemolishBuilding(BuildingBase building) {
            Building.OnDemolishInitiated(building);
            building.enabled = false;
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

        private void SetBuildingHoveringOver(BuildingBase building) {
            if (BuildingHoveringOver != null)
                BuildingHoveringOver.OnHoverExit();
            BuildingHoveringOver = building;
            if (BuildingHoveringOver != null)
                BuildingHoveringOver.OnHoverEnter(onHoverOutlineWidth);
        }
    }
}