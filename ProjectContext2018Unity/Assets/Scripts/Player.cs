using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    // TODO: remove
    private static Player instance;
    public static Player Instance { get { return instance; } }

    public static Player LocalPlayer { get; private set; }

    [SyncVar, SerializeField] private new string name;
    public string Name { get { return name; } }

    [SyncVar, SerializeField] private int playerID;
    public int PlayerID { get { return playerID; } }

    [SyncVar, SerializeField] private float globalPollution;
    public float WorldPollution { get { return globalPollution; } }

    [SyncVar, SerializeField] private float playerPollution;
    public float PlayerPollution { get { return playerPollution; } }

    public PlayerList PlayerList { get; private set; }

    //[SyncVar, SerializeField] private float money;
    //public float Money { get { return money; } }

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
    }

    [Command]
    public void CmdUpateResourceList(int index, int value) {
        // TODO: update
        resourcesCostForTrade[index] = value;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tradeAcceptant">The player who accepted the offer</param>
    /// <param name="tradeProvider">The player whos offer it is</param>
    /// <param name="resoruceID">ID of resource</param>
    /// <param name="amount">Amount of resource bought</param>
    [Command]
    public void CmdTradeWithPlayer(int tradeAcceptant, int tradeProvider, int resourceID, int amount) {
        TargetTradeWithPlayer(PlayerList.Players[tradeProvider].connectionToClient, tradeAcceptant, tradeProvider, resourceID, amount);
    }

    [TargetRpc]
    public void TargetTradeWithPlayer(NetworkConnection target, int tradeAcceptant, int tradeProvider, int resourceID, int amount) {
        PlayerList.Players[tradeProvider].SellTradeOffer(tradeAcceptant, resourceID, amount);
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
        globalPollution += amount;
        RpcUpdateGlobalPollution(playerID, globalPollution);
    }

    [ClientRpc]
    public void RpcUpdateGlobalPollution(int playerID, float amount) {
        foreach (Player player in PlayerList.Players) {
            if (player.PlayerID == playerID)
                playerPollution += amount;

            player.globalPollution = amount;
        }
    }

    [Command]
    public void CmdUpdateName(string name) {
        this.name = name;
    }

    public void GameOver() {
        Debug.Log("Game Over");
        NetworkLoadScene.LoadSceneStatic(gameOverScene);
    }
}
