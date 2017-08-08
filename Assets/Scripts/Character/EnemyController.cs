using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

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
        NetworkState.Singleton.RpcSpawnProjectile(Random.Range(0,4), _particleSpawner.position, transform.rotation, gameObject);
        _anim.SetTrigger("Attack");
    }
    public void Attack()
    {
        StartCoroutine(AttackCooldown());
        _anim.SetTrigger("Attack");
        //Instantiate(_hitPrefab, transform.position + Vector3.forward * 2, Quaternion.identity);
        var targetHealth = _hunted.GetComponent<Health>();
        if (targetHealth != null) targetHealth.InflictDamage(_meleeDamage);
    }
    public void SelfDestruct()
    {
        Attack();
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
    public override void Hit(float dmg)
    {
        _anim.SetTrigger("Hit");
        base.Hit(dmg);
    }
    public override void Die()
    {
        EventController.Singleton.EnemyDied();
        GetComponent<AIController>().TurnOffFSM();
        _anim.SetTrigger("Die");
        _agent.speed = 0;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<NetworkTransform>().enabled = false;
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
