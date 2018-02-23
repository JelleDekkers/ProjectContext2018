using System;
using UnityEngine;

namespace CityView.UI {

    public class BuildingSelectionWidget : MonoBehaviour {

        public static Action<int> OnBuildingSelected;

        [SerializeField]
        private BuildingSelectionWidgetItem itemPrefab;
        [SerializeField]
        private Transform contentParent;

        private bool cached;

        private void Start() {
            Fill();
        }

        private void Fill() {
            for(int i = 0; i < DataManager.BuildingData.dataArray.Length; i++) 
                Instantiate(itemPrefab, contentParent).Init(i);
            cached = true;
        }
    }
}