using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class EventLogWidgetManager : MonoBehaviour {

        [SerializeField] private int maxItemsShowing = 5;
        [SerializeField] private VerticalLayoutGroup group;
        [SerializeField] private EventLogWaterDamageWidgetItem waterDamageLogItem;
        [SerializeField] private EventLogTradeDealSoldWidgetItem tradeDealSoldItem;
        [SerializeField] private EventLogResourcesUnavailableWidgetItem resourceUnavailableItem;
        [SerializeField] private EventLogTemperatureEventTriggeredWidgetItem temperatureEventItem;

        private void Start() {
            EventLogManager.OnNewEventLog += CreateNewItem;
        }

        private void OnDestroy() {
            EventLogManager.OnNewEventLog -= CreateNewItem;
        }

        public void CreateNewItem(IEventLog log) {
            if (log.GetType() == typeof(EventLogWaterDamage))
                Instantiate(waterDamageLogItem, transform).Init(log as EventLogWaterDamage);
            else if (log.GetType() == typeof(EventLogTradeDealSold))
                Instantiate(tradeDealSoldItem, transform).Init(log as EventLogTradeDealSold);
            else if (log.GetType() == typeof(EventLogResourcesUnavailable))
                Instantiate(resourceUnavailableItem, transform).Init(log as EventLogResourcesUnavailable);
            else if (log.GetType() == typeof(EventLogTemperatureEventTriggered))
                Instantiate(temperatureEventItem, transform).Init(log as EventLogTemperatureEventTriggered);

            if (transform.childCount > maxItemsShowing)
                Destroy(transform.GetChild(0).gameObject);
        }
    }
}