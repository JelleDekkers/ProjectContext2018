using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CityView.Terrain;

namespace CityView.Construction {

    public class Tile : MonoBehaviour {

        public static readonly Vector3 SIZE = new Vector3(1, 0.1f, 1);

        public Building occupant;
        public IntVector2 Coordinates { get; private set; }

        private new Renderer renderer;
        
        public void Init(IntVector2 coordinates) {
            Coordinates = coordinates;
            renderer = GetComponent<Renderer>();
            HideTile();
        }

        public void ShowTile() {
            renderer.enabled = true;
        }

        public void HideTile() {
            renderer.enabled = false;
        }

        public static Vector3 GetCentrePoint(Tile[,] tiles) {
            Vector3 centre = Vector3.zero;
            foreach (Tile t in tiles) {
                centre += t.transform.position;
            }
            return centre /= tiles.Length;
        }
    }
}