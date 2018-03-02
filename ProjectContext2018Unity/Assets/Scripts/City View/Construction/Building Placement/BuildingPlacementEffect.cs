using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.Construction {

    public class BuildingPlacementEffect : MonoBehaviour {

        [SerializeField] float dropTime = 0.5f;
        [SerializeField] float startingHeight = 1;
        [SerializeField] AudioClip buildSFX;
        [SerializeField] private AnimationCurve dropEffectCurve;
        [SerializeField] private new ParticleSystem particleSystem;

        private Transform buildingMeshRoot;

        public void Setup(Building building) {
            buildingMeshRoot = building.transform;
            transform.position = building.transform.position;
            StartCoroutine(DropEffect());
        }

        private IEnumerator DropEffect() {
            float timer = 0;
            Vector3 target = transform.position;
            Vector3 start = buildingMeshRoot.transform.position;
            start.y += startingHeight;
            buildingMeshRoot.transform.position = start;
            float interpolation = 0;

            while(timer < dropTime) {
                interpolation = dropEffectCurve.Evaluate(timer / dropTime);
                buildingMeshRoot.transform.position = Vector3.Lerp(start, target, interpolation);
                timer += Time.deltaTime;
                yield return null;
            }

            buildingMeshRoot.transform.position = target;
            CityCamera.Instance.audioSource.PlayOneShot(buildSFX);
            CityCamera.Instance.cameraShaker.Shake();
            particleSystem.Play();
            Destroy(gameObject, particleSystem.main.duration);
        }
    }
}