using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EarthView {

    public class CityObjectsManager : MonoBehaviour {

        public CityObject[] cityObjects;

        private void Awake() {
            cityObjects = GetComponentsInChildren<CityObject>(true);
            
            for(int i = 0; i < PlayerList.Instance.Players.Count; i++) {
                cityObjects[i].SetObjectActive(true);
                cityObjects[i].Init(PlayerList.Instance.Players[i]);
            }
        }
    }
}