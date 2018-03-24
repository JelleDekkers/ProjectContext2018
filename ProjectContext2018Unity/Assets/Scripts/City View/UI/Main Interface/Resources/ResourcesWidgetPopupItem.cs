using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class ResourcesWidgetPopupItem : MonoBehaviour {

        [SerializeField] private Text resourceAmountTxt;
        [SerializeField] private AnimationCurve movementCurve, disappearCurve;
        [SerializeField] private float aliveTime = 1;
        [SerializeField] private float startFadeAfter = 1;
        [SerializeField] private float targetHeight = 40;
        [SerializeField] private Color plusColor = Color.white;
        [SerializeField] private Color minColor = Color.red;
        [SerializeField] private CanvasGroup canvasGroup;

        private void Start() {
            StartCoroutine(Move());
            StartCoroutine(WaitForFade());
        }

        public void Init(int amount) {
            if (amount > 0) {
                resourceAmountTxt.color = plusColor;
                resourceAmountTxt.text = "+" + amount.ToString();
            } else {
                resourceAmountTxt.color = minColor;
                resourceAmountTxt.text = amount.ToString();
            }
        }

        private IEnumerator Move() {
            float timer = 0;
            float startingHeight = transform.position.y;
            float targetHeight = transform.position.y - this.targetHeight;
            while (timer < aliveTime) {
                float lerp = movementCurve.Evaluate(timer / aliveTime);
                float lerpHeight = Mathf.Lerp(startingHeight, targetHeight, lerp);
                Vector3 pos = transform.position;
                pos.y = lerpHeight;
                transform.position = pos;
                timer += Time.deltaTime;
                yield return null;
            }
            Destroy(gameObject);
        }


        private IEnumerator WaitForFade() {
            float timer = 0;
            while (timer < startFadeAfter) {
                timer += Time.deltaTime;
                yield return null;
            }
            yield return StartCoroutine(Fade());
        }

        private IEnumerator Fade() {
            float timer = 0;
            float fadeDuration = aliveTime - startFadeAfter;
            while (timer < fadeDuration) {
                canvasGroup.alpha = Mathf.Lerp(1, 0, timer / fadeDuration);
                timer += Time.deltaTime;
                yield return null;
            }
            Destroy(gameObject);
        }

    }
}