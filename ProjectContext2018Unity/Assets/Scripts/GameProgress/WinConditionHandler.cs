using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameProgress {

    public class WinConditionHandler : MonoBehaviour {

        public float checkTimeInSeconds = 5;
        private float timer = 0;

        public void Start() {
            StartCoroutine(StartBuffer());
        }

        private IEnumerator StartBuffer() {
            float time = 2f;
            while (time > 0) {
                time -= Time.deltaTime;
                yield return null;
            }
            Player.LocalPlayer.OnUpdatePollutionPerMinuteChanged += CheckWinCondition;
        }

        //private void Update() {
        //    if (timer > checkTimeInSeconds) {
        //        CheckWinCondition();
        //        timer = 0;
        //    }
        //    timer += Time.deltaTime;
        //}

        private void CheckWinCondition() {
          foreach(Player player in Player.LocalPlayer.PlayerList.Players) {
                if (player.PlayerPollutionPerYear > 0)
                    return;
            }
            WinConditionMet();
        }

        private void WinConditionMet() {
            Debug.Log("GAME WON");
            GameOverManager.SetGameOverState(GameOverManager.GameOverState.GameWon);
            Player.LocalPlayer.CmdLoadGameOverLobby();
        }

        private void OnDestroy() {
            Player.LocalPlayer.OnUpdatePollutionPerMinuteChanged -= CheckWinCondition;
        }
    }
}
