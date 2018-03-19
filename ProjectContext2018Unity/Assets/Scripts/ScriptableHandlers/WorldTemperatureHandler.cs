using System;
using System.Collections.Generic;
using UnityEngine;

public class WorldTemperatureHandler : MonoBehaviour {

    [SerializeField] private WorldTemperature worldTemperature;

    private void Start() {
        worldTemperature.Init();
        CityView.Building.OnProductionCycleCompleted += ProcessProductionResult;
        Player.OnOtherPlayerPollutionRecieved += ProcessOtherPlayerPollution;
    }

    private void OnDestroy() {
        CityView.Building.OnProductionCycleCompleted -= ProcessProductionResult;
        Player.OnOtherPlayerPollutionRecieved -= ProcessOtherPlayerPollution;
    }

    private void ProcessProductionResult(CityView.Building building, ProductionCycleResult result) {
        if (result.pollutionPoints > 0)
            worldTemperature.AddPollution(result.pollutionPoints);

        if (Player.LocalPlayer != null)
            Player.LocalPlayer.CmdAddGlobalPollution(Player.LocalPlayer.PlayerID, result.pollutionPoints);
    }

    private void ProcessOtherPlayerPollution(float amount) {
        worldTemperature.AddPollution(amount);
    }

    private void OnGUI() {
        GUI.Label(new Rect(10, 200, 1000, 20), "Temperature: " + worldTemperature.CurrentTemperature.ToString());
    }
}
