using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameProgress {
    public class LoseConditionHandler : MonoBehaviour {

        private void Start() {
            WorldTemperature.OnWorldTemperatureMaxReached += GameOver;
            GameTime.OnMaxYearReached += GameOver;
        }

        private void GameOver() {
            Debug.Log("GAME LOST");
            Player.LocalPlayer.LoadGameOverLobby();
            WorldTemperature.OnWorldTemperatureMaxReached -= GameOver;
            GameTime.OnMaxYearReached -= GameOver;
        }
    }
}
