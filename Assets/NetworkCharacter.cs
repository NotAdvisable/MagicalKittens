using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkCharacter : MonoBehaviour {

    [SerializeField] private GameObject _landingEffect;

    public virtual void Land() {
        if (_landingEffect != null) Instantiate(_landingEffect, transform.position,Quaternion.identity);
    }
}
