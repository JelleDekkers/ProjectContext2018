using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class TradeOfferWidget : MonoBehaviour {

        [SerializeField] private ProductAmountItem productOffer;
        [SerializeField] private ProductAmountItem offerCost;
        [SerializeField] private Text playerFromTxt;
        [SerializeField] private Button buyBtn;

        private TradeOffer offer;

        public void Init(MarketPlaceScreen marketPlaceScreen, TradeOffer offer) {
            this.offer = offer;

            GameResourcesData resource = DataManager.ResourcesData.dataArray[offer.productId];
            productOffer.Init(DataManager.ResourcePrefabs.GetResourceSprite(resource.ID), offer.amount);
            offerCost.Init(DataManager.ResourcePrefabs.MoneySprite, offer.totalValue);

            if (offer.player != null)
                playerFromTxt.text = offer.player.Name;
            else
                playerFromTxt.text = "Null Player";

            buyBtn.onClick.AddListener(() => marketPlaceScreen.BuyTradeOffer(offer));
        }
    }
}