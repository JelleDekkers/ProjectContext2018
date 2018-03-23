using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class ResourcesWidgetPopupItem : MonoBehaviour {

        [SerializeField] private Text resourceAmountTxt;
        [SerializeField] private AnimationCurve movementCurve, disappearCurve;
        [SerializeField] private float aliveTime = 1;
        [SerializeField] private Color plusColor = Color.white;
        [SerializeField] private Color minColor = Color.red;

        private void Start() {
            StartCoroutine(Move());
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
            while (timer < aliveTime) {
                float lerp = movementCurve.Evaluate(timer / aliveTime);
                float height = Mathf.Lerp(startingHeight, startingHeight, lerp);
                Vector3 pos = transform.position;
                pos.y = startingHeight - height;
                transform.position = pos;
                timer += Time.deltaTime;
                yield return null;
            }
            Destroy(gameObject);
        }
    }
}