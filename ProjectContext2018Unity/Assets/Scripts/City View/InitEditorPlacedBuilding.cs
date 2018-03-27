using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CityView;
using CityView.Construction;
using CityView.Terrain;

public class InitEditorPlacedBuilding : MonoBehaviour {

    public BuildMode buildMode;
    public IntVector2 coordinates;
    public int dataIndex;

    public void Start() {
        Building building = GetComponent<Building>();
        BuildingsData data = DataManager.BuildingData.dataArray[dataIndex];
        buildMode.BuildAtStart(building, data, coordinates);
    }
}
