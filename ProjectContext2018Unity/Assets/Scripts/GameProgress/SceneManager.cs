using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameProgress {
    public static class SceneManager {
        public const int GameScene = 0;
        public const int LoseScene = 1;
        public const int WinScene = 2;

        public const LoadSceneMode AdditiveLoad = LoadSceneMode.Additive;
        public const LoadSceneMode SingleLoad = LoadSceneMode.Single;

        public static void LoadScene(int index, LoadSceneMode loadSceneMode = LoadSceneMode.Single) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(index, loadSceneMode);
        }
    }
}
