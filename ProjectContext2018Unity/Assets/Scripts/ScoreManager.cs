using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreManager : NetworkBehaviour {

    [SyncVar, SerializeField] private float score;
    public float Score { get { return score; } }

    [SyncVar] public int inhabitants;
    [SyncVar] public float money;

    [SerializeField] private Player player;
    [SerializeField] private PlayerResources playerResources;
    [SerializeField] private Population population;
    [SerializeField] private float pointsPerInhabitant = 1;
    [SerializeField] private float pointsPerMoney = 1;

    public void Start() {
        if (player == Player.LocalPlayer) {
            Population.OnPopulationCountChanged += OnInhabitantsChange;
            PlayerResources.OnMoneyChanged += OnMoneyChange;
        }
    }

    private void Update() {
        if (player != Player.LocalPlayer)
            return;
    }
    private void OnDestroy() {
        Population.OnPopulationCountChanged -= OnInhabitantsChange;
        PlayerResources.OnMoneyChanged -= OnMoneyChange;
    }

    public void OnInhabitantsChange(int change) {
        inhabitants = population.TotalPopulation;
        if(inhabitants > 0)
            CmdUpdateScore(inhabitants, money);
    }

    public void OnMoneyChange(float change) {
        money = PlayerResources.Money;
        if(money > 0)
            CmdUpdateScore(inhabitants, money);
    }

    [Command]
    public void CmdTest() {
        inhabitants++;
    }

    [Command]
    public void CmdUpdateScore(int inhabitants, float money) {
        score = inhabitants * pointsPerInhabitant + money * pointsPerMoney;
        this.money = money;
        this.inhabitants = inhabitants;
    }

    private void OnGUI() {
        if (player != Player.LocalPlayer)
            return;

        for (int i = 0; i < PlayerList.Instance.Players.Count; i++) {
            Player p = PlayerList.Instance.Players[i];
            GUI.Label(new Rect(10, 250 + (20 * i), 1000, 20), p.Name + " Score: " + p.ScoreManager.Score.ToString() + 
                                " money: " + p.ScoreManager.money.ToString() + " pop: " + p.ScoreManager.inhabitants.ToString());
        }
    }
}
