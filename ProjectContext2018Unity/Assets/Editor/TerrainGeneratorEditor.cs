using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CityView.Terrain.Generator.EditorTools {

    [CustomEditor(typeof(GameTerrain))]
    public class TerrainGeneratorInspector : Editor {

        private GameTerrain generator;

        private void OnEnable() {
            generator = (GameTerrain)target;
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            if (GUILayout.Button("Generate Terrain")) 
                generator.GenerateTerrain();

            if (GUILayout.Button("Generate Ocean"))
                generator.GenerateOcean();

            if (GUILayout.Button("Remove All"))
                generator.Clear();
        }
    }

}