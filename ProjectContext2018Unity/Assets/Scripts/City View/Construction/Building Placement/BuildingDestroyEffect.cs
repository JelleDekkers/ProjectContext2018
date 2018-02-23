using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.Construction {

    public class BuildingDestroyEffect : MonoBehaviour {

        [SerializeField] private AudioClip demolishSFX;
        [SerializeField] private new ParticleSystem particleSystem;
        [SerializeField] private float shrinkTime = 1f;

        public void Init(Building building) {
            CityCamera.Instance.audioSource.PlayOneShot(demolishSFX);
            particleSystem.Play();
            float destroyTime = ((shrinkTime > particleSystem.main.duration) ? shrinkTime : particleSystem.main.duration) + 1f;
            Destroy(gameObject, destroyTime);
            StartCoroutine(Shrink(building.transform));
            CityCamera.Instance.audioSource.PlayOneShot(demolishSFX);
        }

        private IEnumerator Shrink(Transform building) {
            Vector3 start = building.localScale;
            Vector3 target = Vector3.zero;
            float timer = 0;

            while (timer < shrinkTime) {
                building.localScale = Vector3.Lerp(start, target, timer / shrinkTime);
                timer += Time.deltaTime;
                yield return null;
            }

            building.localScale = target;
            Destroy(building.gameObject);
        }
    }
}
