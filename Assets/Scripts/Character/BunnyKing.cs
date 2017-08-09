using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Script that handles the king's behaviour using coroutine sequences
/// </summary>
public class BunnyKing : NetworkCharacter
{

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
        while (!_health.AlreadyDead)
        {
            _target = FindClosestPlayerWithinDistance(_searchRadius);
            _leftSpawn.LookAt(_target);
            _rightSpawn.LookAt(_target);
            yield return null;
        }
    }

    /// <summary>
    /// movement routine for the boss
    /// </summary>
    private IEnumerator StartBossRoutine()
    {
        while (!_health.AlreadyDead)
        {
            yield return StartHop(4);
            yield return new WaitForSeconds(1);
            ShootRandomSpell();
            yield return new WaitForSeconds(2);
            yield return StartHop(6);
            yield return new WaitForSeconds(1);
            ShootFinalSpell();
            yield return new WaitForSeconds(1);
        }

    }

    private IEnumerator StartHop(float time)
    {
        _anim.SetBool("Hop", true);
        yield return new WaitForSeconds(time);
        _anim.SetBool("Hop", false);
    }
    /// <summary> Spawns final spell in both eyes</summary>
    private void ShootFinalSpell()
    {
        NetworkState.Singleton.RpcSpawnProjectile(4, _leftSpawn.position, _leftSpawn.rotation, gameObject);
        NetworkState.Singleton.RpcSpawnProjectile(4, _rightSpawn.position, _rightSpawn.rotation, gameObject);
    }
    /// <summary> Spawns random spell in both eyes</summary>
    private void ShootRandomSpell()
    {
        var _randomSpellID = UnityEngine.Random.Range(0, 4);
        NetworkState.Singleton.RpcSpawnProjectile(_randomSpellID, _leftSpawn.position, _leftSpawn.rotation, gameObject);
        NetworkState.Singleton.RpcSpawnProjectile(_randomSpellID, _rightSpawn.position, _rightSpawn.rotation, gameObject);
    }

    public override void Land()
    {
        base.Land();
        Instantiate(_groundAttack, transform.position + Vector3.up, transform.rotation);
        EventController.Singleton.ScreenShake();
    }
    public override void Hit(float dmg, GameObject aggressor)
    {
        base.Hit(dmg, null);
        _anim.SetTrigger("Hit");

    }
    public override void Die()
    {
        EventController.Singleton.BossDied();
        _anim.SetTrigger("Die");
        GetComponent<NetworkTransform>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }
    private void OnDestroy()
    {
        EventController.Singleton.OnActivateBossEvent -= ActivateBoss;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_leftSpawn.position, 1f);
        Gizmos.DrawSphere(_rightSpawn.position, 1f);
        if(_target != null) Gizmos.DrawSphere(_target.position, 1f);
    }
}
