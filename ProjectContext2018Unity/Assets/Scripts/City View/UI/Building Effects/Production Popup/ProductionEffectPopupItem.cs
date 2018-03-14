using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CityView.UI {

    public class ProductionEffectPopupItem : MonoBehaviour {

        [SerializeField] private Image productImg;
        [SerializeField] private Text amountTxt;

        public void Init(Sprite productSprite, float amount) {
            productImg.sprite = productSprite;
            //amountTxt.text = (amount > 0) ? "+" : "-";
            amountTxt.text = Mathf.RoundToInt(amount).ToString();
        }
    }
}