using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data Manager", menuName = "Tools/Data Manager", order = 0)]
public class DataManager : ScriptableObjectSingleton<DataManager> {

    [SerializeField] private GameResources resourcesData;
    public static GameResources ResourcesData { get { return Instance.resourcesData; } }

    [SerializeField] private GameResourcePrefabs resourcePrefabs;
    public static GameResourcePrefabs ResourcePrefabs { get { return Instance.resourcePrefabs; } }

    [SerializeField] private Buildings buildingData;
    public static Buildings BuildingData { get { return Instance.buildingData; } }

    [SerializeField] private BuildingPrefabs buildingPrefabs;
    public static BuildingPrefabs BuildingPrefabs { get { return Instance.buildingPrefabs; } }
}
