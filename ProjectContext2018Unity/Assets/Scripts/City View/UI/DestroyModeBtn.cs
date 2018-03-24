using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.Construction {

    public class DestroyModeBtn : MonoBehaviour {

        [SerializeField] private DestroyMode destroyMode;

        public void Toggle() {
            destroyMode.enabled = !destroyMode.enabled;
        }
    }
}