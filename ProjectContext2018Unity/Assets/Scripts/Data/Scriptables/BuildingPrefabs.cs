using System;
using UnityEngine;
using CityView;

[CreateAssetMenu(fileName = "Building Prefabs", menuName = "Tools/Building Prefabs", order = 3)]
public class BuildingPrefabs : ScriptableObject {

    [Header("Prefabs")]
    [SerializeField]
    private BuildingBase fallbackBuilding;
    [SerializeField] private BuildingBase[] buildingPrefabs;

    [Header("Sprites")]
    [SerializeField]
    private Sprite fallbackSprite;
    [SerializeField] private Sprite[] buildingSprites;

    public BuildingBase GetBuildingPrefab(int index) {
        try {
            return buildingPrefabs[index];
        } catch (Exception ex) {
            Debug.LogWarning("Returning fallback prefab, due to error: " + ex);
            return fallbackBuilding;
        }
    }

    public Sprite GetBuildingSprite(int index) {
        try {
            return buildingSprites[index];
        } catch (Exception ex) {
            Debug.LogWarning("Returning fallback sprite, due to error: " + ex);
            return fallbackSprite;
        }
    }
}
