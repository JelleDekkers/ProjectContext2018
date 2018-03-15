using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CityView.UI {

    public class ProductionEffectPopupItem : MonoBehaviour {

        [SerializeField] private Image productImg;
        [SerializeField] private Text amountTxt;
        [SerializeField] private Color plusColor = Color.green;
        [SerializeField] private Color minColor = Color.red;

        public void Init(Sprite productSprite, float amount) {
            productImg.sprite = productSprite;
            //amountTxt.text = (amount > 0) ? "+" : "-";
            amountTxt.text = Mathf.RoundToInt(amount).ToString();

            if (amount >= 0)
                amountTxt.text = "+" + amountTxt.text;

            if (amount >= 0)
                amountTxt.color = plusColor;
            else
                amountTxt.color = minColor;
        }
    }
}