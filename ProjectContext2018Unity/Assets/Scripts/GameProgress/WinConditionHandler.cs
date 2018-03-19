using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameProgress {
    public class WinConditionHandler : MonoBehaviour {

        // Use this for initialization
        private void Start() {
        }

        // Update is called once per frame
        private void Update() {

        }

        private void CheckWinCondition() {
            if (/*WIN CONDITIONS MET*/true) {
                Debug.Log("GAME WON");
                // IMPLEMENT WIN GAME METHOD HERE
                SceneManager.LoadScene(SceneManager.GameOverLobby);
            }
        }
    }
}
