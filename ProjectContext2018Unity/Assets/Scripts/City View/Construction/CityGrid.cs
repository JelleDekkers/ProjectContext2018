using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.Construction {

    public class CityGrid : MonoBehaviour {

        public Tile[,] Grid { get; private set; }

        [SerializeField] private IntVector2 size;
        [SerializeField] private Tile tilePrefab;
        [SerializeField] private Material lineMaterial;
        [SerializeField] private bool showGridLines;

        private void Awake() {
            Fill();
        }

        public bool IsInsideGrid(IntVector2 coordinates) {
            return (coordinates.x >= 0 && coordinates.x < size.x && coordinates.z >= 0 && coordinates.z < size.z);
        }

        private void Fill() {
            Grid = new Tile[size.x, size.z];
            for (int x = 0; x < size.x; x++) {
                for (int z = 0; z < size.z; z++) {
                    Tile t = Instantiate(tilePrefab, new Vector3(x, 0, z), tilePrefab.transform.rotation, transform);
                    t.Coordinates = new IntVector2(x, z);
                    t.name += "(" + x + "," + z + ")";
                    Grid[x, z] = t;
                    // for checker pattern:
                    //if ((x + z) % 2 == 0)
                    //    t.SetColor();
                }
            }
        }

        public void ToggleGridLines() {
            showGridLines = !showGridLines;
        }

        private void OnRenderObject() {
            if (!showGridLines)
                return;

            // horizontal:
            Vector3 start;
            Vector3 end;
            for (int x = 0; x < size.x + 1; x++) {
                start = new Vector3(x * Tile.SIZE.x - Tile.SIZE.x / 2, 0, -Tile.SIZE.z / 2);
                end = new Vector3(x * Tile.SIZE.x - Tile.SIZE.x / 2, 0, size.z * Tile.SIZE.z - Tile.SIZE.z / 2);
                GL.PushMatrix();
                lineMaterial.SetPass(0);
                GL.Begin(GL.LINES);
                GL.Vertex3(start.x, start.y, start.z);
                GL.Vertex3(end.x, end.y, end.z);
                GL.End();
                GL.PopMatrix();
            }

            // vertical:
            for (int z = 0; z < size.z + 1; z++) {
                start = new Vector3(-Tile.SIZE.x / 2, 0, z * Tile.SIZE.z - Tile.SIZE.z / 2);
                end = new Vector3(size.x * Tile.SIZE.x - Tile.SIZE.x / 2, 0, z * Tile.SIZE.z - Tile.SIZE.z / 2);
                GL.PushMatrix();
                lineMaterial.SetPass(0);
                GL.Begin(GL.LINES);
                GL.Vertex3(start.x, start.y, start.z);
                GL.Vertex3(end.x, end.y, end.z);
                GL.End();
                GL.PopMatrix();
            }
        }
    }
}