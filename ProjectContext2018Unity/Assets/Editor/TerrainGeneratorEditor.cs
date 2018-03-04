using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CityView.Terrain.Generator.EditorTools {

    [CustomEditor(typeof(TerrainGenerator))]
    public class TerrainGeneratorInspector : Editor {

        private TerrainGenerator generator;

        private void OnEnable() {
            generator = (TerrainGenerator)target;
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