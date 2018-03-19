using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class TradeOfferWidget : MonoBehaviour {

        [SerializeField] private ProductAmountItem productOffer;
        [SerializeField] private Text offerCost;
        [SerializeField] private Text playerFromTxt;
        [SerializeField] private Button buyBtn;

        private TradeOffer offer;

        public void Init(MarketPlaceScreen marketPlaceScreen, TradeOffer offer) {
            this.offer = offer;

            playerFromTxt.text = offer.player.Name;
            GameResourcesData resource = DataManager.ResourcesData.dataArray[offer.productId];
            productOffer.Init(DataManager.ResourcePrefabs.GetResourceSprite(resource.ID), offer.amount);
            offerCost.text = offer.totalValue.ToString();

            buyBtn.onClick.AddListener(() => marketPlaceScreen.OpenBuyScreen(offer));
        }
    }
}