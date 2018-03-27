using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    public TutorialItem[] items;
    public bool playTutorial;

    private TutorialItem currenItem;
    private int index = 0;

    private void Start() {
        items = GetComponentsInChildren<TutorialItem>(true);
        currenItem = items[0];
        currenItem.gameObject.SetActive(true);
    }

    public void StopTutorial() {
        gameObject.SetActive(false);
    }

    public void NextTutorialItem() {
        if (index < items.Length - 1) {
            currenItem.gameObject.SetActive(false);
            index++;
            currenItem = items[index];
            currenItem.gameObject.SetActive(true); 
        } else {
            StopTutorial();
        }
    }
}
