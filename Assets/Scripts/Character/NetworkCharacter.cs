using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]

public class NetworkCharacter : NetworkBehaviour, IHitable {

    [SerializeField] private GameObject _landingEffect;
    [SerializeField] private AudioClip _landingSound;
    protected Animator _anim;
    protected Health _health;
    private SkinnedMeshRenderer _renderer;


    protected virtual void Start()
    {
        _anim = GetComponent<Animator>();
        _health = GetComponent<Health>();
        _renderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    public virtual void Land() {
        if (_landingEffect != null) Instantiate(_landingEffect, transform.position + Vector3.up,Quaternion.identity);
    }
    public virtual void Hit(float dmg, GameObject aggressor) {

        _health.InflictDamage(dmg);
        StartCoroutine(FlashHit());
    }

    public virtual void Die() { }

    public virtual Transform FindAnyPlayerWithinDistance(float distance)
    {
        return transform.position.FirstWithinDistance(NetworkState.Singleton.GetPlayerTransform(), distance);
    }
    public virtual Transform FindClosestPlayerWithinDistance(float distance)
    {
        return transform.ClosestTransformWithinDistance(NetworkState.Singleton.GetPlayerTransform(), distance);
    }
    
    private IEnumerator FlashHit()
    {
        _renderer.material.SetColor("_EmissionColor", Color.white);
        _renderer.UpdateGIMaterials();
        DynamicGI.UpdateEnvironment();
        yield return new WaitForSeconds(.1f);
        _renderer.material.SetColor("_EmissionColor", Color.black);
        _renderer.UpdateGIMaterials();
        DynamicGI.UpdateEnvironment();
    }

}
