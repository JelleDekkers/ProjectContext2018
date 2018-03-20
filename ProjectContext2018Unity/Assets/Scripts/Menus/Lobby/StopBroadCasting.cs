using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBroadCasting : MonoBehaviour {

	public void StopBroadCastingNetwork() {
        CustomNetworkDiscovery.Instance.StopBroadcasting();
    }
}
