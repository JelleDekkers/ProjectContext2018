using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Resource Prefabs", menuName = "Tools/Resource Prefabs", order = 1)]
public class GameResourcePrefabs : ScriptableObject {

    public Sprite errorImg;
    [SerializeField]
    private Sprite[] images;

    public Sprite GetSprite(int index) {
        try {
            return images[index];
        } catch (Exception ex) {
            Debug.LogWarning(ex + ", returning errorImg");
            return errorImg;
        }
    }
}
