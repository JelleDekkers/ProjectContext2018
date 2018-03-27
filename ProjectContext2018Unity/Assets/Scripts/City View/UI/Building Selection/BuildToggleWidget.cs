using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CityView.Construction;

public class BuildToggleWidget : MonoBehaviour {

    [SerializeField] private Toggle toggle;
    [SerializeField] private GameObject buildTab;
    [SerializeField] private BuildMode buildMode;
    [SerializeField] private BuildModeClimateBuildings buildModeClimate;

    public void ToggleBuildTab() {
        buildTab.gameObject.SetActive(toggle.isOn);
        if(!toggle.isOn) {
            buildMode.enabled = false;
            buildModeClimate.enabled = false;
        }
    }
}
