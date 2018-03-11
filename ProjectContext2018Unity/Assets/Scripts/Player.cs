using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private static Player instance;
    public static Player Instance { get { return instance; } }

    [SerializeField] private new string name;
    public string Name { get { return name; } }

    [SerializeField] PlayerResourcesHandler resourcesHandler;
    public PlayerResourcesHandler ResourcesHandler { get { return resourcesHandler; } }

    private void Awake() {
        instance = this;
    }
}
