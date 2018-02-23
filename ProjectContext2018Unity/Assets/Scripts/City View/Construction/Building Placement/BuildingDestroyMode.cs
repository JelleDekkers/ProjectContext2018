using System;
using UnityEngine;

namespace CityView.Construction {

    public class BuildingDestroyMode : BuildMode {

        private bool isHittingGrid;

        public static Action<Building> OnBuildingRemoved;

        public override void OnStart() {
            enabled = true;
        }

        public override void OnEnd() {
            enabled = false;
        }

        [SerializeField]
        private AudioClip DemolishSFX;

        public override void Update() {
            if (Input.GetMouseButtonDown(0))
                OnClick();
        }

        private void OnClick() {
            Tile t = GetTileAtPosition(RaycastHelper.GetMousePositionInScene(out isHittingGrid));
            if (t != null && t.occupant != null)
                DestroyBuilding(t.occupant); ;
        }

        private Building GetBuildingAt(Tile t) {
            return t.occupant;
        }

        private void DestroyBuilding(Building b) {
            OnBuildingRemoved(b);
            Destroy(b.gameObject);
            CityCamera.Instance.audioSource.PlayOneShot(DemolishSFX);
        }

        private Tile GetTileAtPosition(Vector3 position) {
            IntVector2 coordinates = IntVector2.ConvertToCoordinates(position);
            if (!City.Instance.grid.IsInsideGrid(coordinates))
                return null;

            Tile tile = City.Instance.grid.Grid[coordinates.x, coordinates.z];
            return tile;
        }
    }
}