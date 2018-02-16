using System;
using UnityEngine;
using CityView;

[CreateAssetMenu(fileName = "Building Prefabs", menuName = "Tools/Building Prefabs", order = 3)]
public class BuildingPrefabs : ScriptableObject {

    public Building[] buildings;
    public Sprite[] images;

    public Building GetBuilding(int index) {
        try {
            return buildings[index];
        } catch(Exception ex) {
            throw ex;
        }
    }

    public Sprite GetSprite(int index) {
        try {
            return images[index];
        } catch (Exception ex) {
            throw ex;
        }
    }
}
