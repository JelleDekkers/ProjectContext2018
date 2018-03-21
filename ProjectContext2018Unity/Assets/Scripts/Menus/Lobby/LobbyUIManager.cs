using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LobbyUIManager : MonoBehaviour {

    [SerializeField] private Text infoTxt;
    [SerializeField] private string serverInfoTxt, clientInfoTxt;

    private void Start() {
        if (NetworkManager.singleton.numPlayers == 0)
            infoTxt.text = serverInfoTxt;
        else
            infoTxt.text = clientInfoTxt;
    }
}
