using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {
    public AudioClip menuClip;
    public AudioClip gameClip;

    private AudioSource source;
	// Use this for initialization
	private void Start () {
        source = GetComponent<AudioSource>();
        DontDestroyOnLoad(this);
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }
	
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if(scene.buildIndex == 0 || scene.buildIndex == 1) {
            if (source.clip != menuClip) {
                source.clip = menuClip;
                source.Play();
            }
        }
        else {
            if (source.clip != gameClip) {
                source.clip = gameClip;
                source.Play();
            }
        }

    }
    
}
