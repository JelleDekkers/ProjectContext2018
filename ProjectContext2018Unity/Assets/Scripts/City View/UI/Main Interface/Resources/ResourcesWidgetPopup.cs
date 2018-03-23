using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {

    public class ResourcesWidgetPopup : MonoBehaviour {

        [SerializeField] private ResourcesWidgetPopupItem itemPrefab;

        public void InstantiateNewItem(int amount) {
            Instantiate(itemPrefab, transform).Init(amount);
        }
    }
}