using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchView : MonoBehaviour {

    public GameObject cityView, earthView;

    public void ToggleView() {
        earthView.SetActive(!earthView.activeInHierarchy);
        cityView.SetActive(!cityView.activeInHierarchy);
    }
}
