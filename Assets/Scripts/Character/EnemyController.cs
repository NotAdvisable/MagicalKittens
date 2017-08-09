using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;
/// <summary>
/// methods and values that define the enemy's strength and what happens when it attacks and dies
/// </summary>
public class EnemyController : NetworkCharacter {

    private NavMeshAgent _agent;
    private bool _withinAttackRange;

    public bool WithinAttackRange { get { return _withinAttackRange; } }


    [SyncVar]private bool _cooldownComplete;
    public bool CoolDownComplete { get { return _cooldownComplete; } }
    [SerializeField] private Transform _particleSpawner;
    [SerializeField] private float _meleeDamage = 75;
    [SerializeField] private float _kamiKazeDamage = 300;
    [SerializeField] private GameObject _hitPrefab;
    [SerializeField] private GameObject _selfDestructPrefab;
    [SerializeField] private float _attackCooldown = 3f;
    private Collider _hunted;
    public event Action<GameObject> OnHitEvent;

    protected override void Start()
    {
        base.Start();
        _agent = GetComponent<NavMeshAgent>();
        _cooldownComplete = true;
    }

    //active
    public void RangeAttack()
    {

        StartCoroutine(AttackCooldown());
        NetworkState.Singleton.RpcSpawnProjectile(UnityEngine.Random.Range(0,4), _particleSpawner.position, transform.rotation, gameObject);
        _anim.SetTrigger("Attack");
    }
    public void Attack()
    {
        StartCoroutine(AttackCooldown());
        _anim.SetTrigger("Attack");
        var hit = _hunted.GetComponent<IHitable>();
        if (hit != null) hit.Hit(_meleeDamage,gameObject);
    }
    public void SelfDestruct()
    {
        Attack();
        NetworkState.Singleton.RpcSpawnProjectile(5, transform.position, transform.rotation, null);
        Instantiate(_selfDestructPrefab, transform.position, transform.rotation);

        var targetHealth = _hunted.GetComponent<Health>();
        if (targetHealth != null) targetHealth.InflictDamage(_kamiKazeDamage);

        EventController.Singleton.ScreenShake();
        Destroy(gameObject);
    }
    private IEnumerator AttackCooldown()
    {
        _cooldownComplete = false;
        yield return new WaitForSeconds(_attackCooldown);
        _cooldownComplete = true;
    }

    //passive
    public override void Hit(float dmg, GameObject aggressor)
    {
        if (OnHitEvent != null) OnHitEvent(aggressor);
        _anim.SetTrigger("Hit");
        base.Hit(dmg,null);
    }
    public override void Die()
    {
        EventController.Singleton.EnemyDied();
        GetComponent<AIController>().TurnOffFSM();
        _anim.SetTrigger("Die");
        _agent.speed = 0;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<NetworkTransform>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }
    public void SetAnimMoving(float value)
    {
        _anim.SetFloat("Speed", value);
    }

    //trigger
    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<CatController>()) return;
        _hunted = other;
        _withinAttackRange = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.GetComponent<CatController>() || other != _hunted) return;
        _hunted = null;
        _withinAttackRange = false;
    }
}
