using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CityView.UI {

    public class ProductionStoppedWidgetItem : MonoBehaviour {

        [SerializeField] private Image productImg;

        public void Init(Sprite productSprite) {
            productImg.sprite = productSprite;
        }
    }
}
