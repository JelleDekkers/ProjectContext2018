using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EarthView.UI {

    public class TradeOfferWidget : MonoBehaviour {

        [SerializeField] private Image img;
        [SerializeField] private Text amount, cost;
        [SerializeField] private Button button;

        private PlayerInfoWidget infoWidget;
        private Player player;
        private int id;
        
        public void Init(PlayerInfoWidget infoWidget, Player player, int id) {
            this.infoWidget = infoWidget;
            this.player = player;
            this.id = id;

            img.sprite = DataManager.ResourcePrefabs.GetResourceSprite(id);
            amount.text = player.resourcesAmountForTrade[id].ToString() + "x";
            cost.text = player.resourcesCostForTrade[id].ToString();
            button.interactable = (player != Player.LocalPlayer);
        }

        public void UpdateSelection() {
            infoWidget.UpdateTradeOfferSelection(id);
        }

        public void SetAmount(float newAmount) {
            amount.text = newAmount.ToString();
        }
    }
}