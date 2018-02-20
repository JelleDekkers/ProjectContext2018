using System;
using UnityEngine;
using CityView;

[CreateAssetMenu(fileName = "Building Prefabs", menuName = "Tools/Building Prefabs", order = 3)]
public class BuildingPrefabs : ScriptableObject {

    public Building errorBuilding;
    [SerializeField]
    private Building[] buildings;

    public Sprite errorImg;
    [SerializeField]
    private Sprite[] images;

    public Building GetBuilding(int index) {
        try {
            return buildings[index];
        } catch(Exception ex) {
            Debug.LogWarning(ex + ", returning errorImg");
            return errorBuilding;
        }
    }

    public Sprite GetSprite(int index) {
        try {
            return images[index];
        } catch (Exception ex) {
            Debug.LogWarning(ex + ", returning errorImg");
            return errorImg;
        }
    }
}
