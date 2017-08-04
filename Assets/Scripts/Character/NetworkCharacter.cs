using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;

public class NetworkCharacter : NetworkBehaviour, IHitable {

    [SerializeField] private GameObject _landingEffect;
    public virtual void Land() {
        if (_landingEffect != null) Instantiate(_landingEffect, transform.position + Vector3.up,Quaternion.identity);
    }
    public virtual void Hit() { }

    public virtual Vector3 FindFirstPlayerWithinDistance(float distance)
    {
        var posList = NetworkState.Singleton.GetCurrentPlayers().Select(element => element.transform.position).ToList();
        return transform.position.FirstWithinDistance(posList, distance);
    }
}
