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

    public virtual Transform FindAnyPlayerWithinDistance(float distance)
    {
        return transform.position.FirstWithinDistance(NetworkState.Singleton.GetPlayerTransform(), distance);
    }
    public virtual Transform FindClosestPlayerWithinDistance(float distance)
    {
        return transform.ClosestTransformWithinDistance(NetworkState.Singleton.GetPlayerTransform(), distance);
    }

}
