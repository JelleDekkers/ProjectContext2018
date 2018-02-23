using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CityView.UI {

    public class PlacedBuildingSelectionResourceGridItem : MonoBehaviour {

        [SerializeField] private Image productImg;
        [SerializeField] private Text amountTxt;

        public void Init(Sprite productSprite, float amount) {
            productImg.sprite = productSprite;
            amountTxt.text = Mathf.RoundToInt(amount).ToString();
        }
    }
}