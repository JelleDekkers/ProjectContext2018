using System;
using UnityEngine;

namespace CityView.Terrain {

    [Serializable, SelectionBase]
    public class TerrainBlock : MonoBehaviour {

        public Action<float> OnHeightChange;

        [SerializeField] private float height, extraHeight;
        public float TotalHeight { get { return height + extraHeight; } }
        public float Height { get { return height; } }

        [SerializeField] private IntVector2 coordinates;
        public IntVector2 Coordinates { get { return coordinates; } }

        private new Renderer renderer;

        public void Init(IntVector2 coordinates, Color c, float height) {
            this.coordinates = coordinates;
            renderer = transform.GetChild(0).GetComponent<Renderer>();
            SetHeight(height);
            SetColor(c);
        }

        public void SetHeight(float amount) {
            height = amount;
            AdjustScaleToHeight();

            if (OnHeightChange != null)
                OnHeightChange(TotalHeight);
        }

        private void AdjustScaleToHeight() {
            if (height <= 0)
                return;

            transform.localScale = new Vector3(transform.localScale.x, height, transform.localScale.z);
        }

        public void SetExtraHeight(float amount) {
            extraHeight = amount;

            if (OnHeightChange != null)
                OnHeightChange(TotalHeight);
        }

        private void SetColor(Color c) {
            Material mat = new Material(renderer.sharedMaterial);
            mat.color = c;
            renderer.sharedMaterial = mat;
        }
    }
}