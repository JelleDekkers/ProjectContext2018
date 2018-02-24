using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Extensions {
    
    /// <summary>
    /// Removes all child transforms
    /// </summary>
    /// <param name="t"></param>
    public static void RemoveChildren(this Transform t) {
        foreach(Transform child in t.transform) {
            Object.Destroy(child.gameObject);
        }
    }
}
