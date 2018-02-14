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

        private void Update() {
            FollowMouse();
        }

        public void FollowMouse() {
            bool isHittingGrid;
            Vector3 mousePos = RaycastHelper.GetMousePositionInScene(out isHittingGrid);
            Vector3 offset = Vector3.zero;
            offset.x = (float)building.Size.x / 2 - Tile.SIZE.x / 2;
            offset.z = (float)building.Size.z / 2 - Tile.SIZE.z / 2;
            ghost.transform.position = new Vector3(Mathf.Round(mousePos.x), 0, Mathf.Round(mousePos.z));
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