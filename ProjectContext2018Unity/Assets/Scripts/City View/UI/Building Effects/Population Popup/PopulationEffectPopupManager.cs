using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.UI {

    public class PopulationEffectPopupManager : MonoBehaviour {

        [SerializeField]
        private PopulationEffectPopup popupPrefab;

        private void Start() {
            House.OnNewInhabitant += InstantiateNewPopup;
            House.OnHousePaused += InstantiateNewPausedPopup;
        }

        private void InstantiateNewPopup(Building house, int amount) {
            PopulationEffectPopup popup = Instantiate(popupPrefab, house.tilesStandingOn[0, 0].transform.position, popupPrefab.transform.rotation, transform);
            popup.transform.localRotation = popup.transform.rotation;
            popup.Init(house, amount);
        }

        private void InstantiateNewPausedPopup(Building house, int amount) {
            InstantiateNewPopup(house, -amount);
        }

        private void OnDestroy() {
            House.OnNewInhabitant -= InstantiateNewPopup;
            House.OnHousePaused -= InstantiateNewPausedPopup;
        }
    }
}