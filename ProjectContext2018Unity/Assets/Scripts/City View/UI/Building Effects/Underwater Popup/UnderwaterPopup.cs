using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.UI {

    public class UnderwaterPopup : MonoBehaviour {

        [SerializeField] private AnimationCurve scaleCurveAtStart;
        [SerializeField] private float scaleAtStartDuration;
        [SerializeField] private AnimationCurve disappearCurve;
        [SerializeField] private float disappearTime = 1;

        private Building building;

        private void Start() {
            StartCoroutine(Scale());
        }

        public void Init(Building building) {
            this.building = building;
            building.OnWaterIsGone += DestroySelf;
        }

        private void DestroySelf() {
            building.OnWaterIsGone -= DestroySelf;
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
            while (timer < disappearTime) {
                lerp = 1 - disappearCurve.Evaluate(timer / disappearTime);
                transform.localScale = new Vector3(lerp, lerp, lerp);
                timer += Time.deltaTime;
                yield return null;
            }
            Destroy(gameObject);
        }
    }
}
