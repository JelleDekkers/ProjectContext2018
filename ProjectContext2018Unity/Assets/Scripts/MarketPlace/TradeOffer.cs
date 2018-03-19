using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TradeOffer {
    public Player player;
    public int productId;
    public int amount;
    public float totalValue;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="player">The player who offered the trade</param>
    /// <param name="productId">ID of the resource</param>
    /// <param name="amount">Amount of the resource</param>
    /// <param name="totalValue">Total value (cost * amount)</param>
    public TradeOffer(Player player, int productId, int amount, float totalValue) {
        this.player = player;
        this.productId = productId;
        this.amount = amount;
        this.totalValue = totalValue;
    }
}
