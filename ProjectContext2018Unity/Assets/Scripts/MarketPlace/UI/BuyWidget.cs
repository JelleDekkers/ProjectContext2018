using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class BuyWidget : MonoBehaviour {

        [SerializeField] private Image img;
        [SerializeField] private Text cost;
        [SerializeField] private Button buyButton;
        [SerializeField] private Text title;
        [SerializeField] private InputField amountInputField;

        private MarketPlaceScreen marketWidget;
        private TradeOffer offer;
        private TradeOffer myOffer;

        public void Init(MarketPlaceScreen marketWidget, TradeOffer offer) {
            this.marketWidget = marketWidget;
            this.offer = offer;

            title.text = "Buy from " + offer.player.Name;
            img.sprite = DataManager.ResourcePrefabs.GetResourceSprite(offer.productId);
            cost.text = offer.player.resourcesCostForTrade[offer.productId].ToString();

            myOffer = new TradeOffer(offer.player, offer.productId, 1, offer.player.resourcesCostForTrade[offer.productId]);
            buyButton.onClick.AddListener(() => marketWidget.BuyTradeOffer(myOffer));
            amountInputField.text = 1.ToString();
        }

        public void ClampInputField() {
            int tradeAmount = 1;

            if (int.TryParse(amountInputField.text, out tradeAmount))
                amountInputField.text = (Mathf.Clamp(tradeAmount, 0, offer.player.resourcesAmountForTrade[offer.productId])).ToString();
            else
                amountInputField.text = offer.amount.ToString();

            if (tradeAmount > offer.player.resourcesAmountForTrade[offer.productId])
                tradeAmount = offer.player.resourcesAmountForTrade[offer.productId];

            cost.text = (offer.player.resourcesCostForTrade[offer.productId] * tradeAmount).ToString();

            buyButton.interactable = (tradeAmount > 0);

            myOffer.totalValue = tradeAmount * offer.player.resourcesCostForTrade[offer.productId];

            if (myOffer.totalValue > PlayerResources.Money) {
                float newCost = PlayerResources.Money;
                float newAmount = tradeAmount * newCost /  myOffer.totalValue;
                myOffer.totalValue = newCost;
                tradeAmount = (int)newAmount;
                amountInputField.text = tradeAmount.ToString();
                cost.text = (offer.player.resourcesCostForTrade[offer.productId] * tradeAmount).ToString();
            }

            myOffer.amount = tradeAmount;
        }
    }
}