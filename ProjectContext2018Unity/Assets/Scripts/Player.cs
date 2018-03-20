using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Player : NetworkBehaviour {

    public static Player LocalPlayer { get; private set; }
    public static Action<float> OnOtherPlayerPollutionRecieved;

    [SyncVar, SerializeField] private new string name;
    public string Name { get { return name; } }

    [SyncVar, SerializeField] private int playerID;
    public int PlayerID { get { return playerID; } }

    [SyncVar, SerializeField] private Climate climateType;
    public Climate ClimateType { get { return climateType; } }

    [SyncVar, SerializeField] private float playerPollutionPerMinute;
    public float PlayerPollutionPerMinute { get { return playerPollutionPerMinute; } }

    public PlayerList PlayerList { get; private set; }
     
    // Temporary hack because syncliststruct won't work properly
    public SyncListInt resourcesAmountForTrade = new SyncListInt();
    public SyncListInt resourcesCostForTrade = new SyncListInt();

    [SerializeField] private SceneAsset gameOverScene;
    [SerializeField] private ScoreManager scoreManager;
    public ScoreManager ScoreManager { get { return scoreManager; } }

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
            resourcesAmountForTrade.Add(0);
            resourcesCostForTrade.Add(0);
        }

        AssignSemiRandomizedClimateType();

        //CityView.City.OnGameSceneWasLoaded += CmdUpdatePollutionPerMinute;
        //CityView.BuildingsHandler.OnBuildingListChanged += CmdUpdatePollutionPerMinute;
    }

    private void AssignSemiRandomizedClimateType() {
        climateType = (Climate)((playerID % (Enum.GetNames(typeof(Climate)).Length - 1)) + 1); // -1 and +1 to prevent Climate.None
    }

    //private void OnDestroy() {
    //    CityView.City.OnGameSceneWasLoaded -= CmdUpdatePollutionPerMinute;
    //    CityView.BuildingsHandler.OnBuildingListChanged -= CmdUpdatePollutionPerMinute;
    //}

    //[Command]
    //private void CmdUpdatePollutionPerMinute() {
    //    playerPollutionPerMinute = CityView.BuildingsHandler.Instance.GetPollutionPerMinute();
    //}

    [Command]
    public void CmdTradeWithPlayer(int tradeAcceptant, int tradeOfferer, int resourceID, int amount) {
        RpcTradeWithPlayer(tradeAcceptant, tradeOfferer, resourceID, amount);
    }

    [ClientRpc]
    public void RpcTradeWithPlayer(int tradeAcceptant, int tradeOfferer, int resourceID, int amount) {
        PlayerList.Players[tradeOfferer].SellTradeOffer(tradeAcceptant, resourceID, amount);
    }

    [Command]
    public void CmdSendTradeOffer(int index, int amount, int value) {
        resourcesAmountForTrade[index] = value;
        resourcesCostForTrade[index] = amount;
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

    public void DisconnectFromNetwork() {
        MasterServer.UnregisterHost();
        NetworkManager.singleton.StopClient();
        NetworkManager.singleton.StopHost();
        NetworkManager.singleton.StopMatchMaker();
        NetworkServer.Reset();
        Network.Disconnect();

        Destroy(gameObject);
        Destroy(NetworkManager.singleton.gameObject);
    }
}
