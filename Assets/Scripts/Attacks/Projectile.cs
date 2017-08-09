using System;
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
        //Random pitch to get some variation
        GetComponent<AudioSource>().pitch = 1 + UnityEngine.Random.Range(-.5f, .5f);

        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(transform.forward * _initialSpeed,ForceMode.VelocityChange);

        Destroy(gameObject, _maxLifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
       //cats can only hit bunnies; bunnies can only hit players
       if (other.CompareTag(_shooter.tag) || other.isTrigger) return;

       var hit = other.GetComponent<IHitable>();
       if (hit != null) hit.Hit(_damage, _shooter);
       //looks better
        _rb.velocity = Vector3.zero;
        //multiple collisions can occur otherwise
        GetComponent<Collider>().enabled = false; 

       Instantiate(_effect, transform.position, Quaternion.identity);

    }

    internal void SetShooter(GameObject shooter)
    {
        _shooter = shooter;
    }
}
