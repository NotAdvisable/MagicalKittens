using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(NetworkCharacter))]
public class Health : NetworkBehaviour {

    [SerializeField] [Range(0, 4000)] private float _maxHealth = 150;

    [SyncVar(hook = "CheckIfAlive")] private float _currentHealth;

    private bool _alreadyDead;

    private NetworkCharacter _owner;

	void Start () {
        _currentHealth = _maxHealth;
        _owner = GetComponent<NetworkCharacter>();
	}

    private void CheckIfAlive(float health)
    {
        if (health <= 0 && _owner != null)
        {
            _owner.Die();
            _alreadyDead = true;
        }
    }
    
    public void InflictDamage(float dmg)
    {
        if (isServer && !_alreadyDead)
        {
            _currentHealth -= dmg;
        }
    }

    public void Respawn()
    {
        _alreadyDead = false;
        _currentHealth = _maxHealth;
    }
}
