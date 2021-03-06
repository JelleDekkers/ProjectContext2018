﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace CityView.UI {

    public class BuildingSelectionWidget : MonoBehaviour {

        public static Action<int> OnBuildingSelected;

        [SerializeField] private BuildingSelectionWidgetItem itemPrefab;
        [SerializeField] private GameObject contentWidget;
        [SerializeField] private Transform contentGrid;
        [SerializeField] private BuildingPrefabs buildings;
        [SerializeField] private BuildingType buildingType;
        [SerializeField] private BuildingListSelectionInfoWidget infoWidget;
        [SerializeField] private Construction.BuildModeBase buildMode;
        [SerializeField] private Toggle toggle;

        private void OnEnable() {
            SwitchView.OnSceneViewSwitched += Disable;
            infoWidget.Init();
        }

        private void OnDisable() {
            SwitchView.OnSceneViewSwitched -= Disable;
        }

        private void Disable() {
            gameObject.SetActive(false);
        }

        public void OnToggled() {
            FillGridBuildingData();
            contentWidget.gameObject.SetActive(toggle.isOn);
            buildMode.enabled = toggle.isOn;
        }

        private void FillGridBuildingData() {
            contentGrid.RemoveChildren();
            contentWidget.gameObject.SetActive(true);
            for (int i = 0; i < DataManager.BuildingData.dataArray.Length; i++) {
                BuildingsData data = DataManager.BuildingData.dataArray[i];
                if(data.BuildingType == buildingType) {
                    bool correctClimate = (data.Climate == Climate.None || data.Climate == Player.LocalPlayer.ClimateType);
                    Instantiate(itemPrefab, contentGrid).Init(i, buildings, data, correctClimate, buildMode);
                }
            }
        }
    }
}