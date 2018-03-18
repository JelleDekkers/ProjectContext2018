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
    public float GlobalPollution { get { return globalPollution; } }

    public PlayerList PlayerList { get; private set; }

    [SerializeField] PlayerResourcesHandler resourcesHandler;
    public PlayerResourcesHandler ResourcesHandler { get { return resourcesHandler; } }
     
    public SyncListUInt resources = new SyncListUInt();

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

        for (int i = 0; i < DataManager.ResourcesData.dataArray.Length; i++)
            resources.Add(0);
    }

    [Command]
    public void CmdUpateResourceList(int index, uint value) {
        resources[index] = value;
    }

    [Command]
    public void CmdTradeWithPlayer(int senderID, int recieverID, int amount) {
        TargetTradeWithPlayer(PlayerList.Players[recieverID].connectionToClient, senderID, recieverID, amount);
    }

    [TargetRpc]
    public void TargetTradeWithPlayer(NetworkConnection target, int senderID, int recieverID, int amount) {
        PlayerList.Players[recieverID].AddResourceFromTrade(senderID, amount);
    }

    private void AddResourceFromTrade(int senderID, int amount) {
        Debug.Log(gameObject.name + " Recieved trade, player: " + Name + " id: " + playerID + " from sender " + senderID + " amount: " + amount);
    }

    [Command]
    public void CmdAddGlobalPollution(int amount) {
        globalPollution += amount;
        RpcUpdateGlobalPollution(globalPollution);
    }

    [ClientRpc]
    public void RpcUpdateGlobalPollution(float amount) {
        foreach (Player player in PlayerList.Players)
            player.globalPollution = amount;
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
