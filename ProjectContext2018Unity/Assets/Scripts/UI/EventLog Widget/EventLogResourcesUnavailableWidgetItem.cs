using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class EventLogResourcesUnavailableWidgetItem : EventLogWidgetItem {

        public Text stringOne, stringTwo, buildingNameTxt;
        public Image resourceImg;

        public void Init(EventLogResourcesUnavailable log) {
            stringOne.text = log.stringOne;
            stringTwo.text = log.stringTwo;
            buildingNameTxt.text = log.building.data.Name;
            resourceImg.sprite = DataManager.ResourcePrefabs.GetResourceSprite(log.missingResourceID);
        }
    }
}