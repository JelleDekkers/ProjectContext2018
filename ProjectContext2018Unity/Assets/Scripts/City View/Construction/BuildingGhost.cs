using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.Construction {

    public class BuildingGhost : MonoBehaviour {

        [SerializeField] private float AlphaValue = 0.8f;
        [SerializeField] private Color unbuildableColor = Color.red;

        private BuildingBase building;

        private Transform Mesh { get { return building.transform.GetChild(1); } }
        private List<Material> meshMaterials;

        public void Setup(BuildingBase prefab) {
            if (building != null)
                Destroy(building.gameObject);

            building = Instantiate(prefab, transform.position, Quaternion.identity, transform);
            building.name = "Ghost " + building.name;
            building.CacheEffects();
            building.enabled = false;
            building.ToggleBuildingEffects(false);

            Renderer renderer = Mesh.GetComponent<Renderer>();
            meshMaterials = new List<Material>();
            foreach (Material m in renderer.materials) {
                meshMaterials.Add(m);
            }
            MakeBuildingMeshTransparent();
            
        }

        private void MakeBuildingMeshTransparent() {
            foreach (Material m in meshMaterials) {
                StandardShaderUtils.ChangeRenderMode(m, StandardShaderUtils.BlendMode.Transparent);
                Color c = m.color;
                c.a = AlphaValue;
                m.color = c;
            }
        }

        private void AdjustBuildingMeshToUnavailableColor() {
            foreach (Material m in meshMaterials) {
                m.color = unbuildableColor;
            }
        }

        public void UpdatePosition(Tile[,] tilesHoveringOver) {
            if(tilesHoveringOver == null) {
                OnInValidMousePosition();
                return;
            }

            foreach (Tile t in tilesHoveringOver) {
                if (t == null) {
                    OnInValidMousePosition();
                    return;
                }
            }
            Vector3 centre = Tile.GetCentrePoint(tilesHoveringOver);
            OnValidMousePosition(centre);
        }

        private void OnInValidMousePosition() {
            building.gameObject.SetActive(false);
        }

        private void OnValidMousePosition(Vector3 centre) {
            if(!building.gameObject.activeInHierarchy)
                building.gameObject.SetActive(true);

            building.transform.position = new Vector3(centre.x, centre.y, centre.z);
        }
    }
}