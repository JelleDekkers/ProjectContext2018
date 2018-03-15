using System;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.Construction {

    public class BuildingGhost : MonoBehaviour {

        [SerializeField] private float AlphaValue = 0.8f;
        [SerializeField] private Color unbuildableColor = Color.red;

        private BuildingBase building;
        private int dataID;

        private List<Material> materials;
        private List<Color> startingColors;

        public void Setup(BuildingBase prefab, int dataID) {
            if (building != null)
                Destroy(building.gameObject);

            this.dataID = dataID;
            building = Instantiate(prefab, transform.position, Quaternion.identity, transform);
            building.name = "Ghost " + building.name;
            building.CacheEffects();
            building.enabled = false;
            building.ToggleBuildingEffects(false);

            materials = new List<Material>();
            startingColors = new List<Color>();

            foreach(Renderer rend in building.GetComponentsInChildren<Renderer>()) {
                foreach(Material m in rend.materials) {
                    if (!m.HasProperty("_Color"))
                        continue;

                    materials.Add(m);
                    Color c = m.color;
                    c.a = AlphaValue;
                    m.color = c;
                    startingColors.Add(m.color);
                }
            }
            
            MakeBuildingMeshTransparent();

            AdjustBuildingMeshToUnavailableColor(building.IsBuildable(dataID));
        }

        private void OnEnable() {
            PlayerResources.OnResourceChanged += (x, y) => CheckForIsBuildableState();
            PlayerResources.OnMoneyChanged += (x) => CheckForIsBuildableState();
        }

        private void OnDestroy() {
            PlayerResources.OnResourceChanged -= (x, y) => CheckForIsBuildableState();
            PlayerResources.OnMoneyChanged -= (x) => CheckForIsBuildableState();
        }

        private void CheckForIsBuildableState() {
            Debug.Log(dataID + " " + building.IsBuildable(dataID));
            AdjustBuildingMeshToUnavailableColor(building.IsBuildable(dataID));
        }

        private void MakeBuildingMeshTransparent() {
            foreach(Material m in materials) { 
                StandardShaderUtils.ChangeRenderMode(m, StandardShaderUtils.BlendMode.Transparent);
                Color c = m.color;
                c.a = AlphaValue;
                m.color = c;
            }
        }

        private void AdjustBuildingMeshToUnavailableColor(bool buildable) {
            for(int i = 0; i < materials.Count; i++) {
                if (buildable)
                    materials[i].color = startingColors[i];
                else 
                    materials[i].color = unbuildableColor;
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