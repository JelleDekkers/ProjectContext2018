using System;
using System.Collections.Generic;
using UnityEngine;
using CityView;

public static class EventLogManager {

    public static Queue<IEventLog> EventLogs = new Queue<IEventLog>();
    public static Action<IEventLog> OnNewEventLog;

    public static void AddEventLogs(IEventLog eventLog) {
        EventLogs.Enqueue(eventLog);

        if(OnNewEventLog != null)
            OnNewEventLog(eventLog);
    }

    public static void AddNewResourceUnavailableLog(Building building, int missingResourceID) {
        AddEventLogs(new EventLogResourcesUnavailable(building, missingResourceID));
    }

    public static void AddNewUnderwaterLog(Building building) {
        AddEventLogs(new EventLogWaterDamage(building));
    }

    public static void AddNewTradeOfferSoldLog(TradeOffer trade) {
        AddEventLogs(new EventLogTradeDealSold(trade));
    }

    public static void AddNewTemperatureEventTriggeredLog(float waterLevel) {
        AddEventLogs(new EventLogTemperatureEventTriggered(waterLevel));
    }
}
