using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.Construction {

    public class Builder : MonoBehaviour {

        [SerializeField] private PlaceMode placeMode;
        [SerializeField] private DestroyMode destroyMode;

        private BuildMode currentBuildMode;

        private void Update() {
            if (currentBuildMode != null)
                currentBuildMode.UpdateMode();
        }

        private void SetBuildMode(BuildMode mode) {
            if (currentBuildMode != null) {
                currentBuildMode.OnEnd();
            } else {
                currentBuildMode = mode;
                currentBuildMode.OnStart(this);
                return;
            }

            // toggle:
            if (currentBuildMode.GetType() != mode.GetType()) {
                currentBuildMode = mode;
                currentBuildMode.OnStart(this);
            } else {
                currentBuildMode = null;
            }
        }

        private void OnGUI() {
            if (GUI.Button(new Rect(10, 10, 150, 20), "Build Mode"))
                SetBuildMode(placeMode);
            if (GUI.Button(new Rect(10, 30, 150, 20), "Destroy Mode"))
                SetBuildMode(destroyMode);
        }


        //private void DestroyBuilding(Tile hoveringOver) {
        //    Building b = tilesHoveringOver[0, 0].occupant;
        //    Player.Instance.RemoveBuilding(b);
        //    Destroy(b.gameObject);
        //}

        //private void AdjustTileColors() {
        //    foreach (Tile t in tilesHoveringOver) {
        //        if (t == null)
        //            continue;
        //        if (t.occupant != null)
        //            t.SetColorToOccupied();
        //        else
        //            t.SetColorToOpen();
        //    }
        //}

        //private void RevertTileColorsToBase() {
        //    foreach (Tile t in tilesHoveringOver) {
        //        if (t == null)
        //            continue;
        //        t.SetColorToBase();
        //    }
        //}

        //private void PlaceBuilding(Building buildingPrefab, Tile[,] tiles, bool fromStart) {
        //    Building building = Instantiate(buildingPrefab, tiles[0, 0].transform.position, Quaternion.identity);
        //    building.transform.SetParent(transform);

        //    foreach (Tile t in tiles)
        //        t.occupant = building;

        //    Vector3 halfSize = Vector3.zero;
        //    if (building.xSize > 1)
        //        halfSize.x = (int)(building.xSize / 2) - Tile.SIZE.x / 2;
        //    if (building.zSize > 1)
        //        halfSize.z = (int)(building.zSize / 2) - Tile.SIZE.z / 2;
        //    building.transform.position += halfSize;

        //    Player.Instance.AddBuilding(building);

        //    if (!fromStart) {
        //        Player.Instance.RemoveResources(building.cost);
        //        effectHandler.Activate(building.transform.position);
        //        GameCam.Instance.Shake();
        //    }
        //}

        //private Tile[,] GetTilesAt(Vector3 position, IntVector2 buildingSize) {
        //    IntVector2 coordinate = new IntVector2((int)RoundDownToGridCoordinate(position).x, (int)RoundDownToGridCoordinate(position).y);
        //    Tile[,] tiles = new Tile[buildingSize.x, buildingSize.z];
        //    // get all tiles necessary for building:
        //    for (int x = 0; x < buildingSize.x; x++) {
        //        for (int z = 0; z < buildingSize.z; z++) {
        //            if (Company.Instance.grid.IsInsideGrid(coordinate.x + x, coordinate.z + z)) {
        //                Tile t = Company.Instance.grid.Grid[coordinate.x + x, coordinate.z + z];
        //                tiles[x, z] = t;
        //            }
        //        }
        //    }

        //    return tiles;
        //}

        //private bool CanBePlaced(Tile[,] tilesNeeded) {
        //    if (tilesNeeded == null)
        //        return false;

        //    for (int x = 0; x < tilesNeeded.GetLength(0); x++) {
        //        for (int z = 0; z < tilesNeeded.GetLength(1); z++) {
        //            if (tilesNeeded[x, z] == null || tilesNeeded[x, z].occupant != null)
        //                return false;
        //        }
        //    }
        //    return true;
        //}

        //private Vector2 RoundDownToGridCoordinate(Vector3 point) {
        //    return new Vector2(Mathf.Round(point.x), Mathf.Round(point.z));
        //}
    }
}