using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.Terrain {

    public class DamBuilder : MonoBehaviour {

        public float heightIncrease = 1;

        private Ray ray;
        private RaycastHit hit;
        public LayerMask layerMask;

        private void Start() {
            Construction.BuildMode.OnBuildStateToggled += (bool toggle) => {
                try { enabled = !toggle; }
                catch(System.Exception e) { }
            };
        }

        public void Update() {
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                return;

            if (Input.GetMouseButtonDown(0))
                BuildDam();
            else if (Input.GetMouseButtonDown(1))
                LowerDam();
        }

        private void BuildDam() {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.MaxValue, layerMask)) {
                TerrainBlock block = hit.collider.gameObject.transform.parent.GetComponent<TerrainBlock>();
                if (block == null)
                    return;
                block.AddHeight(heightIncrease);
            }
        }

        private void LowerDam() {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, float.MaxValue, layerMask)) {
                if (!hit.collider.gameObject.transform.parent.GetComponent<TerrainBlock>())
                    return;

                TerrainBlock block = hit.collider.transform.parent.gameObject.GetComponent<TerrainBlock>();
                block.AddHeight(-heightIncrease);
            }
        }
    }
}