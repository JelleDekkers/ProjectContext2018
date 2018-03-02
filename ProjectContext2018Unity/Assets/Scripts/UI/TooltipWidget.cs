using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CityView.UI {

    public class TooltipWidget : MonoBehaviour {

        private static TooltipWidget instance;
        public static TooltipWidget Instance {
            get {
                if (instance == null)
                    instance = FindObjectOfType<TooltipWidget>();
                return instance;
            }
        }
       
        [SerializeField]
        private Text tooltipText;

        private RectTransform rect;

        void Awake() {
            instance = this;
            rect = GetComponent<RectTransform>();
            Hide();
        }

        public void Show(string text, Vector3 pos, Vector3 offset) {
            tooltipText.text = text;
            rect.position = pos + offset;
            gameObject.SetActive(true);
        }

        public void Hide() {
            gameObject.SetActive(false);
        }  
    }
}