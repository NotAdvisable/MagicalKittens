using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for states that check if a player is within their field of view
/// </summary>
public abstract class EnemySearch : IFSMState<AIController>
{
    public virtual void Enter(AIController entity)
    {
        entity.Agent.speed = entity.WalkSpeed;
    }

    public abstract void Exit(AIController entity);

    public virtual void Reason(AIController entity)
    {
        var firstWithinDitance = entity.Controller.FindAnyPlayerWithinDistance(entity.SearchRadius);
        if (firstWithinDitance != null && entity.transform.WithinEulerAngle(firstWithinDitance, entity.FieldOfView))
        {
            entity.ChangeState(new EnemyHunt(firstWithinDitance));
        }
    }

    public virtual void Update(AIController entity)
    {
        entity.Controller.SetAnimMoving(entity.Agent.velocity.magnitude / entity.Agent.speed);
    }

}