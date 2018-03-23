using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CityView;

public interface IEventLog {
     string Text { get; }
}

public class EventLogWaterDamage : IEventLog {

    public string Text { get; private set; }
    public Building building;

    public EventLogWaterDamage(Building building) {
        this.building = building;

        Text = building.data.Name + " is under water!";
    }
}

public class EventLogTradeDealSold : IEventLog {

    public string Text { get; private set; }
    public TradeOffer trade;

    public EventLogTradeDealSold(TradeOffer trade) {
        this.trade = trade;
        string resource = DataManager.ResourcesData.dataArray[trade.productId].Name;

        Text = trade.player.Name + " bought " + trade.amount + " " + resource + " for " + trade.totalValue;
    }
}

public class EventLogResourcesUnavailable : IEventLog {

    public string Text { get; private set; }
    public Building building;
    public int missingResourceID;

    public EventLogResourcesUnavailable(Building building, int missingResourceID) {
        this.building = building;
        this.missingResourceID = missingResourceID;

        string missingResourceName = DataManager.ResourcesData.dataArray[missingResourceID].Name;
        Text = "Not enough " + missingResourceName + " for " + building.data.Name + "!";
    }
}

public class EventLogTemperatureEventTriggered : IEventLog {

    public string Text { get; private set; }
    public float waterLevel;

    public EventLogTemperatureEventTriggered(float waterLevel) {
        this.waterLevel = waterLevel;

        Text = "Water level has increased to " + waterLevel + "!";
    }
}
