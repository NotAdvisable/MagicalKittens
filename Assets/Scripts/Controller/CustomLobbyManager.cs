using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CustomLobbyManager : NetworkLobbyManager
{
    [SerializeField]
    private CatController m_DefaultCatPrefab;
    [SerializeField]
    private CatController[] m_CatPrefabs;

    private Dictionary<int, CatController> selectedPrefabs = new Dictionary<int, CatController>();
    private Dictionary<int, CatController> playerCatInstances = new Dictionary<int, CatController>();

    public override GameObject OnLobbyServerCreateLobbyPlayer(NetworkConnection conn, short playerControllerId)
    {
        var player = CreateLobbyPlayer(conn, playerControllerId);
        return player;
    }

    public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId)
    {
        var newPlayer = CreateIngamePlayer(conn, playerControllerId);
        return newPlayer;
    }

    public override void OnLobbyServerDisconnect(NetworkConnection conn)
    {
        base.OnLobbyServerDisconnect(conn);
        
        selectedPrefabs.Remove(conn.connectionId);
        playerCatInstances.Remove(conn.connectionId);
    }

    public override void OnStopServer()
    {
        base.OnStopServer();

        selectedPrefabs.Clear();
        playerCatInstances.Clear();
    }

    public override void OnStartServer()
    {
        base.OnStartServer();

        selectedPrefabs.Clear();
        playerCatInstances.Clear();
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
        selectedPrefabs.Remove(conn.connectionId);
        playerCatInstances.Remove(conn.connectionId);
    }

    public GameObject CreateLobbyPlayer(NetworkConnection conn, short playerControllerId)
    {
        //Get this id's starting point
        Transform spawnPoint = startPositions[Mathf.Min(conn.connectionId, startPositions.Count - 1)];

        //Creates a default cat everyone starts with
        CatController defaultCat = Instantiate(m_DefaultCatPrefab, spawnPoint.position, spawnPoint.rotation);
        defaultCat.IsLobbyCat = true;
        defaultCat.IsInitialCat = true;

        selectedPrefabs.Add(conn.connectionId, m_DefaultCatPrefab);
        playerCatInstances.Add(conn.connectionId, defaultCat);

        return defaultCat.gameObject;
    }

    public GameObject CreateIngamePlayer(NetworkConnection conn, short playerControllerId)
    {
        //Get this id's starting point
        Transform spawnPoint = startPositions[Mathf.Min(conn.connectionId, startPositions.Count - 1)];

        //Spawn a new instance of the given cat based on its prefab
        CatController selectedCatPrefab = m_DefaultCatPrefab;
        selectedPrefabs.TryGetValue(conn.connectionId, out selectedCatPrefab);

        //Destroy lobby cat object
        CatController lobbyCat = playerCatInstances[conn.connectionId];

        CatController instance = Instantiate(selectedCatPrefab, spawnPoint.position, spawnPoint.rotation);

        //Transfer the lobby name to the ingame cat
        instance._playerName = lobbyCat._playerName;
        return instance.gameObject;
    }

    public void ChangeSelectedPrefab(NetworkConnection conn, short playerControllerId, int prefabID)
    {
        //Check client inputs
        if (prefabID < m_CatPrefabs.Length && prefabID >= 0)
        {
            //Re-assign the selected prefab for the given connection
            selectedPrefabs.Remove(conn.connectionId);
            selectedPrefabs.Add(conn.connectionId, m_CatPrefabs[prefabID]);

            //Get old cat and spawn new one
            CatController oldCat = playerCatInstances[conn.connectionId];
            CatController newCat = Instantiate(m_CatPrefabs[prefabID]);

            //Copy transform over from the old cat
            newCat.transform.position = oldCat.transform.position;
            newCat.transform.rotation = oldCat.transform.rotation;
            newCat.transform.localScale = oldCat.transform.localScale;

            //Re-assign the currently spawned cat instance for given connection
            playerCatInstances.Remove(conn.connectionId);
            playerCatInstances.Add(conn.connectionId, newCat);

            //Make sure it is marked as lobby cat
            newCat.IsLobbyCat = true;

            //Copy over name of old cat
            newCat.PlayerName = oldCat.PlayerName;

            //Replace the controlled object with the new cat and destroy the old one
            NetworkServer.ReplacePlayerForConnection(conn, newCat.gameObject, playerControllerId);
            NetworkServer.Destroy(oldCat.gameObject);
        }
    }
}