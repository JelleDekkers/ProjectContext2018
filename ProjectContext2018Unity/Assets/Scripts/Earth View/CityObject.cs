using System;
using System.Collections.Generic;
using UnityEngine;

namespace EarthView {

    public class CityObject : MonoBehaviour {

        public static Action<CityObject> OnSelected;

        public Player Player { get; private set; }

        public void Init(Player player) {
            Player = player;
        }
    }
}