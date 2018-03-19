using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerList : MonoBehaviour {

    private static PlayerList instance;
    public static PlayerList Instance { get { return instance; } }

    [SerializeField] private List<Player> players = new List<Player>();
    public List<Player> Players { get { return players; } }

    public static Action<Player> OnPlayerAdded;
    public static Action<int> OnPlayerRemoved;

    public void Awake() {
        instance = this;
    }

    public void AddPlayer(Player player) {
        Players.Add(player);

        if (OnPlayerAdded != null)
            OnPlayerAdded(player);
    }

    public void RemovePlayer(int id) {
        Players.RemoveAt(id);

        if (OnPlayerRemoved != null)
            OnPlayerRemoved(id);
    }

    private void OnDestroy() {
        players.Clear();
    }
}
