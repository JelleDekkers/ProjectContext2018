using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameProgress {

    public class WinConditionHandler : MonoBehaviour {

        private void CheckWinCondition() {
            if (/*WIN CONDITIONS MET*/true) {
                Debug.Log("GAME WON");
                // IMPLEMENT WIN GAME METHOD HERE
                SceneManager.LoadScene(SceneManager.GameOverLobby);
            }
        }
    }
}
