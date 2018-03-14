using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.UI {

    public class ProductionStoppedWidget : MonoBehaviour {

        private Building building;

        public void Init(Building building) {
            building.OnProductionResumed += DestroySelf;
            building.OnDestroyed += DestroySelf;
        }
      
        private void DestroySelf() {
            if (building != null) {
                building.OnProductionResumed -= DestroySelf;
                building.OnDestroyed -= DestroySelf;
            }
            Destroy(gameObject);
        }
    }
}