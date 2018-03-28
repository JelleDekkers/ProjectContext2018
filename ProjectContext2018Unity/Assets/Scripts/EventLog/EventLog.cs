using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CityView;

public interface IEventLog {
    string FullText { get; }
}

public class EventLogWaterDamage : IEventLog {

    public string FullText { get; private set; }
    public string stringOne;

    public Building building;

    public EventLogWaterDamage(Building building) {
        this.building = building;

        stringOne = " is under water";
        FullText = building.data.Name + stringOne;
    }
}

public class EventLogTradeDealSold : IEventLog {

    public string FullText { get; private set; }
    public string stringOne, stringTwo;
    public TradeOffer trade;

    public EventLogTradeDealSold(TradeOffer trade) {
        this.trade = trade;
        string resource = DataManager.ResourcesData.dataArray[trade.productId].Name;

        stringOne = " bought ";
        stringTwo = " for ";
        FullText = trade.player.Name + stringOne + trade.amount + " " + resource + stringTwo + trade.totalValue;
    }
}

public class EventLogResourcesUnavailable : IEventLog {

    public string FullText { get; private set; }
    public string stringOne, stringTwo;
    public Building building;
    public int missingResourceID;

    public EventLogResourcesUnavailable(Building building, int missingResourceID) {
        this.building = building;
        this.missingResourceID = missingResourceID;
        string missingResourceName = DataManager.ResourcesData.dataArray[missingResourceID].Name;
        stringOne = "Not enough ";
        stringTwo = " for ";
        FullText = stringOne + missingResourceName + stringTwo + building.data.Name + "!";
    }
}

public class EventLogTemperatureEventTriggered : IEventLog {

    public string FullText { get; private set; }
    public string stringOne;
    public float waterLevel;

    public EventLogTemperatureEventTriggered(float waterLevel) {
        this.waterLevel = waterLevel;

        stringOne = "Water level has increased to ";
        FullText = stringOne + waterLevel + "!";
    }
}
