using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class EnemyPatrol : EnemySearch {
    private int _currentWaypoint;

    public override void Enter(AIController entity)
    {
        base.Enter(entity);
        _currentWaypoint = entity._patrolPoints.IndexOf(entity.transform.ClosestVector3(entity._patrolPoints));
        entity.Agent.SetDestination(entity._patrolPoints[_currentWaypoint]);
    }

    public override void Exit(AIController entity)
    {
    }

    public override void Reason(AIController entity)
    {
        base.Reason(entity);
    }

    public override void Update(AIController entity)
    {
        if (entity.Agent.remainingDistance <= entity.Agent.stoppingDistance)
        {
            _currentWaypoint = (_currentWaypoint < entity._patrolPoints.Count-1) ? _currentWaypoint+1 : 0;
            entity.Agent.SetDestination(entity._patrolPoints[_currentWaypoint]);
        }
        base.Update(entity);
    }


    
}
