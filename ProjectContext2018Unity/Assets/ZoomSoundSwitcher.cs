using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomSoundSwitcher : MonoBehaviour {
    private SwitchView view;
    private AudioSource source;

    public AudioClip zoomOutSound;
    public AudioClip zoomInSound;
	// Use this for initialization
	void Start () {
        view = GetComponent<SwitchView>();
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToggleSound() {
        if(view.earthActive) {
            source.PlayOneShot(zoomOutSound);
        }
        else {
            source.PlayOneShot(zoomInSound);
        }
    }
}
