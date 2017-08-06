using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : IFSMState<AIController>
{
    private Transform _target;

    public EnemyAttack(ref Transform target)
    {
        _target = target;
    }
    public void Enter(AIController entity)
    {
        if(entity.Behaviour == AIController.AIBehaviour.Kamikaze)
        {
            entity.Controller.SelfDestruct();
        }
    }

    public void Exit(AIController entity)
    {
    }

    public void Reason(AIController entity)
    {
        if (!entity.Controller.WithinAttackRange)
        {
            entity.ChangeState(new EnemyHunt(ref _target));
        }
    }

    public void Update(AIController entity)
    {
        entity.transform.LookAt(_target);
        if (entity.Controller.WithinAttackRange &&  entity.Controller.CoolDownComplete)
        {
            if (entity.Behaviour == AIController.AIBehaviour.Mage)
            {
                entity.Controller.RangeAttack();
            }
            else
            {
                entity.Controller.Attack();
            }
        }

        entity.Controller.SetAnimMoving(entity.Agent.velocity.magnitude / entity.Agent.speed);
    }
}
