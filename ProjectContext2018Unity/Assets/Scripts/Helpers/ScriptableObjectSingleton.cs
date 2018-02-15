using System.Linq;
using UnityEngine;

/// <summary>
/// Abstract class for making reload-proof singletons out of ScriptableObjects
/// Returns the asset created on the editor, or null if there is none
/// </summary>
/// <typeparam name="T">Singleton type</typeparam>
public abstract class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObject {

    private static T instance = null;
    public static T Instance {
        get {
            if (!instance) {
                instance = Resources.LoadAll<T>("").First();
                //instance = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();
            }
            return instance;
        }
    }
}