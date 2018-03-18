using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalPlayerCard : PlayerCard {

    [SerializeField] private InputField nameInputField;

    public void UpdateName() {
        //player.CmdUpdateName(nameInputField.text);
    }
}
