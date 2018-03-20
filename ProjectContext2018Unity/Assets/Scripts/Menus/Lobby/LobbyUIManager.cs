using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LobbyUIManager : MonoBehaviour {

    [SerializeField] private Text infoTxt;
    [SerializeField] private string serverInfoTxt, clientInfoTxt;

    private void Awake() {
        if (NetworkManager.singleton.numPlayers == 1)
            infoTxt.text = serverInfoTxt;
        else
            infoTxt.text = clientInfoTxt;
    }
}
