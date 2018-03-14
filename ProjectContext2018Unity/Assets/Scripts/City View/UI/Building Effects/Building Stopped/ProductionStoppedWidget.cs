using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.UI {

    public class ProductionStoppedWidget : MonoBehaviour {

        [SerializeField] private Transform itemParent;
        [SerializeField] private AnimationCurve scaleCurveAtStart;
        [SerializeField] private float scaleAtStartDuration;
        [SerializeField] private ProductionStoppedWidgetItem itemPrefab;

        private Building building;

        private void Start() {
            StartCoroutine(Scale());
        }

        public void Init(Building building) {
            building.OnProductionResumed += DestroySelf;
            building.OnDestroyed += DestroySelf;

            if (!PlayerResources.Instance.HasMoneyAmount(building.data.Moneycost))
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
            if (building != null) {
                building.OnProductionResumed -= DestroySelf;
                building.OnDestroyed -= DestroySelf;
            }
            Destroy(gameObject);
        }

        private IEnumerator Scale() {
            float timer = 0;
            float startingHeight = transform.position.y;
            while (timer < scaleAtStartDuration) {
                float lerp = scaleCurveAtStart.Evaluate(timer / scaleAtStartDuration);
                transform.localScale = new Vector3(lerp, lerp, lerp);
                timer += Time.deltaTime;
                yield return null;
            }
        }
    }
}