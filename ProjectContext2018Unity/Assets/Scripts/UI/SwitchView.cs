using System;
using UnityEngine;

public class SwitchView : MonoBehaviour {

    public static Action OnSceneViewSwitched;

    public GameObject cityView, earthView;

    public void ToggleView() {
        earthView.SetActive(!earthView.activeInHierarchy);
        cityView.SetActive(!cityView.activeInHierarchy);

        //if (OnSceneViewSwitched != null)
        //    OnSceneViewSwitched();
    }
}
