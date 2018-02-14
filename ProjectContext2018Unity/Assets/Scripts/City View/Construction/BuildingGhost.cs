using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.Construction {

    public class BuildingGhost : MonoBehaviour {

        [SerializeField]
        private float alphaValue = 0.2f;

        private Transform ghost;
        private Building building;

        public void Setup(Building prefab) {
            if (ghost != null)
                Destroy(ghost.gameObject);

            building = prefab;
            ghost = Instantiate(prefab, transform.position, Quaternion.identity, transform).transform;
            ghost.name = "Ghost " + ghost.name;

            foreach (Component c in ghost.GetComponents(typeof(Component))) {
                if (c.GetType() != typeof(Transform) && c.GetType() != typeof(MeshFilter) && c.GetType() != typeof(MeshRenderer))
                    Destroy(c);
            }

            MakeTransparent();
        }

        public void UpdatePosition(Tile[,] tilesHoveringOver) {
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

            ghost.transform.position = new Vector3(centre.x, 0, centre.z);
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