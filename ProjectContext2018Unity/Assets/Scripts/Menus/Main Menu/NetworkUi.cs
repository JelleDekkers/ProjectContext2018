using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace AXNGames.Networking
{ 
    public class NetworkUi : MonoBehaviour
    {
        public Button joinButton;
        public Button hostButton;
        public GameObject hostPanel;
        public Text ipAddressText;

        public void HostGame()
        {
            CustomNetworkDiscovery.Instance.InitializeNetworkDiscovery();
            CustomNetworkDiscovery.Instance.StartBroadcasting();
            NetworkManager.singleton.StartHost();
        }

        public void ReceiveGameBroadcast()
        {
            CustomNetworkDiscovery.Instance.InitializeNetworkDiscovery();
            CustomNetworkDiscovery.Instance.ReceiveBraodcast();
        }

        public void JoinGame()
        {
            NetworkManager.singleton.networkAddress = ipAddressText.text;
            NetworkManager.singleton.StartClient();
            CustomNetworkDiscovery.Instance.StopBroadcasting();
        }

        public void OnReceiveBroadcast(string fromIp, string data)
        {
            hostButton.gameObject.SetActive(false);
            joinButton.gameObject.SetActive(false);
            ipAddressText.text = fromIp;
            hostPanel.SetActive(true);
        }

        void Start()
        {
            CustomNetworkDiscovery.Instance.onServerDetected += OnReceiveBroadcast;
        }

        void OnDestroy()
        {
            CustomNetworkDiscovery.Instance.onServerDetected -= OnReceiveBroadcast;
        }
    }

}
