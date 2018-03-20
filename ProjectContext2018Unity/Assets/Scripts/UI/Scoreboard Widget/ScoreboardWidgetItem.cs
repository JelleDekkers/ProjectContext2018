using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class ScoreboardWidgetItem : MonoBehaviour {

        [SerializeField] private Text nameTxt, scoreTxt;

        public Player Player { get; private set; }
        private ScoreManager scoreManager;
        private ScoreboardWidget scoreboard;

        public void Init(Player player, ScoreManager scoreManager, ScoreboardWidget scoreboard) {
            Player = player;
            this.scoreManager = scoreManager;

            nameTxt.text = player.Name;
            scoreTxt.text = player.ScoreManager.Score.ToString();
            scoreManager.OnScoreChanged += UpdateScore;
            this.scoreboard = scoreboard;
        }

        private void UpdateScore(float newScore) {
            scoreTxt.text = ((int)newScore).ToString();
            scoreboard.UpdateListOrder();
        }

        private void OnDestroy() {
            scoreManager.OnScoreChanged -= UpdateScore;
        }
    }
}