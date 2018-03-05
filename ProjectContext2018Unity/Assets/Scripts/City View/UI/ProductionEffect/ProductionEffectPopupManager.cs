using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.UI {

    public class ProductionEffectPopupManager : MonoBehaviour {

        [SerializeField]
        private ProductionEffectPopup effect;

        private void Start() {
            Building.OnProductionCycleCompleted += InstantiateNewProductionPopup;
            Construction.BuildMode.OnBuildingPlaced += InstantiateNewBuildingCostsPopup;
        }

        private void InstantiateNewProductionPopup(Building building, ProductionCycleResult production) {
            ProductionEffectPopup popup = Instantiate(effect, building.transform.position, effect.transform.rotation, transform);
            popup.transform.localRotation = effect.transform.rotation;
            popup.Init(building, production);
        }

        private void InstantiateNewBuildingCostsPopup(Building building, BuildingsData data) {
            ProductionEffectPopup popup = Instantiate(effect, building.tilesStandingOn[0,0].transform.position, effect.transform.rotation, transform);
            popup.transform.localRotation = effect.transform.rotation;
            popup.Init(building, data);
        }

        private void OnDestroy() {
            Building.OnProductionCycleCompleted -= InstantiateNewProductionPopup;
            Construction.BuildMode.OnBuildingPlaced -= InstantiateNewBuildingCostsPopup;
        }
    }
}