using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;

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
    
    public void AddPlayer(CatController controller) 
    {
        _currentPlayers.Add(controller);
    }

    public void RemovePlayer(CatController controller) 
    {
        _currentPlayers.Remove(controller);
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

    public void KillAllLobbyPlayers() {

        for (int i = _currentPlayers.Count - 1; i >= 0; i--) {
            if (_currentPlayers[i].IsLobbyCat) {
                Debug.LogError("destroyed a cat");
                Destroy(_currentPlayers[i].gameObject);
                _currentPlayers.RemoveAt(i);
            }
        }
    }
}
