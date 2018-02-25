using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerResources))]
public class PlayerResourcesEditor : Editor {

    PlayerResources obj;

    private void OnEnable() {
        obj = (PlayerResources)target;
    }

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        GUILayout.Label("Update Resources Type. Resets all amount values to 0.");
        if (GUILayout.Button("Update Resource Types")) 
            obj.SetResources();
    }
}
