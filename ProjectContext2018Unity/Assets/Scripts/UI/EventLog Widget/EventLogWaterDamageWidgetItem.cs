using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class EventLogWaterDamageWidgetItem : EventLogWidgetItem {

        public Text buildingNameTxt, stringOneTxt;

        public void Init(EventLogWaterDamage log) {
            buildingNameTxt.text = log.building.data.Name;
            stringOneTxt.text = log.stringOne;
        }
    }
}