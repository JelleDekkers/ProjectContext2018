﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.Construction {

    public class DestroyMode : BuildMode {

        private bool isHittingGrid;

        public override void OnStart() { }
        public override void OnEnd() { }

        public override void UpdateMode() {
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
            Destroy(b.gameObject);
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