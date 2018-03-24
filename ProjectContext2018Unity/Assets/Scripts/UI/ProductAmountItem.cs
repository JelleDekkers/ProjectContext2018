using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class ProductAmountItem : MonoBehaviour {

        [SerializeField] private Image productImg;
        [SerializeField] private Text amountTxt;
        [SerializeField] private Color notEnoughResourcesColor;

        private Color normalColor;
        private int amountNeeded;
        private int resourceID;

        public void Init(Sprite productSprite, float amount) {
            amountNeeded = (int)amount;
            productImg.sprite = productSprite;
            amountTxt.text = Mathf.RoundToInt(amount).ToString();
        }

        public void Init(Sprite productSprite, float amount, int resourceID) {
            normalColor = amountTxt.color;
            Init(productSprite, amount);
            this.resourceID = resourceID;
            UpdateColorResource(resourceID, PlayerResources.Instance.GetResourceAmount(resourceID));
            PlayerResources.OnResourceChanged += UpdateColorResource;
        }

        public void Init(Sprite productSprite, float amount, bool updateMoneyState) {
            normalColor = amountTxt.color;
            Init(productSprite, amount);
            UpdateColorMoney(PlayerResources.Money);
            PlayerResources.OnMoneyChanged += UpdateColorMoney;
        }

        private void UpdateColorMoney(float newAmount) {
            if (PlayerResources.Instance.HasMoneyAmount(amountNeeded))
                amountTxt.color = normalColor;
            else
                amountTxt.color = notEnoughResourcesColor;
        }

        private void UpdateColorResource(int resourceID, int newAmount) {
            if (resourceID == this.resourceID) {
                if (PlayerResources.Instance.HasResourceAmount(this.resourceID, amountNeeded)) 
                    amountTxt.color = normalColor;
                else
                    amountTxt.color = notEnoughResourcesColor;
            }
        }

        public void SetMaterial(Material mat) {
            productImg.material = mat;
            amountTxt.material = mat;
        }

        public void SetAmount(float newAmount) {
            amountTxt.text = newAmount.ToString();
        }

        private void OnDestroy() {
            PlayerResources.OnMoneyChanged -= UpdateColorMoney;
            PlayerResources.OnResourceChanged -= UpdateColorResource;
        }
    }
}