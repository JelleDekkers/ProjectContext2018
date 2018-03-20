using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClimateInfo : MonoBehaviour {

    [SerializeField] private Text text;

    private void Start() {
        text.text = Player.LocalPlayer.ClimateType.ToString();
    }
}
