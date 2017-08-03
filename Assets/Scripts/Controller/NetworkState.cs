using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;

public class NetworkState : NetworkBehaviour
{

    private List<CatController> _currentPlayers = new List<CatController>();

    private static NetworkState _instance;

    public static NetworkState Singleton { get { return _instance; } }

    [SerializeField] private GameObject[] _projectiles;

    private Dictionary<int, GameObject> _Projectile_IDtoGO = new Dictionary<int, GameObject>();
    private Dictionary<GameObject, int> _Projectile_GOtoID = new Dictionary<GameObject, int>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(this);


        for (int i = 0; i < _projectiles.Length; i++)
        {
            _Projectile_GOtoID.Add(_projectiles[i], i);
            _Projectile_IDtoGO.Add(i, _projectiles[i]);
        }
        Debug.Log(_Projectile_GOtoID.Count);
    }

    public void TryAddPlayer(GameObject player)
    {
        var temp = player.GetComponent<CatController>();

        if (temp != null)
        {
            _currentPlayers.Add(temp);
        }
        else
        {
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

    public List<CatController> GetCurrentPlayers()
    {
        return _currentPlayers;
    }
    public void KillAllPlayers()
    {
        foreach (CatController player in _currentPlayers)
        {
            Destroy(player.gameObject);
        }
        _currentPlayers.Clear();
    }

    public void KillAllLobbyPlayers()
    {

        for (int i = _currentPlayers.Count - 1; i >= 0; i--)
        {
            if (_currentPlayers[i].IsLobbyCat)
            {
                Debug.LogError("destroyed a cat");
                Destroy(_currentPlayers[i].gameObject);
                _currentPlayers.RemoveAt(i);
            }
        }
    }
    [ClientRpc]
    public void RpcSpawnProjectile(int id, Vector3 position, Quaternion rotation)
    {
       Instantiate(_Projectile_IDtoGO[id], position, rotation);
    }
}
