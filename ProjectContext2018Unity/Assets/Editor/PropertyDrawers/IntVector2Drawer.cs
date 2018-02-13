using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(IntVector2))]
public class IntVector2Drawer : PropertyDrawer {

    private SerializedProperty x, z;
    private string name;
    private bool cached;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        if (!cached) { 
            name = property.displayName;
            property.Next(true);
            x = property.Copy();
            property.Next(true);
            z = property.Copy();
            cached = true;
        }

        Rect contentPosition = EditorGUI.PrefixLabel(position, new GUIContent(name));

        //show the X and Y from the point
        EditorGUIUtility.labelWidth = 14f;
        contentPosition.width *= 0.5f;
        //EditorGUI.indentLevel = 1;

        // Begin/end property & change check make each field
        // behave correctly when multi-object editing.
        EditorGUI.BeginProperty(contentPosition, label, x);
        {
            EditorGUI.BeginChangeCheck();
            int newVal = EditorGUI.IntField(contentPosition, new GUIContent("X"), x.intValue);
            if (EditorGUI.EndChangeCheck())
                x.intValue = newVal;
        }
        EditorGUI.EndProperty();

        contentPosition.x += contentPosition.width;

        EditorGUI.BeginProperty(contentPosition, label, z);
        {
            EditorGUI.BeginChangeCheck();
            int newVal = EditorGUI.IntField(contentPosition, new GUIContent("Y"), z.intValue);
            if (EditorGUI.EndChangeCheck())
                z.intValue = newVal;
        }
        //EditorGUI.indentLevel = 0;
        EditorGUI.EndProperty();
    }
}