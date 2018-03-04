﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.Terrain.Generator {

    [System.Serializable]
    public class TerrainGenerator : MonoBehaviour {

        private static TerrainGenerator instance;
        public static TerrainGenerator Instance { get { return instance; } }

        [SerializeField] private IntVector2 mapSize;
        public IntVector2 MapSize { get { return mapSize; } }

        [HideInInspector] public TerrainBlock[] terrainGrid;
        public TerrainBlock[] TerrainGrid { get { return terrainGrid; } }
        [HideInInspector] public WaterSourceBlock[] waterGrid;
        public WaterSourceBlock[] WaterGrid { get { return waterGrid; } }

        [SerializeField] private Texture2D terrain, water;
        [SerializeField] private TerrainBlock terrainBlockPrefab;
        [SerializeField] private WaterSourceBlock waterBlockPrefab;
        [SerializeField] private Transform terrainParent, waterParent;

        [SerializeField] private Color lowestHeightColor = Color.black;
        [SerializeField] private Color maxHeightColor = Color.white;
        [SerializeField] private float heightMin, heightMax;

        [SerializeField] private float waterStartingHeight = 2;
        [SerializeField] private float waveModifier = 0.3f;
        [SerializeField] private float waveSpeed = 0.8f;
        [SerializeField] private float waveNoiseStrength = 0.5f;

        private void Awake() {
            instance = this;
        }

        private void Update() {
            ExecuteWaves();
        }

        private void ExecuteWaves() {
            foreach (WaterSourceBlock waterBlock in waterGrid) {
                if (waterBlock == null)
                    continue;
                float height = Mathf.Sin(-Time.time * waveSpeed + waterBlock.Coordinates.z) * waveModifier;
                height += Mathf.PerlinNoise(waterBlock.Coordinates.x, waterBlock.Coordinates.z + Mathf.Sin(Time.time * 0.1f) * Mathf.Cos(Time.time * 0.1f)) * waveNoiseStrength;
                waterBlock.ChangeHeight(height);
            }
        }

        private void CleanUp(Object[] grid, Transform parent) {
            Transform[] childs = parent.GetComponentsInChildren<Transform>();
            for (int i = parent.childCount - 1; i >= 0; i--)
                DestroyImmediate(parent.GetChild(i).gameObject);
            grid = null;
        }

        public void Clear() {
            CleanUp(terrainGrid, terrainParent);
            CleanUp(waterGrid, waterParent);
        }

        public void GenerateTerrain() {
            CleanUp(terrainGrid, terrainParent);
            terrainGrid = new TerrainBlock[MapSize.x * MapSize.z];
            for (int x = 0; x < MapSize.x; x++) {
                for (int z = 0; z < MapSize.z; z++) {
                    TerrainBlock gObject;
#if UNITY_EDITOR
                    gObject = UnityEditor.PrefabUtility.InstantiatePrefab(terrainBlockPrefab) as TerrainBlock;
#else
                    gObject = Instantiate(tilePrefab);
#endif
                    gObject.transform.position = new Vector3(x, 0, z);
                    gObject.transform.SetParent(terrainParent);
                    terrainGrid[ConvertToSingleArrayIndex(x, z)] = gObject;
                    Color pixelColor = terrain.GetPixel(terrain.width / MapSize.x * x, terrain.height / MapSize.z * z);
                    float normalized = GetNormalized(pixelColor);
                    gObject.Init(new IntVector2(x, z), GetAdjustedColor(normalized), GetHeightFromScale(normalized));
                }
            }
        }

        public void GenerateOcean() {
            CleanUp(waterGrid, waterParent);
            waterGrid = new WaterSourceBlock[MapSize.x * MapSize.z];
            for (int x = 0; x < MapSize.x; x++) {
                for (int z = 0; z < MapSize.z; z++) {
                    Color pixelColor = terrain.GetPixel(terrain.width / MapSize.x * x, terrain.height / MapSize.z * z);
                    float normalizedHeightValue = GetNormalized(pixelColor);
                    TerrainBlock tileBeneath = terrainGrid[ConvertToSingleArrayIndex(x, z)];
                    float height = waterStartingHeight - tileBeneath.TotalHeight;

                    if (normalizedHeightValue > waterStartingHeight || height <= 0)
                        continue;

                    WaterSourceBlock gObject;
#if UNITY_EDITOR
                    gObject = UnityEditor.PrefabUtility.InstantiatePrefab(waterBlockPrefab) as WaterSourceBlock;
#else
                    gObject = Instantiate(waterPrefab);
#endif
                    gObject.transform.position = new Vector3(x, tileBeneath.TotalHeight, z);
                    gObject.transform.SetParent(waterParent);
                    gObject.Init(new IntVector2(x, z), tileBeneath, height, this);
                    waterGrid[ConvertToSingleArrayIndex(x, z)] = gObject;
                }
            }
        }

        public TerrainBlock GetTerrainBlock(IntVector2 coordinates) {
            if (!IsInsideGrid(coordinates))
                return null;
            return terrainGrid[ConvertToSingleArrayIndex(coordinates.x, coordinates.z)];
        }

        public WaterSourceBlock GetWaterBlock(IntVector2 coordinates) {
            if (!IsInsideGrid(coordinates))
                return null;
            return waterGrid[ConvertToSingleArrayIndex(coordinates.x, coordinates.z)];
        }

        public void AddToWaterGrid(WaterSourceBlock block) {
            waterGrid[ConvertToSingleArrayIndex(block.Coordinates.x, block.Coordinates.z)] = block;
        }

        public bool IsInsideGrid(IntVector2 coordinates) {
            return (coordinates.x >= 0 && coordinates.x < MapSize.x && coordinates.z >= 0 && coordinates.z < MapSize.z);
        }

        private int ConvertToSingleArrayIndex(int x, int z) {
            return z + (x * MapSize.z);
        }

        private Color GetAdjustedColor(float scale) {
            return Color.Lerp(lowestHeightColor, maxHeightColor, scale);
        }

        private float GetHeightFromScale(float scale) {
            float height = (heightMin + heightMax) * scale + heightMin;
            return height;
        }

        private float GetNormalized(Color c) {
            float value = c.r + c.g + c.b;
            return value / 3;
        }
    }
}