using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class EventLogTradeDealSoldWidgetItem : EventLogWidgetItem {

        public Text playerNameTxt, stringOneTxt, stringTwoTxt, tradeAmountTxt, tradeValueTxt;
        public Image productImg;

        public void Init(EventLogTradeDealSold log) {
            playerNameTxt.text = log.trade.player.Name;
            stringOneTxt.text = log.stringOne;
            tradeAmountTxt.text = log.trade.amount.ToString();
            productImg.sprite = DataManager.ResourcePrefabs.GetResourceSprite(log.trade.productId);
            stringTwoTxt.text = log.stringTwo;
            tradeValueTxt.text = log.trade.totalValue.ToString();
        }
    }
}