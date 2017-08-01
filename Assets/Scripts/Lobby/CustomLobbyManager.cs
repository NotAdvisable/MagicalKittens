using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomLobbyManager : NetworkLobbyManager {
    Dictionary<int, int> currentPlayers = new Dictionary<int, int>();

    public override GameObject OnLobbyServerCreateLobbyPlayer(NetworkConnection conn, short playerControllerId) {
        if (!currentPlayers.ContainsKey(conn.connectionId))
            currentPlayers.Add(conn.connectionId, 0);

        return CreatePlayer(conn.connectionId);

        //  return base.OnLobbyServerCreateLobbyPlayer(conn, playerControllerId);
    }
    public void SetPlayerTypeLobby(NetworkConnection conn, int _type) {

        if (currentPlayers.ContainsKey(conn.connectionId))
            currentPlayers[conn.connectionId] = _type;
        Debug.Log("set");
    }

    public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId) {
        // NetworkServer.AddPlayerForConnection(conn, _temp, playerControllerId);

        return CreatePlayer(conn.connectionId);
    }

    public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer) {
        NetworkState.Singleton.CmdTryKillPlayer(lobbyPlayer);
        return base.OnLobbyServerSceneLoadedForPlayer(lobbyPlayer, gamePlayer);
        
    }

    private GameObject CreatePlayer(int id) {
        int index = currentPlayers[id];

        Debug.Log("creating cat number " + index);
        Debug.Log("created");

       Transform transTemp = startPositions[id];
        Debug.Log("created player for connectionID " + id);
        GameObject temp = Instantiate(spawnPrefabs[index], transTemp.position, transTemp.rotation);

        NetworkState.Singleton.TryAddPlayer(temp);

        return temp;
    }
}
