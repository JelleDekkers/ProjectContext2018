using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EarthView.UI {

    public class PlayerInfoWidget : MonoBehaviour {

        [SerializeField] private CityObjectsManager cityManager;
        [SerializeField] private Text nameTxt, climateTxt;
        [SerializeField] private Text pollutionPerMinuteTxt;
        [SerializeField] private GridLayoutGroup tradeOffers;
        [SerializeField] private TradeOfferWidget tradeOfferWidgetPrefab;
        [SerializeField] private InputField amountInputField;
        [SerializeField] private Text costTxt;
        [SerializeField] private GameObject tradeInfoObject;
        [SerializeField] private Button acceptButton;
        [SerializeField] private Image resourceImg;
        [SerializeField] private Button setTradeOffersBtn;

        private Player playerInspecting;
        private CityObject cityObject;
        private int tradeOfferSelectionID;
        private int amount = 0;
        private float cost;
        private TradeOfferWidget[] tradeOfferWidgets;

        private void Awake() {
            CityObject.OnSelected += Init;
            gameObject.SetActive(false);
        }

        public void Init(CityObject city, Player player) {
            playerInspecting = player;
            cityObject = city;

            nameTxt.text = player.Name;
            if (player == Player.LocalPlayer)
                nameTxt.text += "(You)";
            climateTxt.text = playerInspecting.ClimateType.ToString();
            setTradeOffersBtn.gameObject.SetActive(player == Player.LocalPlayer);
            gameObject.SetActive(true);
            pollutionPerMinuteTxt.text = string.Format("{0:0}", player.PlayerPollutionPerYear);
            InstantiateTradeOffers();
        }

        public void CycleToNextPlayer(int direction) {
            int next = playerInspecting.PlayerID + direction;
            if (next >= PlayerList.Instance.Players.Count)
                next = 0;
            else if (next < 0)
                next = PlayerList.Instance.Players.Count - 1;

            Init(cityManager.cityObjects[next], PlayerList.Instance.Players[next]);
        }

        private void InstantiateTradeOffers() {
            tradeOffers.transform.RemoveChildren();
            tradeOfferWidgets = new TradeOfferWidget[playerInspecting.resourcesAmountForTrade.Count];
            // start at 1 to skip Energy
            for(int i = 1; i < playerInspecting.resourcesAmountForTrade.Count; i++) {
                tradeOfferWidgets[i] = Instantiate(tradeOfferWidgetPrefab, tradeOffers.transform);
                tradeOfferWidgets[i].Init(this, playerInspecting, i);
            }
        }

        public void UpdateTradeOfferSelection(int newID) {
            tradeOfferSelectionID = newID;
            amountInputField.text = (0).ToString();
            resourceImg.sprite = DataManager.ResourcePrefabs.GetResourceSprite(newID);
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
            tradeOfferWidgets[tradeOfferSelectionID].SetAmount(playerInspecting.resourcesAmountForTrade[tradeOfferSelectionID] - amount);
            Player.LocalPlayer.CmdTradeWithPlayer(Player.LocalPlayer.PlayerID, playerInspecting.PlayerID, tradeOfferSelectionID, amount);
            TradeOffer tradeOffer = new TradeOffer(PlayerList.Instance.Players[playerInspecting.PlayerID], tradeOfferSelectionID, amount, cost);
            MarketPlace.OnTradeOfferBought(tradeOffer);
            tradeInfoObject.gameObject.SetActive(false);
            PlayerResources.Instance.RemoveMoney(tradeOffer.totalValue);
            //Init(cityObject, playerInspecting);
        }

        private void OnDestroy() {
            CityObject.OnSelected -= Init;
        }
    }
}