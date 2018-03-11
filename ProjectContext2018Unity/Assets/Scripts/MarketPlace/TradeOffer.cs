using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TradeOffer {
    public Player player;
    public int productId;
    public int amount;
    public float cost;

    public TradeOffer(Player player, int productId, int amount, float cost) {
        this.player = player;
        this.productId = productId;
        this.amount = amount;
        this.cost = cost;
    }
}
