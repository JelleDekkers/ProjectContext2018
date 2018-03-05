using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnEnabledEvent : MonoBehaviour {

    public UnityEvent OnEnabled, OnDisabled;

    private void OnEnable() {
        OnEnabled.Invoke();
    }

    private void OnDisable() {
        OnDisabled.Invoke();
    }
}
