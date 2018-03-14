using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Resource Prefabs", menuName = "Tools/Resource Prefabs", order = 1)]
public class GameResourcePrefabs : ScriptableObject {

    [SerializeField] private Sprite fallbackSprite;

    [SerializeField] private Sprite moneySprite;
    public Sprite MoneySprite { get { return moneySprite; } }

    [SerializeField] private Sprite pollutionSprite;
    public Sprite PollutionSprite { get { return pollutionSprite; } }

    [SerializeField] private Sprite populationSprite;
    public Sprite PopulationSprite { get { return populationSprite; } }

    [SerializeField] private Sprite[] resourceSprites;

    public Sprite GetResourceSprite(int index) {
        try {
            return resourceSprites[index];
        } catch (Exception ex) {
            Debug.LogWarning("Returning fallback sprite, due to error: " + ex);
            return fallbackSprite;
        }
    }
}
