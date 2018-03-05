﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;
using CityView.Terrain;

namespace CityView.Construction {

    public class BuildMode : MonoBehaviour {

        [SerializeField] private City city;
        [SerializeField] private BuildingGhost buildingGhost;
        [SerializeField] private BuildingPlacementEffect placeEffectPrefab;
        [SerializeField] private Transform buildingsParent;
        [SerializeField] private LayerMask tileLayer;

        private Tile[,] tilesHoveringOver;
        private bool isHittingGrid;

        private int selectionIndex = -1;
        private Building SelectedBuilding { get { return DataManager.BuildingPrefabs.GetBuilding(selectionIndex); } }
        private BuildingsData SelectedBuildingData { get { return DataManager.BuildingData.dataArray[selectionIndex]; } }

        public static Action<Building, BuildingsData> OnBuildingPlaced;
        public static Action<bool> OnBuildStateToggled;

        private void Start() {
            UI.BuildingSelectionWidget.OnBuildingSelected += OnBuildingSelected;
        }

        private void OnDisable() {
            if(buildingGhost != null)
                buildingGhost.gameObject.SetActive(false);
            RevertTileColors();
            selectionIndex = -1;
            if(OnBuildStateToggled != null)
                OnBuildStateToggled(false);
        }

        private void Update() {
            if (selectionIndex == -1) 
                return;

            RevertTileColors();
            tilesHoveringOver = GetTilesHoveringOver(SelectedBuilding.Size);
            if (tilesHoveringOver != null) {
                HighlightUnbuildableTiles();
                buildingGhost.UpdatePosition(tilesHoveringOver);
            }

            if (Input.GetMouseButtonDown(0))
                OnMouseClick();
        }

        private void OnBuildingSelected(int index) {
            if (index == selectionIndex) {
                selectionIndex = -1;
                buildingGhost.gameObject.SetActive(false);
                RevertTileColors();
                OnBuildStateToggled(false);
            } else {
                selectionIndex = index;
                buildingGhost.Setup(SelectedBuilding);
                buildingGhost.gameObject.SetActive(true);
                OnBuildStateToggled(true);
            }
        }

        private void HighlightUnbuildableTiles() {
            foreach(Tile t in tilesHoveringOver) {
                if (t == null)
                    continue;
                else if(!TileIsBuildable(t))
                    t.ShowTile();
            }
        }

        private void RevertTileColors() {
            if (tilesHoveringOver == null)
                return;

            foreach (Tile t in tilesHoveringOver) {
                if (t != null)
                    t.HideTile();
            }
        }

        private void OnMouseClick() {
            if (tilesHoveringOver == null || EventSystem.current.IsPointerOverGameObject())
                return;

            if(CanBePlacedAtTiles(tilesHoveringOver))
                Build(tilesHoveringOver);
        }

        private bool CanBePlacedAtTiles(Tile[,] tiles) {
            foreach(Tile t in tiles) {
                if (t == null || !TileIsBuildable(t))
                    return false;
            }
            return true;
        }

        private bool TileIsBuildable(Tile tile) {
            return tile.occupant == null && city.Terrain.GetWaterBlock(tile.Coordinates) == null;
        }

        private void Build(Tile[,] tiles) {
            Building b = Instantiate(SelectedBuilding, Tile.GetCentrePoint(tiles), Quaternion.identity, buildingsParent);
            foreach (Tile t in tiles)
                t.occupant = b;
            Instantiate(placeEffectPrefab).Setup(b);
            OnBuildingPlaced(b, SelectedBuildingData);

            PlayerResources.Instance.RemoveMoney(SelectedBuildingData.Moneycost);
            for (int i = 0; i < SelectedBuildingData.Resourcecost.Length; i++)
                PlayerResources.Instance.RemoveResource(SelectedBuildingData.Resourcecost[i], SelectedBuildingData.Resourcecostamount[i]);

            b.enabled = true;
            b.Init(SelectedBuildingData);

            if (!Building.IsBuildable(selectionIndex))
                selectionIndex = -1;
        }

        private Tile[,] GetTilesHoveringOver(IntVector2 buildingSize) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.MaxValue, tileLayer)) {
                Tile tileHit = hit.collider.gameObject.GetComponent<Tile>();
                if (tileHit == null)
                    return null;

                Tile[,] tiles = new Tile[buildingSize.x, buildingSize.z];
                for (int x = 0; x < buildingSize.x; x++) {
                    for (int z = 0; z < buildingSize.z; z++) {
                        IntVector2 coordinates = new IntVector2(tileHit.Coordinates.x + x, tileHit.Coordinates.z + z);
                        if (City.Instance.TilesGrid.IsInsideGrid(coordinates)) {
                            Tile t = City.Instance.TilesGrid.GetTile(coordinates);
                            tiles[x, z] = t;
                        }
                    }
                }
                return tiles;
            }
            return null;
        }

        //private Tile[,] GetTilesAtPosition(Vector3 position, IntVector2 buildingSize) {
        //    IntVector2 coordinates = IntVector2.ConvertToCoordinates(position);
        //    Tile[,] tiles = new Tile[buildingSize.x, buildingSize.z];
        //    for (int x = 0; x < buildingSize.x; x++) {
        //        for (int z = 0; z < buildingSize.z; z++) {
        //            if (City.Instance.TilesGrid.IsInsideGrid(new IntVector2(coordinates.x + x, coordinates.z + z))) {
        //                Tile t = City.Instance.TilesGrid.GetTile(coordinates);
        //                tiles[x, z] = t;
        //            }
        //        }
        //    }

        //    return tiles;
        //}

        private void OnDestroy() {
            UI.BuildingSelectionWidget.OnBuildingSelected -= OnBuildingSelected;
        }
    }
}