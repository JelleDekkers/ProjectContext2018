using System;
using UnityEngine;
using CityView;

[CreateAssetMenu(fileName = "Building Prefabs", menuName = "Tools/Building Prefabs", order = 3)]
public class BuildingPrefabs : ScriptableObject {

    public Building errorBuilding;
    public Building[] buildings;

    public Sprite errorImg;
    public Sprite[] images;

    public Building GetBuilding(int index) {
        try {
            return buildings[index];
        } catch(Exception ex) {
            Debug.LogError(ex);
            return errorBuilding;
        }
    }

    public Sprite GetSprite(int index) {
        try {
            return images[index];
        } catch (Exception ex) {
            Debug.LogError(ex);
            return errorImg;
        }
    }
}
