using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.Terrain {

    [System.Serializable, SelectionBase]
    public class WaterSourceBlock : MonoBehaviour {

        [SerializeField] private IntVector2 coordinates;
        public IntVector2 Coordinates { get { return coordinates; } }

        [SerializeField] private float startingheight;
        public float Startingheight { get { return startingheight; } }

        [SerializeField] private float currentHeight;
        public float CurrentHeight { get { return currentHeight; } }

        [SerializeField] private TerrainBlock blockBeneath;
        public TerrainBlock BlockBeneath { get { return blockBeneath; } }

        [SerializeField] private float newWaterBlockCreationTimer = 2;
        [SerializeField] private float scaleIncreasePerDegree = 0.01f;

        [HideInInspector]
        public GameTerrain generator;

        public TerrainBlock[] neighbours = new TerrainBlock[4];
        public List<WaterSourceBlock> children;
        public float flowHeightDecrease = 0.2f;
        public Action OnDestroyEvent;

        [HideInInspector]
        public new Renderer renderer;

        private Coroutine heightCoroutine;

        private void Start() {
            WorldTemperature.OnWorldTemperatureChanged += AddHeightToTemperature;
            blockBeneath.OnHeightChange += CheckBlockBeneathSize;

            StartCoroutine(CheckForPossibleNewWaterBlockCoroutine());

            // TODO: nettere manier
            City.Instance.TilesGrid.GetTile(coordinates).OnWaterLevelChanged(true);
        }

        private void UpdatePossibleNeighbours() {
            for (int i = 0; i < neighbours.Length; i++) {
                IntVector2 neighbourCoordinates = coordinates + IntVector2.NeighbourCoordinates[i];
                if (generator.IsInsideGrid(neighbourCoordinates) && generator.GetWaterBlock(neighbourCoordinates) == null) {
                    neighbours[i] = generator.GetTerrainBlock(neighbourCoordinates);
                }
            }
        }

        public void Init(IntVector2 coordinates, TerrainBlock blockBeneath, float height, GameTerrain generator) {
            this.coordinates = coordinates;
            this.generator = generator;
            this.blockBeneath = blockBeneath;
            renderer = transform.GetChild(0).GetComponent<Renderer>();
            SetHeight(height);
            startingheight = height;
        }

        private void SetHeight(float height) {
            if (UnityEditor.EditorApplication.isPlaying) {
                if (heightCoroutine != null) {
                    StopCoroutine(heightCoroutine);
                    heightCoroutine = null;
                }

                heightCoroutine = StartCoroutine(SetHeightCoroutine(height));
                StartCoroutine(CheckForPossibleNewWaterBlockCoroutine());
            } else {
                transform.localScale = new Vector3(transform.localScale.x, height, transform.localScale.z);
                currentHeight = height;
            }
        }

        private IEnumerator SetHeightCoroutine(float target) {
            float height = currentHeight;
            float time = 1;
            float timer = 0;

            while(timer < time) {
                currentHeight = Mathf.Lerp(height, target, time / timer);
                timer += Time.deltaTime;
                yield return null;
            }
        }

        private void AddHeightToTemperature(float amount) {
            SetHeight(currentHeight + (amount * scaleIncreasePerDegree));
        }

        public void AdjustScaleToWaves(float amount) {
            if (currentHeight + amount > 0)
                renderer.enabled = true;
            else
                renderer.enabled = false;

            transform.localScale = new Vector3(transform.localScale.x, currentHeight + amount, transform.localScale.z);
        }

        private void CheckBlockBeneathSize(float blockBeneathNewHeight) {
            if (blockBeneathNewHeight >= currentHeight + transform.position.y)
                Destroy(gameObject);
        }

        private void CheckForPossibleNewWaterBlock() {
            UpdatePossibleNeighbours();

            for (int i = 0; i < neighbours.Length; i++) {
                if (neighbours[i] == null)
                    continue;

                if (neighbours[i].TotalHeight < currentHeight + blockBeneath.TotalHeight - flowHeightDecrease &&
                    generator.GetWaterBlock(neighbours[i].Coordinates) == null) {
                    CreateNewWaterBlock(neighbours[i]);
                }
            }
        }

        private IEnumerator CheckForPossibleNewWaterBlockCoroutine() {
            float timer = 0;

            while (timer < newWaterBlockCreationTimer) {
                timer += Time.deltaTime;
                yield return null;
            }
            CheckForPossibleNewWaterBlock();
        }

        private void CreateNewWaterBlock(TerrainBlock neighbourTerrainBlock) {
            WaterSourceBlock waterBlock = Instantiate(this);

            waterBlock.transform.position = new Vector3(neighbourTerrainBlock.transform.position.x, neighbourTerrainBlock.TotalHeight, neighbourTerrainBlock.transform.position.z);
            waterBlock.transform.SetParent(transform.parent);

            float height;
            if ((neighbourTerrainBlock.TotalHeight + currentHeight) - blockBeneath.TotalHeight > 0)
                height = (blockBeneath.TotalHeight + currentHeight) - neighbourTerrainBlock.TotalHeight - flowHeightDecrease;
            else
                height = currentHeight - flowHeightDecrease;

            waterBlock.Init(neighbourTerrainBlock.Coordinates, neighbourTerrainBlock, height, generator);
            children.Add(waterBlock);
            generator.AddToWaterGrid(waterBlock);
        }

        private void OnDestroy() {
            if(OnDestroyEvent != null)
                OnDestroyEvent();

            foreach (WaterSourceBlock block in children) {
                if(block != null)
                    Destroy(block.gameObject);
            }

            WorldTemperature.OnWorldTemperatureChanged += AddHeightToTemperature;
            blockBeneath.OnHeightChange -= CheckBlockBeneathSize;
        }
    }
}