using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    class BuildingScriptable : ScriptableObject {
        public GameObject prefab;
        public Sprite mask;

        [MenuItem("Assets/Create/Building")]
        public static void CreateMyAsset() {
            BuildingScriptable asset = ScriptableObject.CreateInstance<BuildingScriptable>();

            AssetDatabase.CreateAsset(asset, "Assets/ScriptableObjects/NewBuilding.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }
    }
}