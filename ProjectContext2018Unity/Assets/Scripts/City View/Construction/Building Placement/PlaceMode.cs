using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.Construction {

    public class PlaceMode : BuildMode {

        [SerializeField] private Building buildingPrefab;
        [SerializeField] private BuildingGhost buildingGhost;
        [SerializeField] private BuildingPlacementEffectHandler placeEffect;

        private Tile[,] tilesHoveringOver;
        private bool isHittingGrid;

        public override void OnStart() {
            buildingGhost.Setup(buildingPrefab);
            buildingGhost.gameObject.SetActive(true);
        }
        
        public override void OnEnd() {
            buildingGhost.gameObject.SetActive(false);
            RevertTileColors();
        }

        public override void UpdateMode() {
            //placeholder
            if (buildingPrefab == null)
                return;

            RevertTileColors();
            tilesHoveringOver = GetTilesAtPosition(RaycastHelper.GetMousePositionInScene(out isHittingGrid), buildingPrefab.Size);
            HighlightUnbuildableTiles();

            if (Input.GetMouseButtonDown(0))
                OnClick();
        }

        private void HighlightUnbuildableTiles() {
            foreach(Tile t in tilesHoveringOver) {
                if (t == null)
                    continue;
                else if(!TileIsBuildable(t))
                    t.SetColorToUnbuildable();
            }
        }

        private void RevertTileColors() {
            if (tilesHoveringOver != null) {
                foreach (Tile t in tilesHoveringOver) {
                    if (t != null)
                        t.ResetColor();
                }
            }
        }

        private void OnClick() {
            if (!isHittingGrid)
                return;

            if(CanBePlacedAtTiles(tilesHoveringOver))// && BuildingSelector.SelectedBuilding.CanBeBought())
                Build(tilesHoveringOver);
        }

        private bool CanBePlacedAtTiles(Tile[,] tiles) {
            foreach(Tile t in tiles) {
                if (t == null || !TileIsBuildable(t))
                    return false;
            }
            return true;
        }

        private bool TileIsBuildable(Tile t) {
            return t.occupant == null;
        }

        private void Build(Tile[,] tiles) {
            Vector3 pos = tiles[0, 0].transform.position;
            pos.x += Tile.SIZE.x / 2;
            pos.z += Tile.SIZE.z / 2;
            Building b = Instantiate(buildingPrefab, pos, Quaternion.identity, City.Instance.transform);
            foreach (Tile t in tiles)
                t.occupant = b;
            Instantiate(placeEffect).Setup(b);
        }

        private Tile[,] GetTilesAtPosition(Vector3 position, IntVector2 buildingSize) {
            IntVector2 coordinate = IntVector2.ConvertToCoordinates(position);
            Tile[,] tiles = new Tile[buildingSize.x, buildingSize.z];
            for (int x = 0; x < buildingSize.x; x++) {
                for (int z = 0; z < buildingSize.z; z++) {
                    if (City.Instance.grid.IsInsideGrid(new IntVector2(coordinate.x + x, coordinate.z + z))) {
                        Tile t = City.Instance.grid.Grid[coordinate.x + x, coordinate.z + z];
                        tiles[x, z] = t;
                    }
                }
            }

            return tiles;
        }
    }
}