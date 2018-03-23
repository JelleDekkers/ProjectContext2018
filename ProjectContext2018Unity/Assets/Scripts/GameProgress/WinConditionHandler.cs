using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameProgress {

    public class WinConditionHandler : MonoBehaviour {

        public float checkTimeInSeconds = 5;
        private float timer = 0;

        public void Start() {
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
            Player.LocalPlayer.CmdLoadGameOverLobby();
        }

        private void OnDestroy() {
            Player.LocalPlayer.OnUpdatePollutionPerMinuteChanged -= CheckWinCondition;
        }
    }
}
