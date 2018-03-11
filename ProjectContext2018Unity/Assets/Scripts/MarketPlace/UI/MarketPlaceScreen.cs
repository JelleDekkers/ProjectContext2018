using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class MarketPlaceScreen : MonoBehaviour {

        [SerializeField] private MarketPlace marketPlace;
        [SerializeField] private GridLayoutGroup contentGrid;
        [SerializeField] private TradeOfferWidget offerWidgetPrefab;

        private void OnEnable() {
            RefreshList();
            marketPlace.OnTradeOffersChanged += RefreshList;
        }

        private void OnDisable() {
            marketPlace.OnTradeOffersChanged -= RefreshList;
        }

        private void RefreshList() {
            contentGrid.transform.RemoveChildren();
            foreach (TradeOffer offer in marketPlace.TradeOffers) {
                if (offer.player != Player.Instance)
                    CreateNewTradeOfferPrefab(offer);
            }
        }

        private void CreateNewTradeOfferPrefab(TradeOffer offer) {
            TradeOfferWidget widget = Instantiate(offerWidgetPrefab);
            widget.transform.SetParent(contentGrid.transform, false);
            widget.Init(this, offer);
        }

        public void BuyTradeOffer(TradeOffer offer) {
            marketPlace.RemoveTradeOffer(offer);
            RefreshList();
            MarketPlace.OnTradeOfferBought(offer);
        }
    }
}