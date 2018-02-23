using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.UI {

    public class ProductionEffectPopupManager : MonoBehaviour {

        [SerializeField]
        private ProductionEffectPopup effect;

        private void Start() {
            Building.OnProductionCycleCompleted += InstantiateNewPopup;
            //Construction.PlaceMode.OnBuildingPlaced += InstantiateNewPopup;
        }

        private void InstantiateNewPopup(Building building, ProductionCycleResult production) {
            ProductionEffectPopup popup = Instantiate(effect, building.transform.position, effect.transform.rotation, transform);
            popup.transform.localRotation = effect.transform.rotation;
            popup.Init(building, production);
        }

        private void InstantiateNewPopup(Building building, BuildingsData data) {
            ProductionEffectPopup popup = Instantiate(effect, building.transform.position, effect.transform.rotation, transform);
            popup.transform.localRotation = effect.transform.rotation;
            popup.Init(building, data);
        }

        private void OnDestroy() {
            Building.OnProductionCycleCompleted -= InstantiateNewPopup;
            Construction.BuildMode.OnBuildingPlaced -= InstantiateNewPopup;
        }
    }
}