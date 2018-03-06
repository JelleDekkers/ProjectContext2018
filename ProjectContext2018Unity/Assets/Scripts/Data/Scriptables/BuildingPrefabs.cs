using System;
using UnityEngine;
using CityView;

[CreateAssetMenu(fileName = "Building Prefabs", menuName = "Tools/Building Prefabs", order = 3)]
public class BuildingPrefabs : ScriptableObject {

    [Header("Prefabs")]
    [SerializeField] private Building fallbackBuilding;
    [SerializeField] private Building[] buildingPrefabs;
    [SerializeField] private ClimateBuilding[] climateBuildingPrefabs;

    [Header("Sprites")]
    [SerializeField] private Sprite fallbackSprite;
    [SerializeField] private Sprite[] buildingSprites;
    [SerializeField] private Sprite[] climateBuildingSprites;

    public Building GetBuildingPrefab(int index) {
        try {
            return buildingPrefabs[index];
        } catch(Exception ex) {
            Debug.LogWarning("Returning fallback prefab, due to error: " + ex);
            return fallbackBuilding;
        }
    }

    public Building GetSpecialBuildingPrefab(int index) {
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

    public Sprite GetSpecialBuildingSprite(int index) {
        try {
            return climateBuildingSprites[index];
        } catch (Exception ex) {
            Debug.LogWarning("Returning fallback sprite, due to error: " + ex);
            return fallbackSprite;
        }
    }
}
