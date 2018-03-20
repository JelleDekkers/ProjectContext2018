using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class PlayerInfoItem : MonoBehaviour {

        [SerializeField] private Text indexTxt, nameTxt, scoreTxt, moneyTxt, populationTxt;

        public void Init(int index, Player player) {
            indexTxt.text = index.ToString();
            nameTxt.text = player.Name;
            scoreTxt.text = player.ScoreManager.Score.ToString();
            moneyTxt.text = player.ScoreManager.money.ToString();
            populationTxt.text = player.ScoreManager.inhabitants.ToString();
        }
    }
}