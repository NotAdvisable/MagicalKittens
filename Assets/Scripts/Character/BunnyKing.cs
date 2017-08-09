using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class BunnyKing : NetworkCharacter {

    [SerializeField] private GroundAttack _groundAttack;
    [SerializeField] private float _searchRadius = 60f;
    [SerializeField] private Transform _leftSpawn;
    [SerializeField] private Transform _rightSpawn;
    private Transform _target;

    protected override void Start()
    {
        base.Start();
        EventController.Singleton.OnActivateBossEvent += ActivateBoss;
    }

    public void ActivateBoss()
    {
        if (!isServer) return;
        StartCoroutine(FocusOnClosestPlayer());
        StartCoroutine(StartBossRoutine());
    }

    private IEnumerator FocusOnClosestPlayer()
    {
        _target = FindClosestPlayerWithinDistance(_searchRadius);
        yield return new WaitForSeconds(3);
        StartCoroutine(FocusOnClosestPlayer());
    }

    private IEnumerator StartBossRoutine()
    {
        yield return StartHop(4);
        var _randomSpellID = UnityEngine.Random.Range(0, 4);
        NetworkState.Singleton.RpcSpawnProjectile(_randomSpellID, _leftSpawn.position, _leftSpawn.rotation, gameObject);
        NetworkState.Singleton.RpcSpawnProjectile(_randomSpellID, _rightSpawn.position, _rightSpawn.rotation, gameObject);
    }

    private IEnumerator StartHop(float time)
    {
        _anim.SetBool("Hop", true);
        yield return new WaitForSeconds(time);
        _anim.SetBool("Hop", false);
    }
    private void SpawnSpell(Transform origin, int spellID)
    {
        NetworkState.Singleton.RpcSpawnProjectile(spellID, origin.position, origin.rotation, gameObject);
    }
    public Transform GetTarget()
    {
        return _target;
    }


    public override void Land() {
        base.Land();
        Instantiate(_groundAttack, transform.position + Vector3.up, transform.rotation);
        EventController.Singleton.ScreenShake();
    }
    public override void Hit(float dmg, GameObject aggressor)
    {
        base.Hit(dmg,null);
        _anim.SetTrigger("Hit");

    }
    public override void Die()
    {
        EventController.Singleton.BossDied();
        _anim.SetTrigger("Die");
    }
    private void OnDestroy()
    {
        EventController.Singleton.OnActivateBossEvent -= ActivateBoss;
    }
}
