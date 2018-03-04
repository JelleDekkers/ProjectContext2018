using System;
using UnityEngine;

namespace CityView.Terrain {

    [Serializable, SelectionBase]
    public class TerrainBlock : MonoBehaviour {

        public Action<float> OnHeightChange;

        [SerializeField] private float totalHeight;
        public float TotalHeight { get { return totalHeight; } }

        [SerializeField] private IntVector2 coordinates;
        public IntVector2 Coordinates { get { return coordinates; } }

        private new Renderer renderer;

        public void Init(IntVector2 coordinates, Color c, float height) {
            this.coordinates = coordinates;
            renderer = transform.GetChild(0).GetComponent<Renderer>();
            SetHeight(height);
            SetColor(c);
        }

        public void ChangeHeight(float amount) {
            SetHeight(totalHeight + amount);
        }

        private void SetHeight(float height) {
            if (height <= 0)
                return;

            transform.localScale = new Vector3(transform.localScale.x, height, transform.localScale.z);
            totalHeight = height;

            if (OnHeightChange != null)
                OnHeightChange(totalHeight);
        }

        private void SetColor(Color c) {
            Material mat = new Material(renderer.sharedMaterial);
            mat.color = c;
            renderer.sharedMaterial = mat;
        }
    }
}