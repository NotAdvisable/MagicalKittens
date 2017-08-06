using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BunnyKing : NetworkCharacter {

    [SerializeField] GroundAttack _groundAttack;
    protected override void Start()
    {
        base.Start();

    }

    public void ActivateBoss()
    {

    }

    public override void Land() {
        base.Land();
        Instantiate(_groundAttack, transform.position + Vector3.up, transform.rotation);
        EventController.Singleton.ScreenShake();
    }
    public override void Hit(float dmg)
    {
        base.Hit(dmg);
        _anim.SetTrigger("Hit");

    }
    public override void Die()
    {
        EventController.Singleton.BossDied();
        _anim.SetTrigger("Die");
    }
}
