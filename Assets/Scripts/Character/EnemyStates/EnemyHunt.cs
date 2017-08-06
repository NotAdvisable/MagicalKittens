using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHunt : IFSMState<AIController>
{
    private Transform _target;
    private float _huntedFor;

    public EnemyHunt(ref Transform target)
    {
        _target = target;

    }

    public void Enter(AIController entity)
    {
        entity.Agent.SetDestination(_target.position);
        entity.Agent.speed = entity.RunSpeed;
    }

    public void Exit(AIController entity)
    {
    }

    public void Reason(AIController entity)
    {
        if (entity.Controller.WithinAttackRange)
        {
            entity.ChangeState(new EnemyAttack(ref _target));
        }
        var firstWithinDitance = entity.Controller.FindAnyPlayerWithinDistance(entity.SearchRadius);
        switch (entity.Behaviour)
        {
            case AIController.AIBehaviour.Guard:
                if ((_huntedFor >= entity.GuardChaseTime && entity.GuardChaseTime != 0) || firstWithinDitance == null)
                {
                    entity.ChangeState(new EnemyGuard(2));
                }
                break;
            case AIController.AIBehaviour.Patrol:
                if (firstWithinDitance == null)
                {
                    entity.ChangeState(new EnemyPatrol());
                }
                break;
            case AIController.AIBehaviour.Kamikaze:
                //never stops hunting
                break;
            case AIController.AIBehaviour.Mage:
                if (firstWithinDitance == null)
                {
                    entity.ChangeState(new EnemyWait());
                }
                break;
        }

    }

    public void Update(AIController entity)
    {
        var closest = entity.Controller.FindClosestPlayerWithinDistance(entity.SearchRadius);
        if (_target != closest) _target = closest;

        if (entity.Agent.destination != _target.transform.position)
        {
            entity.Agent.SetDestination(_target.position);
        }
        entity.Controller.SetAnimMoving(entity.Agent.velocity.magnitude / entity.Agent.speed);
        _huntedFor = _huntedFor + Time.deltaTime;
    }
}
