using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomLobbyManager : NetworkLobbyManager {
    Dictionary<int, int> currentPlayers = new Dictionary<int, int>();

    public override GameObject OnLobbyServerCreateLobbyPlayer(NetworkConnection conn, short playerControllerId) {
        if (!currentPlayers.ContainsKey(conn.connectionId))
            currentPlayers.Add(conn.connectionId, 0);
        SetPlayerTypeLobby(conn, currentPlayers.Count-1);
        return OnLobbyServerCreateGamePlayer(conn,playerControllerId);
   
      //  return base.OnLobbyServerCreateLobbyPlayer(conn, playerControllerId);
    }
    public void SetPlayerTypeLobby(NetworkConnection conn, int _type) {
        if (currentPlayers.ContainsKey(conn.connectionId))
            currentPlayers[conn.connectionId] = _type;
    }

    public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId) {
        int index = currentPlayers[conn.connectionId];
        Transform transTemp = startPositions[conn.connectionId];
        /*
        GameObject _temp = Instantiate(spawnPrefabs[index],
            startPositions[conn.connectionId].position,
            Quaternion.identity);
        
        NetworkServer.AddPlayerForConnection(conn, _temp, playerControllerId);
        */
        return Instantiate(spawnPrefabs[index], transTemp.position, transTemp.rotation);
    }
    public override void OnLobbyServerPlayersReady() {
        base.OnLobbyServerPlayersReady();
        Debug.Log("READY");
        ServerChangeScene(playScene);
    }
}
