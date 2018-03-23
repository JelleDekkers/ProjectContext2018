using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {

    public class EventLogWidgetItem : MonoBehaviour {

        [SerializeField] private float aliveTime = 3;

        private void Start() {
            StartCoroutine(DestroySelf());
        }

        private IEnumerator DestroySelf() {
            float timer = 0;

            while (timer < aliveTime) {
                timer += Time.deltaTime;
                yield return null;
            }

            Destroy(gameObject);
        }
    }
}
