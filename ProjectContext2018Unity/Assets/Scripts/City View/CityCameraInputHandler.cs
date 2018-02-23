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
            myDelegate = (bool toggle) => enabled = !toggle;
            Construction.BuildMode.OnToggled += myDelegate;
        }

        private void Update() {
            if (Input.GetMouseButtonDown(0))
                OnClick();
        }

        private void OnClick() {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
                Building[] building = hit.collider.gameObject.GetComponents<Building>();
                if (building.Length > 0)
                    OnBuildingSelected(building[0]);
            } //else {
            //    OnBuildingSelected(null);
            //}
        }

        private void OnDestroy() {
            Construction.BuildMode.OnToggled -= myDelegate;
        }
    }
}