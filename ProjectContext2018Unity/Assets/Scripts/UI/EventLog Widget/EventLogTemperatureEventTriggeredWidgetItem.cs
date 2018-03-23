using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class EventLogTemperatureEventTriggeredWidgetItem : EventLogWidgetItem {

        public Text stringOneTxt, stringTwoTxt;

        public void Init(EventLogTemperatureEventTriggered log) {
            stringOneTxt.text = log.stringOne;
            stringTwoTxt.text = log.waterLevel.ToString() + " meter";
        }
    }
}