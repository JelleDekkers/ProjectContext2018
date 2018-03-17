using System;
using System.Collections;
using UnityEngine;

namespace CityView {

    public class House : Building {

        public static Action<Building, int> OnHousePaused;
        public static Action<Building, int> OnNewInhabitant;

        public int inhabitants;
        public int maxInhabitantsPerHouse = 12;

        [SerializeField] private float newInhabitantTime = 3;

        private Coroutine newInhabitantCoroutine;

        protected override void OnEnable() {
            base.OnEnable();
            if (inhabitants < maxInhabitantsPerHouse) 
                newInhabitantCoroutine = StartCoroutine(NewInhabitantMovingIn());
        }

        protected override void OnDisable() {
            base.OnDisable();

            if (newInhabitantCoroutine != null)
                StopCoroutine(newInhabitantCoroutine);

            if(OnHousePaused != null)
                OnHousePaused(this, inhabitants);

            inhabitants = 0;
        }

        private void AddNewInhabitant() {
            inhabitants++;
            OnNewInhabitant(this, 1);

            if (inhabitants < maxInhabitantsPerHouse)
                newInhabitantCoroutine = StartCoroutine(NewInhabitantMovingIn());
        }

        private IEnumerator NewInhabitantMovingIn() {
            WaitForSeconds wait = new WaitForSeconds(newInhabitantTime);
            yield return wait;

            if (enabled)
                AddNewInhabitant();
        }
    }
}