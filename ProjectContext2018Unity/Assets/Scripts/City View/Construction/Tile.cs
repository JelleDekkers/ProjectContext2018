using System;
using UnityEngine;
using CityView.Terrain;

namespace CityView.Construction {

    public class Tile : MonoBehaviour {

        public static readonly Vector3 SIZE = new Vector3(1, 0.1f, 1);

        public BuildingBase Occupant { get; private set; }
        public IntVector2 Coordinates { get; private set; }
        public bool IsUnderWater { get; private set; }

        private new Renderer renderer;
        public Action<bool> OnWaterStateChanged;

        public void Init(IntVector2 coordinates) {
            Coordinates = coordinates;
            renderer = transform.GetChild(0).GetComponent<Renderer>();
            HideTile();
        }

        public void SetOccupant(BuildingBase building) {
            Occupant = building;
        }

        public void OnWaterLevelChanged(bool water) {
            IsUnderWater = water;

            if (OnWaterStateChanged != null)
                OnWaterStateChanged(water);
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