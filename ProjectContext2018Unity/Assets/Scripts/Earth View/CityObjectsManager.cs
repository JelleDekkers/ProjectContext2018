using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EarthView {

    public class CityObjectsManager : MonoBehaviour {

        public CityObject[] cityObjects;

        private void Awake() {
            cityObjects = GetComponentsInChildren<CityObject>(true);
            
        }
    }
}