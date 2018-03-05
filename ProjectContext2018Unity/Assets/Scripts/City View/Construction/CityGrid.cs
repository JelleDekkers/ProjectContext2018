using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CityView.Terrain;

namespace CityView.Construction {

    public class CityGrid : MonoBehaviour {

        public Tile[,] Grid { get; private set; }
        public IntVector2 Size { get; private set; }

        [SerializeField] private Tile tilePrefab;
        [SerializeField] private Material lineMaterial;
        [SerializeField] private bool showGridLines;

        private void Awake() {
            Size = City.Instance.Terrain.MapSize;
            Generate(City.Instance.Terrain);
        }

        public bool IsInsideGrid(IntVector2 coordinates) {
            return (coordinates.x >= 0 && coordinates.x < Size.x && coordinates.z >= 0 && coordinates.z < Size.z);
        }

        public Tile GetTile(IntVector2 coordinates) {
            return Grid[coordinates.x, coordinates.z];
        }

        private void Generate(GameTerrain terrain) {
            Grid = new Tile[Size.x, Size.z];
            TerrainBlock block;
            for (int x = 0; x < Size.x; x++) {
                for (int z = 0; z < Size.z; z++) {
                    block = terrain.GetTerrainBlock(x, z);
                    Tile t = Instantiate(tilePrefab);// new Vector3(x + Tile.SIZE.x / 2, 0, z + Tile.SIZE.z / 2), tilePrefab.transform.rotation, transform);
                    t.transform.SetParent(block.transform.GetChild(0));
                    t.transform.localPosition = new Vector3(0, 0.51f, 0);
                    t.Init(new IntVector2(x, z));
                    t.name += "(" + x + "," + z + ")";
                    Grid[x, z] = t;
                }
            }
        }
    }
}