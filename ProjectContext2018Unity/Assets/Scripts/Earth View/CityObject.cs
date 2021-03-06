﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace EarthView {

    public class CityObject : MonoBehaviour {

        public static Action<CityObject, Player> OnSelected;

        public Player Player { get; private set; }
        public GameObject localPlayerCircle;

        public void Init(Player player) {
            Player = player;

            localPlayerCircle.SetActive(player == Player.LocalPlayer);
        }

        public void Select() {
            if (OnSelected != null)
                OnSelected(this, Player);
        }

        public void SetObjectActive(bool active) {
            transform.parent.gameObject.SetActive(active);
        }
    }
}