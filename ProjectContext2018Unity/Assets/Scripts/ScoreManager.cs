using System;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreManager : NetworkBehaviour {

    [SyncVar(hook = "OnScoreChangedFunction"), SerializeField] private float score;
    public float Score { get { return score; } }

    [SyncVar] public int inhabitants;
    [SyncVar] public float money;

    [SerializeField] private Player player;
    [SerializeField] private PlayerResources playerResources;
    [SerializeField] private Population population;
    [SerializeField] private float pointsPerInhabitant = 1;
    [SerializeField] private float pointsPerMoney = 1;

    public Action<float> OnScoreChanged;

    public void Start() {
        if (player == Player.LocalPlayer) {
            Population.OnPopulationCountChanged += OnInhabitantsChange;
            PlayerResources.OnMoneyChanged += OnMoneyChange;
        }
    }

    private void OnDestroy() {
        Population.OnPopulationCountChanged -= OnInhabitantsChange;
        PlayerResources.OnMoneyChanged -= OnMoneyChange;
    }

    public void OnScoreChangedFunction(float newScore) {
        if (OnScoreChanged != null)
            OnScoreChanged(newScore);
    }

    public void OnInhabitantsChange(int change) {
        inhabitants = population.TotalPopulation;
        score = inhabitants* pointsPerInhabitant + money * pointsPerMoney;
        if (inhabitants > 0)
            CmdUpdateScore(score, inhabitants, money);
    }

    public void OnMoneyChange(float change) {
        money = PlayerResources.Money;
        score = inhabitants* pointsPerInhabitant + money * pointsPerMoney;
        if(money > 0)
            CmdUpdateScore(score, inhabitants, money);
    }

    [Command]
    public void CmdTest() {
        inhabitants++;
    }

    [Command]
    public void CmdUpdateScore(float score, int inhabitants, float money) {
        this.score = score;
        this.money = money;
        this.inhabitants = inhabitants;
    }

    //private void OnGUI() {
    //    if (player != Player.LocalPlayer)
    //        return;

    //    for (int i = 0; i < PlayerList.Instance.Players.Count; i++) {
    //        Player p = PlayerList.Instance.Players[i];
    //        GUI.Label(new Rect(10, 250 + (20 * i), 1000, 20), p.Name + " Score: " + p.ScoreManager.Score.ToString() + 
    //                            " money: " + p.ScoreManager.money.ToString() + " pop: " + p.ScoreManager.inhabitants.ToString());
    //    }
    //}
}
