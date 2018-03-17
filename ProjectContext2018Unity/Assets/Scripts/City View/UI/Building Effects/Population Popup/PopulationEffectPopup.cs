using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CityView.UI {

    public class PopulationEffectPopup : MonoBehaviour {

        [SerializeField] private AnimationCurve movementCurve;
        [SerializeField] private AnimationCurve scaleCurve;
        [SerializeField] private float spawnHeight = 0;
        [SerializeField] private float duration = 1f;
        [SerializeField] private float targetHeight = 1f;
        [SerializeField] private float startFadeAfter = 0.5f;

        [Header("References")]
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Text amountTxt;
        [SerializeField] private Color negativeColor = Color.red;

        public void Init(Building house, int amount) {
            transform.position = new Vector3(house.transform.position.x, house.tilesStandingOn[0, 0].transform.position.y + spawnHeight, house.transform.position.z);
            transform.SetAsFirstSibling();
            if (amount > 0) {
                amountTxt.text = "+" + amount.ToString();
            } else {
                amountTxt.text = amount.ToString();
                amountTxt.color = negativeColor;
            }
            StartCoroutine(Move());
            StartCoroutine(WaitForFade());
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