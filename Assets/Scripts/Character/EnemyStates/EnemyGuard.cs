using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGuard : EnemySearch
{
    private float _ignoreTime;
    private bool _repositioned;

    public EnemyGuard()
    {
        _ignoreTime = 0;
    }
    public EnemyGuard(float ignoreTime)
    {
        _ignoreTime = ignoreTime;
    }

    public override void Enter(AIController entity)
    {
        base.Enter(entity);
        entity.Agent.SetDestination(entity.GuardPosition.position);
    }

    public override void Exit(AIController entity)
    {
    }

    public override void Reason(AIController entity)
    {
        if (_ignoreTime > 0) return;
        base.Reason(entity);
    }

    public override void Update(AIController entity)
    {
        if (_ignoreTime > 0) _ignoreTime -= Time.deltaTime;
        if (entity.Agent.remainingDistance <= entity.Agent.stoppingDistance && !_repositioned)
        {
            _repositioned = true;
            entity.StartCoroutine(SlowTurnToFaceDirection(entity, entity.GuardPosition.transform.forward));
        }
        base.Update(entity);
    }
    private IEnumerator SlowTurnToFaceDirection(AIController owner, Vector3 lookAt)
    {
       var neededRotation = Quaternion.LookRotation(lookAt - owner.transform.position);
        while (owner.transform.rotation != neededRotation)
        {
            owner.transform.rotation = Quaternion.RotateTowards(owner.transform.rotation, neededRotation, owner.Agent.angularSpeed * Time.deltaTime);
            yield return null;
        }
    }

}
