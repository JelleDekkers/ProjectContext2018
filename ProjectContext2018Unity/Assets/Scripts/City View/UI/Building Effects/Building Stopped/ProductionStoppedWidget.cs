using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.UI {

    public class ProductionStoppedWidget : MonoBehaviour {

        [SerializeField] private Transform itemParent;
        [SerializeField] private AnimationCurve scaleCurveAtStart;
        [SerializeField] private float scaleAtStartDuration;
        [SerializeField] private ProductionStoppedWidgetItem itemPrefab;
        [SerializeField] private AnimationCurve disappearCurve;
        [SerializeField] private float disappearTime = 1;

        private Building building;

        private void Start() {
            StartCoroutine(Scale());
        }

        public void Init(Building building) {
            this.building = building;
            building.OnProductionResumed += DestroySelf;
            building.OnDestroyed += DestroySelf;

            if (!PlayerResources.Instance.HasMoneyAmount(building.data.Moneyinput))
                CreateWidgetItem(DataManager.ResourcePrefabs.MoneySprite);

            for (int i = 0; i < building.data.Resourceinput.Length; i++) {
                if (!PlayerResources.Instance.HasResourceAmount(building.data.Resourceinput[i], building.data.Resourceinputamount[i]))
                    CreateWidgetItem(DataManager.ResourcePrefabs.GetResourceSprite(building.data.Resourceinput[i]));
            }
        }
      
        private void CreateWidgetItem(Sprite sprite) {
            Instantiate(itemPrefab, itemParent).Init(sprite);
        }

        private void DestroySelf() {
            building.OnProductionResumed -= DestroySelf;
            building.OnDestroyed -= DestroySelf;
            StartCoroutine(Disappear());
        }

        private IEnumerator Scale() {
            float timer = 0;
            float startingHeight = transform.position.y;
            float lerp;
            while (timer < scaleAtStartDuration) {
                lerp = scaleCurveAtStart.Evaluate(timer / scaleAtStartDuration);
                transform.localScale = new Vector3(lerp, lerp, lerp);
                timer += Time.deltaTime;
                yield return null;
            }
        }

        private IEnumerator Disappear() {
            float timer = 0;
            float lerp;
            Vector3 scale = transform.localScale;
            while(timer < disappearTime) {
                lerp = 1 - disappearCurve.Evaluate(timer / disappearTime);
                transform.localScale = new Vector3(lerp, lerp, lerp);
                timer += Time.deltaTime;
                yield return null;
            }
            Destroy(gameObject);
        }
    }
}