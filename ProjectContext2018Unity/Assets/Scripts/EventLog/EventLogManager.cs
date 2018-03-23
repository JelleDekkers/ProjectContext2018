using System;
using System.Collections.Generic;
using UnityEngine;
using CityView;

public static class EventLogManager {

    public static Queue<IEventLog> EventLogs = new Queue<IEventLog>();

    public static void AddEventLogs(IEventLog eventLog) {
        EventLogs.Enqueue(eventLog);
        Debug.Log(eventLog.Text);
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
