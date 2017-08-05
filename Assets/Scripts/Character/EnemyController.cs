using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : NetworkCharacter {

    private Animator _anim;
	void Start () {
        _anim = GetComponent<Animator>();

    }
    public void SetAnimMoving(float value)
    {
        _anim.SetFloat("Speed", value);
    }

}
