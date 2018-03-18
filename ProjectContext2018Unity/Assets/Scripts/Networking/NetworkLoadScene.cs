using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkLoadScene : NetworkBehaviour {

    public SceneAsset sceneAsset;

    public void LoadSceneAsset() {
        NetworkManager.singleton.ServerChangeScene(sceneAsset);
    }

    public void LoadScene(string name) {
        NetworkManager.singleton.ServerChangeScene(name);
    }

    public static void LoadSceneStatic(string name) {
        NetworkManager.singleton.ServerChangeScene(name);
    }
}
