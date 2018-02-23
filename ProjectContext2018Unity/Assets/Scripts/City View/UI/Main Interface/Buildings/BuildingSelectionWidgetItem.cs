using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CityView.UI {

    public class BuildingSelectionWidgetItem : MonoBehaviour {

        [SerializeField]
        private Image img;

        private int id;

        public void Init(int id) {
            img.sprite = DataManager.BuildingPrefabs.GetSprite(id);
            Button button = GetComponent<Button>();
            button.onClick.AddListener(() => BuildingSelectionWidget.OnBuildingSelected(id));
        }
    }
}
