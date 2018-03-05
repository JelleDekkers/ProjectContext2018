using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.Construction {

    public class BuildingGhost : MonoBehaviour {

        [SerializeField]
        private float alphaValue = 0.2f;

        private Building ghost;
        private Building building;

        public void Setup(Building prefab) {
            if (ghost != null)
                Destroy(ghost.gameObject);

            building = prefab;
            ghost = Instantiate(prefab, transform.position, Quaternion.identity, transform);
            ghost.name = "Ghost " + ghost.name;
            ghost.enabled = false;
            ghost.Setup();
            ghost.ToggleBuildingEffects(false);
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
            ghost.gameObject.SetActive(false);
        }

        private void OnValidMousePosition(Vector3 centre) {
            if(!ghost.gameObject.activeInHierarchy)
                ghost.gameObject.SetActive(true);

            ghost.transform.position = new Vector3(centre.x, centre.y, centre.z);
        }

        private void MakeTransparent() {
            Renderer[] renderers = ghost.GetComponentsInChildren<Renderer>();
            Color c;

            foreach(Renderer r in renderers) {
                c = r.material.color;
                c.a = alphaValue;
                r.material.color = c;
            }
        }
    }
}