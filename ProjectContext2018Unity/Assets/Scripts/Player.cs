using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Player : NetworkBehaviour {

    // TODO: remove
    private static Player instance;
    public static Player Instance { get { return instance; } }

    public static Player LocalPlayer { get; private set; }
    public static Action<float> OnOtherPlayerPollutionRecieved;

    [SyncVar, SerializeField] private new string name;
    public string Name { get { return name; } }

    [SyncVar, SerializeField] private int playerID;
    public int PlayerID { get { return playerID; } }

    [SyncVar, SerializeField] private float playerPollutionPerMinute;
    public float PlayerPollutionPerMinute { get { return playerPollutionPerMinute; } }

    public PlayerList PlayerList { get; private set; }

    [SerializeField] PlayerResourcesHandler resourcesHandler;
    public PlayerResourcesHandler ResourcesHandler { get { return resourcesHandler; } }
     
    // Hack because syncliststruct won't work properly
    public SyncListInt resourcesAmountForTrade = new SyncListInt();
    public SyncListInt resourcesCostForTrade = new SyncListInt();

    [SerializeField] private SceneAsset gameOverScene;

    public void Start() {
        transform.SetParent(NetworkManager.singleton.transform);
        PlayerList = NetworkManager.singleton.transform.GetComponent<PlayerList>();
        playerID = PlayerList.Players.Count;
        PlayerList.AddPlayer(this);

        if (isLocalPlayer) {
            LocalPlayer = this;
            gameObject.name += "(LOCAL)";
        }

        for (int i = 0; i < DataManager.ResourcesData.dataArray.Length; i++) {
            resourcesAmountForTrade.Add(50);
            resourcesCostForTrade.Add(100);
        }

        //CityView.City.OnGameSceneWasLoaded += CmdUpdatePollutionPerMinute;
        //CityView.BuildingsHandler.OnBuildingListChanged += CmdUpdatePollutionPerMinute;
    }

    //private void OnDestroy() {
    //    CityView.City.OnGameSceneWasLoaded -= CmdUpdatePollutionPerMinute;
    //    CityView.BuildingsHandler.OnBuildingListChanged -= CmdUpdatePollutionPerMinute;
    //}

    [Command]
    private void CmdUpdatePollutionPerMinute() {
        playerPollutionPerMinute = CityView.BuildingsHandler.Instance.GetPollutionPerMinute();
    }

    [Command]
    public void CmdTradeWithPlayer(int tradeAcceptant, int tradeofferer, int resourceID, int amount) {
        TargetTradeWithPlayer(PlayerList.Players[tradeofferer].connectionToClient, tradeAcceptant, tradeofferer, resourceID, amount);
    }

    [TargetRpc]
    public void TargetTradeWithPlayer(NetworkConnection target, int tradeAcceptant, int tradeOfferer, int resourceID, int amount) {
        PlayerList.Players[tradeOfferer].SellTradeOffer(tradeAcceptant, resourceID, amount);
    }

    private void SellTradeOffer(int tradeAcceptant, int resourceID, int amount) {
        resourcesAmountForTrade[resourceID] -= amount;
        if (resourcesAmountForTrade[resourceID] < 0)
            resourcesAmountForTrade[resourceID] = 0;

        float value = resourcesCostForTrade[resourceID] * amount;
        TradeOffer tradeOffer = new TradeOffer(this, resourceID, amount, value);
        MarketPlace.OnTradeOfferSold(tradeOffer);
    }

    [Command]
    public void CmdAddGlobalPollution(int playerID, float amount) {
        RpcAddGlobalPollution(playerID, amount);
    }

    [ClientRpc]
    public void RpcAddGlobalPollution(int playerID, float amount) {
        foreach (Player player in PlayerList.Players) {
            OnOtherPlayerPollutionRecieved(amount);
        }
    }

    [Command]
    public void CmdUpdateName(string name) {
        this.name = name;
    }

    public void LoadGameOverLobby() {
        NetworkLoadScene.LoadSceneStatic(gameOverScene);
    }
}
