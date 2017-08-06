using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ShowLobbyInfo : MonoBehaviour {
    [SerializeField] private Text[] _info = new Text[2];

    private void OnEnable() {
        if (NetworkServer.active) {
            _info[0].text = "Server: port = " + NetworkManager.singleton.networkPort;
            _info[0].gameObject.SetActive(true);
        }
        if(NetworkClient.active) {
            _info[1].text = "Client: adress = " + NetworkManager.singleton.networkAddress + ":" + NetworkManager.singleton.networkPort;
            _info[1].gameObject.SetActive(true);
        }
    }
    private void OnDisable() {
        foreach(Text t in _info) {
            t.gameObject.SetActive(false);
        }
    }
}
