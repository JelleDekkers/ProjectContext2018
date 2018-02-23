using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.Construction {

    public class BuildingDestroyEffectHandler : MonoBehaviour {

        [SerializeField] private AudioClip demolishSFX;
        [SerializeField] private new ParticleSystem particleSystem;

        private void Start() {
            CityCamera.Instance.audioSource.PlayOneShot(demolishSFX);
            particleSystem.Play();
            Destroy(gameObject, particleSystem.main.duration);
        }
    }
}
