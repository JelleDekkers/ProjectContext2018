using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.Construction {

    public class BuildingPlacementEffectHandler : MonoBehaviour {

        [SerializeField] float dropTime = 0.5f;
        [SerializeField] float startingHeight = 1;
        [SerializeField] AudioClip buildSFX;

        private Building building;

        public void Setup(Building b) {
            building = b;
            transform.position = b.transform.position;
            StartCoroutine(DropEffect());
        }

        private IEnumerator DropEffect() {
            float timer = 0;
            Vector3 target = transform.position;
            Vector3 start = building.transform.position;
            start.y += startingHeight;
            building.transform.position = start;

            while(timer < dropTime) {
                building.transform.position = Vector3.Lerp(start, target, timer / dropTime);
                timer += Time.deltaTime;
                yield return null;
            }

            building.transform.position = target;
            ParticleSystem p = GetComponent<ParticleSystem>();
            CityCamera.Instance.audioSource.PlayOneShot(buildSFX);
            p.Play();
            Destroy(gameObject, p.main.duration);
        }
    }
}