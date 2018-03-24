using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameProgress {
    public class LoseConditionHandler : MonoBehaviour {

        private void Start() {
            WorldTemperature.OnWorldTemperatureMaxReached += GameOverByTemperature;
            GameTime.OnMaxYearReached += GameOverByTime;
        }

        private void GameOverByTemperature() {
            Debug.Log("GAME LOST");
            GameOverManager.SetGameOverState(GameOverManager.GameOverState.GameLostByTemperature);
            Player.LocalPlayer.LoadGameOverLobby();
        }

        private void GameOverByTime() {
            Debug.Log("GAME LOST");
            GameOverManager.SetGameOverState(GameOverManager.GameOverState.GameLostByTime);
            Player.LocalPlayer.LoadGameOverLobby();
        }

        private void OnDestroy() {
            WorldTemperature.OnWorldTemperatureMaxReached -= GameOverByTemperature;
            GameTime.OnMaxYearReached -= GameOverByTime;
        }
    }
}
