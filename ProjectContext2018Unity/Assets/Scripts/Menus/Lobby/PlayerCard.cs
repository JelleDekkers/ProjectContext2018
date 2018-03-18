using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCard : MonoBehaviour {

    public Player player;
    public new Text name;

    public void Init(Player player) {
        this.player = player;
    }

    public void Update() {
        name.text = player.Name;
    }
}
