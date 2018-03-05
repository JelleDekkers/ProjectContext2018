using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.Terrain {

    public class DamBuilder : MonoBehaviour {

        public float heightIncrease = 1;

        private Ray ray;
        private RaycastHit hit;

        public void Update() {
            if (Input.GetMouseButtonDown(0))
                BuildDam();
            else if (Input.GetMouseButtonDown(1))
                LowerDam();
        }

        private void BuildDam() {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                TerrainBlock block = hit.collider.gameObject.transform.parent.GetComponent<TerrainBlock>();
                if (block == null)
                    return;
                block.AddHeight(heightIncrease);
            }
        }

        private void LowerDam() {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) {
                if (!hit.collider.gameObject.transform.parent.GetComponent<TerrainBlock>())
                    return;

                TerrainBlock block = hit.collider.transform.parent.gameObject.GetComponent<TerrainBlock>();
                block.AddHeight(-heightIncrease);
            }
        }
    }
}