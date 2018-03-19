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
            for(int i = 0; i < DataManager.ResourcesData.dataArray.Length; i++) {
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

        public void OpenBuyScreen(TradeOffer offer) {
            buyWidget.gameObject.SetActive(true);
            buyWidget.Init(this, offer);
        }

        public void BuyTradeOffer(TradeOffer offer) {
            Player.LocalPlayer.CmdTradeWithPlayer(Player.LocalPlayer.PlayerID, offer.player.PlayerID, offer.productId, offer.amount);
            MarketPlace.OnTradeOfferBought(offer);

            buyWidget.gameObject.SetActive(false);
            RefreshTradeOffersList();
        }
    }
}