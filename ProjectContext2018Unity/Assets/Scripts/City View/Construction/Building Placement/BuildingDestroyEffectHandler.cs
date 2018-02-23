using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.Construction {

    public class BuildingDestroyEffectHandler : MonoBehaviour {

        [SerializeField] private BuildingDestroyEffect destroyEffectPrefab;

        private void Start() {
            Building.OnDemolishInitiated += InstantiateDemolishEffect;
        }

        private void InstantiateDemolishEffect(Building building) {
            BuildingDestroyEffect effect = Instantiate(destroyEffectPrefab);
            effect.transform.position = building.transform.position;
            effect.Init(building);
        }
    }
}