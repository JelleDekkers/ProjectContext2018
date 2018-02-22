using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CityView.UI {

    public class ResourcesWidgetItem : MonoBehaviour {

        [SerializeField]
        private int resourceID;
        [SerializeField]
        private Image img;
        [SerializeField]
        private Text amountText;

        private void Start() {
            img.sprite = DataManager.ResourcePrefabs.GetSprite(resourceID);
            string resourceName = DataManager.ResourcesData.dataArray[resourceID].Name;
            float amount = PlayerResources.Resources[resourceName];
            amountText.text = Mathf.RoundToInt(amount).ToString();
            PlayerResources.OnResourceChanged += UpdateAmount;
        }

        private void UpdateAmount(int id, float newAmount) {
            if(id == resourceID)
                amountText.text = Mathf.RoundToInt(newAmount).ToString(); 
        }
    }
}
