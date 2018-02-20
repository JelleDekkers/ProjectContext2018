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
        [SerializeField] private Sprite placeholderSprite;
        [SerializeField] private CanvasGroup canvasGroup;

        public void Init(Building b, ProductionCycleResult production) {
            transform.position = new Vector3(b.transform.position.x, b.transform.position.y + spawnHeight, b.transform.position.z);
            StartCoroutine(Move());
            StartCoroutine(WaitForFade());
            CreatePopupItems(production);
        }

        private void CreatePopupItems(ProductionCycleResult production) {
            // TODO: ook gebruiken wanneer sprites bescikbaar zijn
            //if (production.money != 0)
            //    InstantiatePopupItem(null, production.money);
            //if (production.researchPoints != 0)
            //    InstantiatePopupItem(null, production.researchPoints);
            //if (production.pollutionPoints != 0)
            //    InstantiatePopupItem(null, production.pollutionPoints);

            foreach(ResourceContainer resource in production.producedResources) 
                InstantiateNewPopupItem(DataManager.ResourcePrefabs.GetSprite(resource.id), resource.amount);
        }

        private void InstantiateNewPopupItem(Sprite sprite, float amount) {
            Instantiate(item, transform).Init(sprite, amount);
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