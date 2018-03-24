using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class MarketPlaceScreen : MonoBehaviour {

        [SerializeField] private MarketPlace marketPlace;
        [SerializeField] private GridLayoutGroup contentGrid, filterGrid;
        [SerializeField] private TradeOfferWidget offerWidgetPrefab;
        [SerializeField] private ResourceFilterWidget filterWidgetPrefab;
        [SerializeField] private ToggleGroup toggleGroup;
        [SerializeField] private BuyWidget buyWidget;

        private int currentFilterIndex;
        private TradeOfferWidget selectedWidget;

        private void Start() {
            FillFilterGrid();
        }

        private void OnEnable() {
            RefreshTradeOffersList();
            marketPlace.OnTradeOffersChanged += RefreshTradeOffersList;
        }

        private void OnDisable() {
            marketPlace.OnTradeOffersChanged -= RefreshTradeOffersList;
        }

        public void UpdateFilter(int index) {
            currentFilterIndex = index;
            RefreshTradeOffersList();
        }

        private void FillFilterGrid() {
            filterGrid.transform.RemoveChildren();
            // start at 1 to skip Energy
            for(int i = 1; i < DataManager.ResourcesData.dataArray.Length; i++) {
                Instantiate(filterWidgetPrefab, filterGrid.transform).Init(this, i, toggleGroup);
            }
        }

        private void RefreshTradeOffersList() {
            contentGrid.transform.RemoveChildren();
            foreach (Player player in PlayerList.Instance.Players) {
                if (player == Player.LocalPlayer)
                    continue;

                if (player.resourcesAmountForTrade[currentFilterIndex] <= 0)
                    continue;

                TradeOffer offer = new TradeOffer(player, currentFilterIndex, player.resourcesAmountForTrade[currentFilterIndex], player.resourcesCostForTrade[currentFilterIndex]);
                CreateNewTradeOfferItem(offer);
            }
        }

        private void CreateNewTradeOfferItem(TradeOffer offer) {
            TradeOfferWidget widget = Instantiate(offerWidgetPrefab);
            widget.transform.SetParent(contentGrid.transform, false);
            widget.Init(this, offer);
        }

        public void OpenBuyScreen(TradeOfferWidget widget, TradeOffer offer) {
            buyWidget.gameObject.SetActive(true);
            buyWidget.Init(this, offer);
            selectedWidget = widget;
        }

        public void BuyTradeOffer(TradeOffer offer) {
            selectedWidget.SetProductAmount(offer.player.resourcesAmountForTrade[offer.productId] - offer.amount);
            Player.LocalPlayer.CmdTradeWithPlayer(Player.LocalPlayer.PlayerID, offer.player.PlayerID, offer.productId, offer.amount);
            MarketPlace.OnTradeOfferBought(offer);
            buyWidget.gameObject.SetActive(false);
            PlayerResources.Instance.RemoveMoney(offer.totalValue);
            //RefreshTradeOffersList();
        }
    }
}