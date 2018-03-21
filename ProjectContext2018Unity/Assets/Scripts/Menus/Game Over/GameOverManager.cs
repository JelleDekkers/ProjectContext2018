using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class GameOverManager : MonoBehaviour {

        [SerializeField] private SceneAsset menu;
        [SerializeField] private WorldTemperature worldTemperature;
        [SerializeField] private GameTime gameTime;
        [SerializeField] private Text gameOverDescriptionTxt;
        [SerializeField] private string temperatureThresholdReachedDescription, timeThresholdReachedDescription, sustainableConditionMetDescription;
        [SerializeField] private Transform playerList;
        [SerializeField] private PlayerInfoItem infoItemPrefab;

        private void Start() {
            FillPlayerList();
            if (worldTemperature.IsWorldTemperatureThresholdReached())
                gameOverDescriptionTxt.text = temperatureThresholdReachedDescription;
            else if (gameTime.IsMaxYearReached())
                gameOverDescriptionTxt.text = timeThresholdReachedDescription;
            else
                gameOverDescriptionTxt.text = sustainableConditionMetDescription;
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