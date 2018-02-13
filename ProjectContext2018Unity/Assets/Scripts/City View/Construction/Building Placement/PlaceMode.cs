using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.Construction {

    public class PlaceMode : BuildMode {

        public override Builder Builder { get; set; }

        [SerializeField] private Building buildingPrefab;
        [SerializeField] private BuildingGhost buildingGhost;

        public override void OnStart(Builder builder) {
            Builder = builder;
        }

        public override void OnEnd() {
            throw new System.NotImplementedException();
        }

        public override void UpdateMode() {
            if (buildingPrefab == null)
                return;

            if (Input.GetMouseButtonDown(0))
                OnClick();
        }

        private void OnClick() {
            bool isHittingGrid;
            Tile[,] tiles = GetTilesAtPosition(RaycastHelper.GetMousePositionInScene(out isHittingGrid), buildingPrefab.Size);
            if (!isHittingGrid)
                return;

            if(CanBePlaced(tiles))// && BuildingSelector.SelectedBuilding.CanBeBought())
                Build(tiles);
        }

        private bool CanBePlaced(Tile[,] tiles) {
            foreach(Tile t in tiles) {
                if (t.occupant != null)
                    return false;
            }
            return true;
        }

        private void Build(Tile[,] tiles) {
            Building b = Instantiate(buildingPrefab, tiles[0, 0].transform.position, Quaternion.identity, City.Instance.transform);
            foreach (Tile t in tiles)
                t.occupant = b;
        }

        private Tile[,] GetTilesAtPosition(Vector3 position, IntVector2 buildingSize) {
            IntVector2 coordinate = ConvertToCoordinates(position);
            Tile[,] tiles = new Tile[buildingSize.x, buildingSize.z];
            for (int x = 0; x < buildingSize.x; x++) {
                for (int z = 0; z < buildingSize.z; z++) {
                    if (City.Instance.grid.IsInsideGrid(coordinate.x + x, coordinate.z + z)) {
                        Tile t = City.Instance.grid.Grid[coordinate.x + x, coordinate.z + z];
                        tiles[x, z] = t;
                    }
                }
            }

            return tiles;
        }

        private IntVector2 ConvertToCoordinates(Vector3 point) {
            return new IntVector2((int)Mathf.Round(point.x), (int)Mathf.Round(point.z));
        }
    }
}