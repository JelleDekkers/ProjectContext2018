using System;
using UnityEngine;

namespace CityView {

    public class House : Building {

        public static Action OnHouseHabited, OnHouseUninhabitated;

        protected override void OnEnable() {
            base.OnEnable();
            OnHouseHabited();
        }

        protected override void OnDisable() {
            base.OnDisable();
            OnHouseUninhabitated();
        }
    }
}