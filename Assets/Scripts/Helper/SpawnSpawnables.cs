using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnSpawnables : NetworkBehaviour
{

    NetworkManager _manager;
    private int nextTransform;
    [SerializeField] private int _spawnInterval = 5;

    void Start()
    {
        if (!isServer) return;
        _manager = FindObjectOfType<NetworkManager>();
        StartCoroutine(Spawn());
    }

    //Spawns a random mix of mage and kamikaze bunnies at the spawn positions
    private IEnumerator Spawn()
    {
        while (nextTransform < transform.childCount)
        {
            var pos = transform.GetChild(nextTransform).position;
            var spawnable = Instantiate(_manager.spawnPrefabs[Random.Range(4, 6)], pos, Quaternion.identity);
            NetworkServer.Spawn(spawnable);
            nextTransform++;
            yield return new WaitForSeconds(_spawnInterval);
        }
    }
}
