using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.UI {

    public class ProductionEffectPopupManager : MonoBehaviour {

        [SerializeField]
        private ProductionEffectPopup effect;

        private void Start() {
            Building.OnProductionCycleCompleted += InstantiateNewPopup;
        }

        private void InstantiateNewPopup(Building building, ProductionCycleResult production) {
            ProductionEffectPopup popup = Instantiate(effect, building.transform.position, effect.transform.rotation, transform);
            popup.transform.localRotation = effect.transform.rotation;
            popup.Init(building, production);
        }
    }
}