using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {

    public class PlayerOffersWidget : MonoBehaviour {

        [SerializeField] private PlayerOffersWidgetItem itemPrefab;
        [SerializeField] private Transform grid;

        private void OnEnable() {
            FillItemGrid();
        }

        private void FillItemGrid() {
            grid.RemoveChildren();
            // start at 1 to skip Energy
            for(int i = 1; i < Player.LocalPlayer.resourcesAmountForTrade.Count; i++) 
                InstantiateNewItem(i);   
        }

        private void InstantiateNewItem(int id) {
            Instantiate(itemPrefab, grid).Init(id);
        }
    }
}