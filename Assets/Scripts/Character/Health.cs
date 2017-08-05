using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(NetworkCharacter))]
public class Health : NetworkBehaviour {

    [SerializeField] [Range(0, 4000)] private float _maxHealth = 150;

    [SyncVar(hook = "CheckIfAlive")] private float _currentHealth;
    private NetworkCharacter _owner;
	// Use this for initialization
	void Start () {
        _currentHealth = _maxHealth;
        _owner = GetComponent<NetworkCharacter>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void CheckIfAlive(float health)
    {
        Debug.Log(health);
        if (health <= 0 && _owner != null)
        {
            _owner.Die();
        }

    }
    
    public void InflictDamage(float dmg)
    {
        if (isServer)
        {
            _currentHealth -= dmg;
        }
    }
}
