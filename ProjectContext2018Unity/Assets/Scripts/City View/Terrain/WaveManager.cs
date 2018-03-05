using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.Terrain {

    public class WaveManager : MonoBehaviour {

        [SerializeField] private GameTerrain generator;

        [Header("Variables")]
        [SerializeField] private float waveModifier = 0.3f;
        [SerializeField] private float waveSpeed = 0.8f;
        [SerializeField] private float waveNoiseStrength = 0.5f;

        private void Update() {
            ExecuteWaves();
        }

        private void ExecuteWaves() {
            foreach (WaterSourceBlock waterBlock in generator.WaterGrid) {
                if (waterBlock == null)
                    continue;
                float height = Mathf.Sin(-Time.time * waveSpeed + waterBlock.Coordinates.z) * waveModifier;
                height += Mathf.PerlinNoise(waterBlock.Coordinates.x, waterBlock.Coordinates.z + Mathf.Sin(Time.time * 0.1f / (waterBlock.Coordinates.z + 0.1f)) * Mathf.Cos(Time.time * 0.1f / (waterBlock.Coordinates.z + 0.1f))) * waveNoiseStrength;
                waterBlock.ChangeHeight(height);
            }
        }
    }
}