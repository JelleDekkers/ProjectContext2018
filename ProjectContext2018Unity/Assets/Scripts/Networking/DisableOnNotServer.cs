using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DisableOnNotServer: MonoBehaviour {

    private void Start() {
        gameObject.SetActive(NetworkManager.singleton.numPlayers == 1);
    }
}
