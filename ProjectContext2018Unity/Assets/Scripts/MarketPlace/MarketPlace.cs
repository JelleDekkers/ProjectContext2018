using System;
using System.Collections.Generic;
using UnityEngine;

public class MarketPlace : MonoBehaviour {

    [SerializeField] private List<TradeOffer> tradeOffers = new List<TradeOffer>();
    public List<TradeOffer> TradeOffers { get { return tradeOffers; } }

    public static Action<TradeOffer> OnTradeOfferBought, OnTradeOfferSold;

    public Action OnTradeOffersChanged;

    public void RemoveTradeOffer(TradeOffer offer) {
        tradeOffers.Remove(offer);

        if (OnTradeOffersChanged != null)
            OnTradeOffersChanged();
    }
    
    public void AddTradeOffer(TradeOffer offer) {
        tradeOffers.Add(offer);

        if (OnTradeOffersChanged != null)
            OnTradeOffersChanged();
    }
}
