using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.Construction {

    public class Tile : MonoBehaviour {

        public static readonly Vector3 SIZE = new Vector3(1, 0.1f, 1);

        public Building occupant;
        public IntVector2 Coordinates { get; set; }

        [SerializeField]
        private Color unBuildableColor;

        private Color baseColor;
        private Renderer rend;

        private void Start() {
            rend = GetComponent<Renderer>();
            baseColor = rend.material.color;
        }

        public void SetColorToUnbuildable() {
            rend.material.color = unBuildableColor;
        }

        public void ResetColor() {
            rend.material.color = baseColor;
        }
    }
}