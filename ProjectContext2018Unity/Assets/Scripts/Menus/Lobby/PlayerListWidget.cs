using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListWidget : MonoBehaviour {

    public PlayerCard playerCardPrefab;
    public LocalPlayerCard localPlayerCardPrefab;
    public PlayerList playerList;

    private List<PlayerCard> cardList;

    private void Start() {
        cardList = new List<PlayerCard>();
        PlayerList.OnPlayerAdded += InstantiateNewCard;
        PlayerList.OnPlayerRemoved += RemoveCard;
        playerList = UnityEngine.Networking.NetworkManager.singleton.gameObject.GetComponent<PlayerList>();
    }

    private void InstantiateNewCard(Player player) {
        PlayerCard card;
        if (player.isLocalPlayer)
            card = Instantiate(localPlayerCardPrefab);
        else
            card = Instantiate(playerCardPrefab);

        card.transform.SetParent(transform);
        cardList.Add(card);
        card.Init(player);
    }

    private void RemoveCard(int id) {
        Destroy(cardList[id].gameObject);
        cardList.RemoveAt(id);
    }

    private void OnDestroy() {
        PlayerList.OnPlayerAdded -= InstantiateNewCard;
        PlayerList.OnPlayerRemoved -= RemoveCard;
    }
}
