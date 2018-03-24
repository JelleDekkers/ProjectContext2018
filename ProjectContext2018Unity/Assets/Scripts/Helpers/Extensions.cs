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

    public static bool Contains<T>(this T[] array, T value) {
        foreach(T t in array) {
            if (t.Equals(value))
                return true;
        }
        return false;
    }

    public static T[] RemoveFirstElement<T>(this T[] array) {
        T[] finalArray = new T[array.Length - 1];
        for(int i = 1; i < array.Length; i++) {
            finalArray[i - 1] = array[i];
        }
        return finalArray;
    }
}
