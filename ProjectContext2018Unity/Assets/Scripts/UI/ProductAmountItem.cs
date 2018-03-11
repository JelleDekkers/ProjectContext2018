using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class ProductAmountItem : MonoBehaviour {

        [SerializeField] private Image productImg;
        [SerializeField] private Text amountTxt;

        public void Init(Sprite productSprite, float amount) {
            productImg.sprite = productSprite;
            amountTxt.text = Mathf.RoundToInt(amount).ToString();
        }

        public void SetMaterial(Material mat) {
            productImg.material = mat;
            amountTxt.material = mat;
        }
    }
}