using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour {

    public enum GameOverState {
        GameLostByTemperature,
        GameLostByTime,
        GameWon
    }

    public static GameOverState state;

    [SerializeField] private SceneAsset menu;
    [SerializeField] private WorldTemperature worldTemperature;
    [SerializeField] private GameTime gameTime;
    [SerializeField] private Text gameOverDescriptionTxt, gameOverTitleTxt;
    [SerializeField] private string gameWonTitle, gameLostTitle;
    [SerializeField] private string temperatureThresholdReachedDescription, timeThresholdReachedDescription, sustainableConditionMetDescription;
    [SerializeField] private Transform playerList;
    [SerializeField] private UI.PlayerInfoItem infoItemPrefab;

    private void Start() {
        FillPlayerList();
        if (state == GameOverState.GameLostByTemperature) {
            gameOverDescriptionTxt.text = temperatureThresholdReachedDescription;
            gameOverTitleTxt.text = gameLostTitle;
        } else if (state == GameOverState.GameLostByTime) {
            gameOverDescriptionTxt.text = timeThresholdReachedDescription;
            gameOverTitleTxt.text = gameLostTitle;
        } else {
            gameOverDescriptionTxt.text = sustainableConditionMetDescription;
            gameOverTitleTxt.text = gameWonTitle;
        }
    }

    private void FillPlayerList() {
        for (int i = 0; i < PlayerList.Instance.Players.Count; i++) {
            Instantiate(infoItemPrefab, playerList).Init(i + 1, PlayerList.Instance.Players[i]);
        }
    }

    public static void SetGameOverState(GameOverState gameOverState) {
        state = gameOverState;
    }

    public void QuitFromServer() {
        Player.LocalPlayer.DisconnectFromNetwork();
        SceneManager.LoadScene(menu);
    }
}
