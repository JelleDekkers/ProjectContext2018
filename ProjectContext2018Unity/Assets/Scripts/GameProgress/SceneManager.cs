using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class SceneManager {
    public const int GameScene = 2;
    public const int GameOverLobby = 3;

    public static void LoadScene(int index, LoadSceneMode loadSceneMode = LoadSceneMode.Single) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index, loadSceneMode);
    }
}

