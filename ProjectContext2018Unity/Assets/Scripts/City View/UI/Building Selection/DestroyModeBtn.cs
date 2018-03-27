using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CityView.Construction {

    public class DestroyModeBtn : MonoBehaviour {

        [SerializeField] private DestroyMode destroyMode;
        [SerializeField] private Toggle toggle;
        [SerializeField] private GameObject buildTab;

        public void Toggle() {
            destroyMode.enabled = toggle.isOn;
            buildTab.gameObject.SetActive(false);
        }
    }
}