using System;
using UnityEngine;
using CityView;

[CreateAssetMenu(fileName = "Building Prefabs", menuName = "Tools/Building Prefabs", order = 3)]
public class BuildingPrefabs : ScriptableObject {

    [SerializeField] private Building fallbackBuilding;
    [SerializeField] private Building[] buildingsPrefabs;

    [SerializeField] private Sprite fallbackSprite;
    [SerializeField] private Sprite[] buildingSprites;

    public Building GetBuilding(int index) {
        try {
            return buildingsPrefabs[index];
        } catch(Exception ex) {
            Debug.LogWarning("Returning fallback buildingPrefab, due to error: " + ex);
            return fallbackBuilding;
        }
    }

    public Sprite GetSprite(int index) {
        try {
            return buildingSprites[index];
        } catch (Exception ex) {
            Debug.LogWarning("Returning fallback sprite, due to error: " + ex);
            return fallbackSprite;
        }
    }
}
