using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewTradeOfferWidget : MonoBehaviour {

    [SerializeField] private MarketPlace marketPlace;
    [SerializeField] private Dropdown dropDown;
    [SerializeField] private InputField amountInput, costInput;
    [SerializeField] private Button addBtn;

    private int amountValue;

    private void Start() {
        dropDown.options = new List<Dropdown.OptionData>();
        for(int i = 0; i < DataManager.ResourcesData.dataArray.Length; i++) {
            dropDown.options.Add(new Dropdown.OptionData());
            dropDown.options[i].text = DataManager.ResourcesData.dataArray[i].Name;
            dropDown.options[i].image = DataManager.ResourcePrefabs.GetResourceSprite(i);
        }
        dropDown.value = 0;
    }

    private void Update() {
        if(int.TryParse(amountInput.text, out amountValue))
            addBtn.interactable = PlayerResources.Instance.HasResourceAmount(dropDown.value, amountValue);
    }

    public void NewTradeOfferDone() {
        int resourceId = dropDown.value;
        int amount = int.Parse(amountInput.text);
        float cost = int.Parse(costInput.text);
        marketPlace.AddTradeOffer(new TradeOffer(Player.Instance, resourceId, amount, cost));
    }
}
