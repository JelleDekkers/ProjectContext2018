using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class PlayerOffersWidgetItem : MonoBehaviour {

        public static Action<int, int> OnOfferChanged, OnOfferRemoved;

        [SerializeField] private Image productImg;
        [SerializeField] private InputField amountInputField, valueInputField;
        [SerializeField] private Button addBtn, removeBtn;

        private int indexID;
        private int amountBeforeChange;

        public void Init(int indexID) {
            this.indexID = indexID;
            productImg.sprite = DataManager.ResourcePrefabs.GetResourceSprite(indexID);
            amountBeforeChange = Player.LocalPlayer.resourcesAmountForTrade[indexID];
            amountInputField.text = amountBeforeChange.ToString();
            valueInputField.text = Player.LocalPlayer.resourcesCostForTrade[indexID].ToString();
            UpdateRemoveButtonInteractableState();
            UpdateAddButtonInteractableState();
        }

        public void OnAmountInputFieldChange() {
            amountInputField.text = Mathf.Clamp(int.Parse(amountInputField.text), 0, PlayerResources.Instance.GetResourceAmount(indexID)).ToString();
        }

        public void UpdateAddButtonInteractableState() {
            if (Player.LocalPlayer.resourcesAmountForTrade[indexID] > 0) {
                addBtn.interactable = false;
                return;
            }
            addBtn.interactable = (int.Parse(amountInputField.text) > 0 && int.Parse(valueInputField.text) > 0);
        }

        public void UpdateRemoveButtonInteractableState() {
            removeBtn.interactable = (Player.LocalPlayer.resourcesAmountForTrade[indexID] > 0);
        }

        public void UpdateOffer() {
            if (int.Parse(amountInputField.text) > 0 && int.Parse(valueInputField.text) > 0) {
                int difference = Player.LocalPlayer.resourcesAmountForTrade[indexID] - int.Parse(amountInputField.text);
                PlayerResourcesHandler.Instance.UpdateResource(indexID, difference);
                if (OnOfferChanged != null)
                    OnOfferChanged(indexID, difference);

                Player.LocalPlayer.CmdSendTradeOffer(indexID, int.Parse(valueInputField.text), int.Parse(amountInputField.text));
            }
            UpdateAddButtonInteractableState();
        }

        public void RemoveOffer() {
            amountInputField.text = 0.ToString();
            valueInputField.text = 0.ToString();
            int amount = Player.LocalPlayer.resourcesAmountForTrade[indexID];
            if (OnOfferRemoved != null)
                OnOfferRemoved(indexID, amount);
            PlayerResourcesHandler.Instance.UpdateResource(indexID, amount);
            Player.LocalPlayer.CmdSendTradeOffer(indexID, 0, 0);
        }
    }
}