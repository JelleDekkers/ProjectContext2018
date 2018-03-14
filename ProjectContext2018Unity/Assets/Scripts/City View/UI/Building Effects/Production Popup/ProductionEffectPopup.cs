using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CityView.UI {

    public class ProductionEffectPopup : MonoBehaviour {

        [SerializeField] private AnimationCurve movementCurve;
        [SerializeField] private AnimationCurve scaleCurve;
        [SerializeField] private float spawnHeight = 0;
        [SerializeField] private float duration = 1f;
        [SerializeField] private float targetHeight = 1f;
        [SerializeField] private float startFadeAfter = 0.5f;

        [Header("References")]
        [SerializeField] private ProductionEffectPopupItem item;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Transform itemParent;
        [SerializeField] private GameObject emissionSprite;

        public void Init(BuildingBase building, ProductionCycleResult production) {
            transform.position = new Vector3(building.transform.position.x, building.transform.position.y + spawnHeight, building.transform.position.z);
            transform.SetAsFirstSibling();
            CreatePopupItems(production);
            StartCoroutine(Move());
            StartCoroutine(WaitForFade());
        }

        public void Init(BuildingBase building, BuildingsData data) {
            transform.position = new Vector3(building.transform.position.x, building.tilesStandingOn[0,0].transform.position.y + spawnHeight, building.transform.position.z);
            transform.SetAsFirstSibling();
            CreatePopupItems(data);
            StartCoroutine(Move());
            StartCoroutine(WaitForFade());
        }

        public void Init(BuildingBase building, ClimateBuildingsData data) {
            transform.position = new Vector3(building.transform.position.x, building.tilesStandingOn[0, 0].transform.position.y + spawnHeight, building.transform.position.z);
            transform.SetAsFirstSibling();
            CreatePopupItems(data);
            StartCoroutine(Move());
            StartCoroutine(WaitForFade());
        }

        private void CreatePopupItems(ProductionCycleResult production) {
            emissionSprite.SetActive(production.pollutionPoints != 0);

            if (production.money != 0)
                InstantiateNewPopupItem(DataManager.ResourcePrefabs.MoneySprite, production.money);

            foreach (ResourceContainer resource in production.producedResources) 
                InstantiateNewPopupItem(DataManager.ResourcePrefabs.GetResourceSprite(resource.id), resource.amount);
        }

        private void CreatePopupItems(BuildingsData data) {
            if (data.Moneycost != 0)
                InstantiateNewPopupItem(DataManager.ResourcePrefabs.MoneySprite, -data.Moneycost);

            for(int i = 0; i < data.Resourcecost.Length; i++) {
                GameResourcesData resource = DataManager.ResourcesData.dataArray[data.Resourcecost[i]];
                InstantiateNewPopupItem(DataManager.ResourcePrefabs.GetResourceSprite(resource.ID), -data.Resourcecostamount[i]);
            }
        }

        private void CreatePopupItems(ClimateBuildingsData data) {
            if (data.Moneycost != 0)
                InstantiateNewPopupItem(DataManager.ResourcePrefabs.MoneySprite, -data.Moneycost);

            for (int i = 0; i < data.Resourcecost.Length; i++) {
                GameResourcesData resource = DataManager.ResourcesData.dataArray[data.Resourcecost[i]];
                InstantiateNewPopupItem(DataManager.ResourcePrefabs.GetResourceSprite(resource.ID), -data.Resourcecostamount[i]);
            }
        }

        private void InstantiateNewPopupItem(Sprite sprite, float amount) {
            Instantiate(item, itemParent).Init(sprite, amount);
        }
 
        private IEnumerator Move() {
            float timer = 0;
            float startingHeight = transform.position.y;
            while (timer < duration) {
                float lerp = movementCurve.Evaluate(timer / duration);
                float height = Mathf.Lerp(startingHeight, startingHeight + targetHeight, lerp);
                Vector3 pos = transform.position;
                pos.y = startingHeight + height;
                transform.position = pos;
                lerp = scaleCurve.Evaluate(timer / duration);
                transform.localScale = new Vector3(lerp, lerp, lerp);
                timer += Time.deltaTime;
                yield return null;
            }
            Destroy(gameObject);
        }

        private IEnumerator WaitForFade() {
            float timer = 0;
            while(timer < startFadeAfter) {
                timer += Time.deltaTime;
                yield return null;
            }
            yield return StartCoroutine(Fade());
        }

        private IEnumerator Fade() {
            float timer = 0;
            float fadeDuration = duration - startFadeAfter;
            while (timer < fadeDuration) {
                canvasGroup.alpha = Mathf.Lerp(1, 0, timer / fadeDuration);
                timer += Time.deltaTime;
                yield return null;
            }
            Destroy(gameObject);
        }

    }
}