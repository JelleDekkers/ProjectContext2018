using System;
using UnityEngine;
using UnityEngine.EventSystems;
using CityView.Terrain;

namespace CityView.Construction {

    public class BuildMode : BuildModeBase {

        private Building SelectedBuilding { get { return DataManager.BuildingPrefabs.GetBuildingPrefab(selectionIndex) as Building; } }
        private BuildingsData SelectedBuildingData { get { return DataManager.BuildingData.dataArray[selectionIndex]; } }

        public static Action<BuildingBase, BuildingsData> OnBuildingPlaced;
        public static Action<bool> OnBuildStateToggled;

        protected override void OnEnable() {
            base.OnEnable();
            UI.BuildingSelectionWidget.OnBuildingSelected += OnBuildingSelected;
        }

        protected override void OnDisable() {
            base.OnDisable();

            UI.BuildingSelectionWidget.OnBuildingSelected -= OnBuildingSelected;

            selectionIndex = -1;
            if(OnBuildStateToggled != null)
                OnBuildStateToggled(false);
        }

        protected override void Update() {
            if (selectionIndex == -1) 
                return;

            RevertTileColors();
            tilesHoveringOver = GetTilesHoveringOver(SelectedBuilding.Size);
            if (tilesHoveringOver != null) {
                HighlightUnbuildableTiles();
                buildingGhost.UpdatePosition(tilesHoveringOver);
            }

            base.Update();
        }

        protected override void OnBuildingSelected(int index) {
            if (index == selectionIndex) {
                selectionIndex = -1;
                buildingGhost.gameObject.SetActive(false);
                RevertTileColors();
                OnBuildStateToggled(false);
                enabled = false;
            } else {
                selectionIndex = index;
                buildingGhost.Setup(SelectedBuilding, SelectedBuildingData.ID);
                buildingGhost.gameObject.SetActive(true);
                OnBuildStateToggled(true);
            }
        }

        protected override void OnMouseClick() {
            if (tilesHoveringOver == null || EventSystem.current.IsPointerOverGameObject() || !Building.IsBuildingBuildable(SelectedBuildingData.ID))
                return;

            if(CanBePlacedAtTiles(tilesHoveringOver))
                Build(tilesHoveringOver);
        }

        protected override bool TileIsBuildable(Tile tile) {
            return tile.Occupant == null && city.Terrain.GetWaterBlock(tile.Coordinates) == null;
        }

        protected override void Build(Tile[,] tiles) {
            Building building = Instantiate(SelectedBuilding, Tile.GetCentrePoint(tiles), SelectedBuilding.transform.rotation, buildingsParent);
            AverageOutTerrain(tiles);

            foreach (Tile tile in tiles)
                tile.SetOccupant(building);

            Instantiate(placeEffectPrefab).Setup(building);

            PlayerResources.Instance.RemoveMoney(SelectedBuildingData.Moneycost);
            for (int i = 0; i < SelectedBuildingData.Resourcecost.Length; i++)
                PlayerResources.Instance.RemoveResource(SelectedBuildingData.Resourcecost[i], SelectedBuildingData.Resourcecostamount[i]);

            building.enabled = true;
            building.Init(SelectedBuildingData, tiles);
            building.AddOutline(outlinePrefab);

            OnBuildingPlaced(building, SelectedBuildingData);

            buildingGhost.Setup(SelectedBuilding, SelectedBuildingData.ID);
        }

        public void BuildAtStart(Building building, BuildingsData data, IntVector2 coordinates) {
            Tile[,] tiles = GetNeededTiles(coordinates, building.Size);
            AverageOutTerrain(tiles);

            foreach (Tile tile in tiles)
                tile.SetOccupant(building);

            building.transform.position = Tile.GetCentrePoint(tiles);
            building.enabled = true;
            building.Init(data, tiles);
            building.AddOutline(outlinePrefab);

            OnBuildingPlaced(building, data);
        }

        private void OnDestroy() {
            UI.BuildingSelectionWidget.OnBuildingSelected -= OnBuildingSelected;
        }
    }
}