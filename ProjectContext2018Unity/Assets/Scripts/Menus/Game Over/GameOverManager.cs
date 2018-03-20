using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class GameOverManager : MonoBehaviour {

        [SerializeField] private SceneAsset menu;
        [SerializeField] private WorldTemperature worldTemperature;
        [SerializeField] private Text gameOverDescriptionTxt;
        [SerializeField] private string temperatureThresholdReachedDescription, timeThresholdReachedDescription, sustainableConditionMetDescription;
        [SerializeField] private Transform playerList;
        [SerializeField] private PlayerInfoItem infoItemPrefab;

        private void Start() {
            FillPlayerList();
            // zelf via code invoeren, ook voor als er te veel dagen voorbij zijn
            if (worldTemperature.IsWorldTemperatureThresholdReached())
                gameOverDescriptionTxt.text = temperatureThresholdReachedDescription;
            else
                gameOverDescriptionTxt.text = sustainableConditionMetDescription;
            // else if days 
        }

        private void FillPlayerList() {
            for (int i = 0; i < PlayerList.Instance.Players.Count; i++) {
                Instantiate(infoItemPrefab, playerList).Init(i + 1, PlayerList.Instance.Players[i]);
            }
        }

        public void QuitFromServer() {
            Player.LocalPlayer.DisconnectFromNetwork();
            SceneManager.LoadScene(menu);
        }
    }
}