using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHunt : IFSMState<AIController>
{
    private Transform _target;

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
        if (entity.Behaviour != AIController.AIBehaviour.Kamikaze) //Kamikaze hunt you relentlessly
        {
            var firstWithinDitance = entity.Controller.FindAnyPlayerWithinDistance(entity.SearchRadius);
            if (firstWithinDitance == null)
            {
                if (entity.Behaviour == AIController.AIBehaviour.Patrol)
                {
                    entity.ChangeState(new EnemyPatrol());
                }
                else if (entity.Behaviour == AIController.AIBehaviour.Guard)
                {
                    entity.ChangeState(new EnemyGuard());
                }
            }

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
    }
}
