using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]

public class NetworkCharacter : NetworkBehaviour, IHitable {

    [SerializeField] private GameObject _landingEffect;
    [SerializeField] private AudioClip _landingSound;
    protected Animator _anim;
    protected Health _health;

    protected virtual void Start()
    {
        _anim = GetComponent<Animator>();
        _health = GetComponent<Health>();
    }

    public virtual void Land() {
        if (_landingEffect != null) Instantiate(_landingEffect, transform.position + Vector3.up,Quaternion.identity);
    }
    public virtual void Hit(float dmg) {
        _health.InflictDamage(dmg);
    }

    public virtual void Die() { }

    public virtual void Decay() { }

    public virtual Transform FindAnyPlayerWithinDistance(float distance)
    {
        return transform.position.FirstWithinDistance(NetworkState.Singleton.GetPlayerTransform(), distance);
    }
    public virtual Transform FindClosestPlayerWithinDistance(float distance)
    {
        return transform.ClosestTransformWithinDistance(NetworkState.Singleton.GetPlayerTransform(), distance);
    }

}
