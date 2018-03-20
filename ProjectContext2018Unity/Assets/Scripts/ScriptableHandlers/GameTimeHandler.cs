using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeHandler : MonoBehaviour {

    [SerializeField] private GameTime gameTime;
    public GameTime GameTime { get { return GameTime; } }

    public void Start() {
        gameTime.Init();
    }

    public void Update() {
        gameTime.Update();
    }
}
