using System;
using UnityEngine;

public class SwitchView : MonoBehaviour {
    public bool earthActive;
    public static Action OnSceneViewSwitched;

    public GameObject cityView, earthView;

    public void ToggleView() {
        earthView.SetActive(!earthView.activeInHierarchy);
        cityView.SetActive(!cityView.activeInHierarchy);
        if(earthView.activeInHierarchy) {
            earthActive = true;
        }
        else {
            earthActive = false;
        }

        //if (OnSceneViewSwitched != null)
        //    OnSceneViewSwitched();
    }
}
