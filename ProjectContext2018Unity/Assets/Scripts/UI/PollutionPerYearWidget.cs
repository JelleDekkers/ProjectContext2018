using UnityEngine;
using UnityEngine.UI;

public class PollutionPerYearWidget : MonoBehaviour {

    [SerializeField] private Text pollutionAmount;

    private void Awake() {
        Player.LocalPlayer.OnUpdatePollutionPerMinuteChanged += UpdateAmount;
        UpdateAmount();
    }

    private void OnDestroy() {
        Player.LocalPlayer.OnUpdatePollutionPerMinuteChanged -= UpdateAmount;
    }

    private void UpdateAmount() {
        pollutionAmount.text = string.Format("{0:0}", Player.LocalPlayer.PlayerPollutionPerYear);
    }
}
