using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CityView.Construction;

namespace CityView {
    public abstract class BuildingBase : MonoBehaviour {

        protected IntVector2? size;
        public IntVector2 Size {
            get {
                if (!size.HasValue)
                    size = CalculateTileSize();
                return size.Value;
            }
        }

        public Tile[,] tilesStandingOn;

        private Renderer floorObjectRenderer;
        private Renderer FloorObjectRenderer {
            get {
                if (floorObjectRenderer == null)
                    floorObjectRenderer = transform.GetChild(0).GetComponent<Renderer>();
                return floorObjectRenderer;
            }
        }

        protected SpriteRenderer outline;

        public abstract void Init(System.Object data, Tile[,] tilesStandingOn);
        public abstract void CacheEffects();
        public abstract void ToggleBuildingEffects(bool toggle);

        public virtual void OnDemolishStart() { }

        public IntVector2 CalculateTileSize() {
            IntVector2 calcSize = IntVector2.Zero;
            calcSize.x = (int)Mathf.Round(FloorObjectRenderer.bounds.size.x);
            calcSize.z = (int)Mathf.Round(FloorObjectRenderer.bounds.size.z);
            return calcSize;
        }

        public abstract bool IsBuildable(int dataId);

        public void AddOutline(SpriteRenderer sprite) {
            outline = Instantiate(sprite, transform);
            outline.transform.position = transform.position;
            outline.size = new Vector2(Size.x + 0.1f, Size.z + 0.1f);
            outline.enabled = false;
        }

        public virtual void OnHoverEnter(float outlineWidth) {
            //FloorObjectRenderer.material.SetFloat("_Outline", outlineWidth);
            outline.enabled = true;
        }

        public virtual void OnHoverExit() {
            //FloorObjectRenderer.material.SetFloat("_Outline", 0);
            outline.enabled = false;
        }
    }
}