using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkState : NetworkBehaviour {

    public static NetworkState Singleton { get; private set; }

    private List<CatController> _currentPlayers = new List<CatController>();

    void Awake() {
        Singleton = this;
        DontDestroyOnLoad(this);

    }

    public void TryAddPlayer(GameObject player) {
        var temp = player.GetComponent<CatController>();

        if (temp != null) {
            _currentPlayers.Add(temp);
        }
        else {
            Debug.Log("Tried to add invalid player. Component of type CatController required.");
        }
    }
    public List<CatController> GetCurrentPlayers() {
        return _currentPlayers;
    }
    public void KillAllPlayers() {
        foreach (CatController player in _currentPlayers) {
            Destroy(player.gameObject);
        }
        _currentPlayers.Clear();
    }
    [Command]
    public void CmdTryKillPlayer(GameObject player) {
        var temp = player.GetComponent<CatController>();
        if (temp != null) {
            Destroy(player);
            _currentPlayers.Remove(temp);
        }
        else {
            Debug.Log("Tried to remove invalid player. Component of type CatController required.");
        }
    }
}
