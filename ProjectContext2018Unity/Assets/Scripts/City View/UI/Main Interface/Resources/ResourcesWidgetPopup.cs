using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {

    public class ResourcesWidgetPopup : MonoBehaviour {

        [SerializeField] private ResourcesWidgetPopupItem itemPrefab;

        public void InstantiateNewItem(int amount) {
            ResourcesWidgetPopupItem item = Instantiate(itemPrefab);
            item.transform.SetParent(transform, false);
            item.Init(amount);
        }
    }
}