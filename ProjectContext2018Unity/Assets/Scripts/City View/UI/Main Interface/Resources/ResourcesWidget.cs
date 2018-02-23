using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityView.UI {

    public class ResourcesWidget : MonoBehaviour {

        private ResourcesWidgetItem[] items;

        private void Start() {
            items = GetComponentsInChildren<ResourcesWidgetItem>();

            foreach(ResourcesWidgetItem item in items) {

            }
        }
    }
}