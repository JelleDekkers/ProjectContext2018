using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.Construction {

    public class BuildingGhost : MonoBehaviour {

        [SerializeField]
        private float alphaValue = 0.2f;

        private BuildingBase building;

        public void Setup(BuildingBase prefab) {
            if (building != null)
                Destroy(building.gameObject);

            building = Instantiate(prefab, transform.position, Quaternion.identity, transform);
            building.name = "Ghost " + building.name;
            building.CacheEffects();
            building.enabled = false;
            building.ToggleBuildingEffects(false);
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

        private void MakeTransparent() {
            Renderer[] renderers = building.GetComponentsInChildren<Renderer>();
            Color c;

            foreach(Renderer r in renderers) {
                c = r.material.color;
                c.a = alphaValue;
                r.material.color = c;
            }
        }
    }
}