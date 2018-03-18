using System;
using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;
#endif


[System.Serializable]
public class SceneAsset {
    [SerializeField] private Object sceneAsset;
    [SerializeField] private string sceneName = "";

    public string SceneName {
        get { return sceneName; }
    }

    // makes it work with the existing Unity methods (LoadLevel/LoadScene)
    public static implicit operator string(SceneAsset sceneField) {
        return sceneField.SceneName;
    }
}
