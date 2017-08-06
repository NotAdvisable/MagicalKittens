﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ExplosionEffect _effect;
    [SerializeField] private float _initialSpeed = 40f;
    [SerializeField] private float _maxLifeTime = 1f;
    [SerializeField] private float _damage = 50f;
    private GameObject _shooter;

    private Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(transform.forward * _initialSpeed,ForceMode.VelocityChange);
        Destroy(gameObject, _maxLifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag(_shooter.tag) || other.isTrigger) return;
       var hit = other.GetComponent<IHitable>();
       if (hit != null) hit.Hit(_damage);
        _rb.velocity = Vector3.zero;
       // _rb.AddForce(Vector3.zero, ForceMode.VelocityChange);
       Instantiate(_effect, transform.position, Quaternion.identity);

    }

    internal void SetShooter(GameObject shooter)
    {
        _shooter = shooter;
    }
}
