using System;
using UnityEngine;

namespace CityView.Construction {

    public class PlaceMode : BuildMode {

        [SerializeField] private BuildingGhost buildingGhost;
        [SerializeField] private BuildingPlacementEffectHandler placeEffect;

        private Tile[,] tilesHoveringOver;
        private bool isHittingGrid;

        private int selectionIndex = -1;
        private Building SelectedBuilding { get { return DataManager.BuildingPrefabs.GetBuilding(selectionIndex); } }
        private BuildingsData SelectedBuildingData { get { return DataManager.BuildingData.dataArray[selectionIndex]; } }

        public static Action<Building, BuildingsData> OnBuildingPlaced;

        public override void OnStart() {
            enabled = true;
        }

        public override void OnEnd() {
            buildingGhost.gameObject.SetActive(false);
            RevertTileColors();
            enabled = false;
        }

        public override void Update() {
            if (selectionIndex == -1)
                return;

            RevertTileColors();
            tilesHoveringOver = GetTilesAtPosition(RaycastHelper.GetMousePositionInScene(out isHittingGrid), new IntVector2(SelectedBuilding.Size));
            HighlightUnbuildableTiles();
            buildingGhost.UpdatePosition(tilesHoveringOver);

            if (Input.GetMouseButtonDown(0))
                OnMouseClick();
        }

        // placeholder
        private void OnGUI() {
            for (int i = 0; i < DataManager.BuildingData.dataArray.Length; i++) {
                if (GUI.Button(new Rect(10, 100 + i * 20, 200, 15), DataManager.BuildingData.dataArray[i].Name))
                    OnBuildingSelected(i);
            }
        }

        private void OnBuildingSelected(int index) {
            if (index == selectionIndex) {
                selectionIndex = -1;
                buildingGhost.gameObject.SetActive(false);
                RevertTileColors();
            } else {
                selectionIndex = index;
                buildingGhost.Setup(SelectedBuilding);
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
            bool t;
            Vector3 mousePos = RaycastHelper.GetMousePositionInScene(out t);

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
            Building b = Instantiate(SelectedBuilding, Tile.GetCentrePoint(tiles), Quaternion.identity, transform);
            foreach (Tile t in tiles)
                t.occupant = b;
            Instantiate(placeEffect).Setup(b);
            OnBuildingPlaced(b, SelectedBuildingData);
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