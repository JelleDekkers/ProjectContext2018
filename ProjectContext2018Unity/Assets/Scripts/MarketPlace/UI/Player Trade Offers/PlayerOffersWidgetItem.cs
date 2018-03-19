using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class PlayerOffersWidgetItem : MonoBehaviour {

        [SerializeField] private Image productImg;
        [SerializeField] private InputField amountInputField, valueInputField;

        private int indexID;
        private int amountOnInit;

        public void Init(int indexID) {
            this.indexID = indexID;
            productImg.sprite = DataManager.ResourcePrefabs.GetResourceSprite(indexID);
            amountOnInit = Player.LocalPlayer.resourcesAmountForTrade[indexID];
            amountInputField.text = amountOnInit.ToString();
            valueInputField.text = Player.LocalPlayer.resourcesCostForTrade[indexID].ToString();
        }

        public void OnAmountInputFieldChange() {
            int difference = int.Parse(amountInputField.text) - amountOnInit;
            PlayerResourcesHandler.Instance.UpdateResource(indexID, difference);
        }

        public void UpdatePlayerTradeOffers() {
            amountInputField.text = Mathf.Clamp(int.Parse(amountInputField.text), 0, PlayerResources.Instance.GetResource(indexID)).ToString();
            Player.LocalPlayer.CmdSendTradeOffer(indexID, int.Parse(valueInputField.text), int.Parse(amountInputField.text));
        }
    }
}