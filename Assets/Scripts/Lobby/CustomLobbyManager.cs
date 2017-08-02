using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class CustomLobbyManager : NetworkLobbyManager {
    Dictionary<int, int> currentPlayers = new Dictionary<int, int>();

    public override GameObject OnLobbyServerCreateLobbyPlayer(NetworkConnection conn, short playerControllerId) {
        if (!currentPlayers.ContainsKey(conn.connectionId)) {
            currentPlayers.Add(conn.connectionId, 0);

        }

        var player = CreatePlayer(conn.connectionId);
        CatController controller = player.GetComponent<CatController>();
        if (controller) {
            controller.IsLobbyCat = true;
        }

        return player;
    }
    public void SetPlayerTypeLobby(NetworkConnection conn, int _type) {

        if (currentPlayers.ContainsKey(conn.connectionId))
            currentPlayers[conn.connectionId] = _type;
        Debug.Log("set");
    }

    public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId) {
        // NetworkServer.AddPlayerForConnection(conn, _temp, playerControllerId);
        Debug.Log("create now");

        var newPlayer = CreatePlayer(conn.connectionId);
        return newPlayer;
    }

    public override void OnLobbyClientSceneChanged(NetworkConnection conn) {
        base.OnLobbyClientSceneChanged(conn);

     //   NetworkState.Singleton.KillAllLobbyPlayers();
    }

    private GameObject CreatePlayer(int id) {
        int index = currentPlayers[id];
        
        Debug.Log("creating cat number " + index);
        Debug.Log("created");

        Debug.Log("cur not null: " + currentPlayers != null);

        Transform transTemp = startPositions[id];
        GameObject temp = Instantiate(spawnPrefabs[index], transTemp.position, transTemp.rotation);

        //NetworkState.Singleton.TryAddPlayer(temp);

        return temp;
    }
}
