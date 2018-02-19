using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace CompanyView.UI.Testing {
    public class TestShowBuilding : MonoBehaviour {
        private Text text;

        public void Start() {
            text = gameObject.GetComponent<Text>();
        }

        public void Update() {
            /*if (BuildingSelector.SelectedBuilding != null) {

                text.text = BuildingSelector.SelectedBuilding.name;
            }
            else {
                text.text = "No building selected";
            }*/
        }
    }
}
