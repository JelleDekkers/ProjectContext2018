using System;
using UnityEngine;
using UnityEngine.EventSystems;
using CityView.Terrain;

namespace CityView.Construction {

    public abstract class BuildModeBase : MonoBehaviour {

        [SerializeField] protected City city;
        [SerializeField] protected BuildingGhost buildingGhost;
        [SerializeField] protected BuildingPlacementEffect placeEffectPrefab;
        [SerializeField] protected Transform buildingsParent;
        [SerializeField] protected LayerMask tileLayer;
        [SerializeField] protected SpriteRenderer outlinePrefab;
        //[SerializeField] protected float maxTileHeightDistance;

        protected int selectionIndex = -1;
        protected Tile[,] tilesHoveringOver;
        protected bool isHittingGrid;

        protected virtual void OnDisable() {
            if(buildingGhost != null)
                buildingGhost.gameObject.SetActive(false);
            RevertTileColors();
        }

        protected virtual void Update() {
            if (Input.GetMouseButtonDown(0))
                OnMouseClick();
        }

        protected void HighlightUnbuildableTiles() {
            foreach (Tile t in tilesHoveringOver) {
                if (t == null)
                    continue;
                else if (!TileIsBuildable(t))
                    t.ShowTile();
            }
        }

        protected void RevertTileColors() {
            if (tilesHoveringOver == null)
                return;

            foreach (Tile t in tilesHoveringOver) {
                if (t != null)
                    t.HideTile();
            }
        }

        protected abstract void OnMouseClick();
        protected abstract bool TileIsBuildable(Tile tile);
        protected abstract void Build(Tile[,] tiles);

        protected virtual bool CanBePlacedAtTiles(Tile[,] tiles) {
            foreach (Tile t in tiles) {
                if (t == null || !TileIsBuildable(t))
                    return false;
            }
            return true;
        }

        protected void AverageOutTerrain(Tile[,] tiles) {
            TerrainBlock[] blocks = GetTerrainHoveringOver(tiles);
            float avgHeight = GetTerrainAverageHeight(blocks);

            foreach(TerrainBlock block in blocks) 
                block.SetHeight(avgHeight);
        }

        protected float GetTerrainAverageHeight(TerrainBlock[] terrainBlocks) {
            float totalheight = 0;
            foreach (TerrainBlock block in terrainBlocks) {
                totalheight += block.Height;
            }
            return totalheight / terrainBlocks.Length;
        }

        protected TerrainBlock[] GetTerrainHoveringOver(Tile[,] tiles) {
            TerrainBlock[] blocks = new TerrainBlock[tiles.Length];
            int index = 0;
            foreach (Tile tile in tiles) {
                if (tile == null)
                    continue;
                blocks[index] = City.Instance.Terrain.GetTerrainBlock(tile.Coordinates);
                index++;
            }
            return blocks;
        }

        protected Tile[,] GetTilesHoveringOver(IntVector2 buildingSize) {
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
    }
}