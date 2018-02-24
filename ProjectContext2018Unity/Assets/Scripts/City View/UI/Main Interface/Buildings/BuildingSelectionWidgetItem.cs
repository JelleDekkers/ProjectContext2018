using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace CityView.UI {

    public class BuildingSelectionWidgetItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

        [SerializeField]
        private Image img;

        private int id;

        public static Action<int> OnPointerEnterEvent;
        public static Action OnPointerExitEvent;

        public void Init(int id) {
            this.id = id;
            img.sprite = DataManager.BuildingPrefabs.GetSprite(id);
            Button button = GetComponent<Button>();
            button.onClick.AddListener(() => BuildingSelectionWidget.OnBuildingSelected(id));
        }

        public void OnPointerEnter(PointerEventData eventData) {
            OnPointerEnterEvent(id);
        }

        public void OnPointerExit(PointerEventData eventData) {
            OnPointerExitEvent();
        }
    }
}
