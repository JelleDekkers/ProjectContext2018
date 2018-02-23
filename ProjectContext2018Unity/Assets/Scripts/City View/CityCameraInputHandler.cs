using System;
using UnityEngine;

namespace CityView {

    public class CityCameraInputHandler : MonoBehaviour {

        public static Action<Building> OnBuildingSelected;

        [SerializeField]
        private LayerMask layerMask;

        private Ray ray;
        private RaycastHit hit;
        private Action<bool> myDelegate;

        private void Start() {
            myDelegate = (bool toggle) => gameObject.SetActive(toggle);
            //Construction.BuildingPlaceMode.OnModeToggled += myDelegate;
            // moet naar boolean, on unsubscribe bij ondestroy
        }


        private void Update() {
            if (Input.GetMouseButtonDown(0))
                OnClick();
        }

        private void OnClick() {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
                Building building = hit.collider.gameObject.GetComponent<Building>();
                if (building != null)
                    OnBuildingSelected(building);
            } else {
                OnBuildingSelected(null);
            }
        }
    }
}