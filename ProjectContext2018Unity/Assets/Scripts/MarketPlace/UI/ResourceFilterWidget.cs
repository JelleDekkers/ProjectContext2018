using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class ResourceFilterWidget : MonoBehaviour {

        [SerializeField] private Image img;
        [SerializeField] Toggle toggle;

        private MarketPlaceScreen screen;
        private int id;

        public void Init(MarketPlaceScreen marketPlaceWidget, int id, ToggleGroup group) {
            this.screen = marketPlaceWidget;
            img.sprite = DataManager.ResourcePrefabs.GetResourceSprite(id);
            toggle.group = group;
            toggle.onValueChanged.AddListener((bool b) => marketPlaceWidget.UpdateFilter(id));
            toggle.isOn = (id == 0);
        }

    }
}