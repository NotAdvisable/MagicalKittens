using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkCharacter : NetworkBehaviour {

    [SerializeField] private GameObject _landingEffect;
    public virtual void Land() {
        if (_landingEffect != null) Instantiate(_landingEffect, transform.position,Quaternion.identity);
    }
}
