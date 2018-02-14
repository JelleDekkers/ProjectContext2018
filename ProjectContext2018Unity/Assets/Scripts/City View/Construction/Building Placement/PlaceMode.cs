using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.Construction {

    public class PlaceMode : BuildMode {

        [SerializeField] private BuildingGhost buildingGhost;
        [SerializeField] private BuildingPlacementEffectHandler placeEffect;

        private Tile[,] tilesHoveringOver;
        private bool isHittingGrid;
        private Building selectedBuilding;

        // placeholder
        public Building[] buildingPrefabs;

        public override void OnStart() {
            enabled = true;
        }

        public override void OnEnd() {
            buildingGhost.gameObject.SetActive(false);
            RevertTileColors();
            enabled = false;
        }

        public override void Update() {
            if (selectedBuilding == null)
                return;

            RevertTileColors();
            tilesHoveringOver = GetTilesAtPosition(RaycastHelper.GetMousePositionInScene(out isHittingGrid), selectedBuilding.Size);
            HighlightUnbuildableTiles();

            if (Input.GetMouseButtonDown(0))
                OnMouseClick();
        }

        // placeholder
        private void OnGUI() {
            for (int i = 0; i < buildingPrefabs.Length; i++) {
                if (GUI.Button(new Rect(10, 100 + i * 20, 200, 15), buildingPrefabs[i].name))
                    OnBuildingSelected(i);
            }
        }

        private void OnBuildingSelected(int index) {
            if (selectedBuilding == buildingPrefabs[index]) {
                selectedBuilding = null;
                buildingGhost.gameObject.SetActive(false);
                RevertTileColors();
            } else {
                selectedBuilding = buildingPrefabs[index];
                buildingGhost.Setup(selectedBuilding);
                buildingGhost.gameObject.SetActive(true);
            }
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

        private void OnMouseClick() {
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
            pos.x += Mathf.Round(selectedBuilding.Size.x / 2);
            pos.z += Mathf.Round(selectedBuilding.Size.z / 2);

            Vector3 offset = Vector3.zero;
            offset.x = (float)selectedBuilding.Size.x / 2 - Tile.SIZE.x / 2;
            offset.z = (float)selectedBuilding.Size.z / 2 - Tile.SIZE.z / 2;

            Debug.Log(selectedBuilding.Size + " " + offset);
            pos -= offset;
            
            Building b = Instantiate(selectedBuilding, pos, Quaternion.identity, City.Instance.transform);
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