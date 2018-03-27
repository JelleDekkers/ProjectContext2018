using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialItem : MonoBehaviour {

    public UnityEvent onItemReached;

    public void Start() {
        onItemReached.Invoke();
    }
}
