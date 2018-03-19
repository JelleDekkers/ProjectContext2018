﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EarthView.UI {

    public class PlayerInfoWidget : MonoBehaviour {

        [SerializeField] private Text nameTxt;
        [SerializeField] private Text pollutionPerMinuteTxt;
        [SerializeField] private GridLayoutGroup tradeOffers;
        [SerializeField] private TradeOfferWidget tradeOfferWidgetPrefab;
        [SerializeField] private InputField amountInputField;
        [SerializeField] private Text costTxt;
        [SerializeField] private GameObject tradeInfoObject;
        [SerializeField] private Button acceptButton;

        private Player playerInspecting;
        private CityObject cityObject;
        private int tradeOfferSelectionID;
        private int amount = 0;
        private float cost;

        private void Awake() {
            CityObject.OnSelected += Init;
            gameObject.SetActive(false);
        }

        public void Init(CityObject city, Player player) {
            // if is localPlayer return;
            playerInspecting = player;
            cityObject = city;

            nameTxt.text = player.Name;
            if (player == Player.LocalPlayer)
                nameTxt.text += "(You)";
            gameObject.SetActive(true);
            pollutionPerMinuteTxt.text = player.PlayerPollution.ToString();
            InstantiateTradeOffers();
        }

        private void InstantiateTradeOffers() {
            tradeOffers.transform.RemoveChildren();
            for(int i = 0; i < playerInspecting.resourcesAmountForTrade.Count; i++) {
                Instantiate(tradeOfferWidgetPrefab, tradeOffers.transform).Init(this, playerInspecting, i);
            }
        }

        public void UpdateTradeOfferSelection(int newID) {
            tradeOfferSelectionID = newID;
            amountInputField.text = (0).ToString();
            tradeInfoObject.gameObject.SetActive(true);
        }

        public void ClampInputField() {
            amount = 0;
            if (int.TryParse(amountInputField.text, out amount))
                amountInputField.text = (Mathf.Clamp(amount, 0, playerInspecting.resourcesAmountForTrade[tradeOfferSelectionID])).ToString();
            else
                amountInputField.text = amount.ToString();

            if (amount > playerInspecting.resourcesAmountForTrade[tradeOfferSelectionID])
                amount = playerInspecting.resourcesAmountForTrade[tradeOfferSelectionID];

            costTxt.text = (playerInspecting.resourcesCostForTrade[tradeOfferSelectionID] * amount).ToString();

            acceptButton.interactable = (amount > 0);

            cost = amount * playerInspecting.resourcesCostForTrade[tradeOfferSelectionID];

            if (cost > PlayerResources.Money) {
                float newCost = PlayerResources.Money;
                float newAmount = amount * newCost / cost;
                cost = newCost;
                amount = (int)newAmount;
                amountInputField.text = amount.ToString();
                costTxt.text = (playerInspecting.resourcesCostForTrade[tradeOfferSelectionID] * amount).ToString();
            }
        }

        public void AcceptOffer() {
            Player.LocalPlayer.CmdTradeWithPlayer(Player.LocalPlayer.PlayerID, playerInspecting.PlayerID, tradeOfferSelectionID, amount);
            TradeOffer tradeOffer = new TradeOffer(PlayerList.Instance.Players[playerInspecting.PlayerID], tradeOfferSelectionID, amount, cost);
            MarketPlace.OnTradeOfferBought(tradeOffer);
            tradeInfoObject.gameObject.SetActive(false);
            Init(cityObject, playerInspecting);
        }
        
        private void OnDestroy() {
            CityObject.OnSelected -= Init;
        }
    }
}