using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : IFSMState<AIController> {
    private int currentWaypoint = 0;

    private NavMeshAgent agent;

    static readonly EnemyPatrol instance = new EnemyPatrol();
    public static EnemyPatrol Instance
    {
        get { return instance; }
    }


    public void Enter(AIController entity)
    {
        agent = entity.GetComponent<NavMeshAgent>();
        agent.SetDestination(entity.wayPoints[currentWaypoint]);
        Debug.Log("started patrolling");
    }

    public void Exit(AIController entity)
    {
        throw new NotImplementedException();
    }

    public void Reason(AIController entity)
    {
       // throw new NotImplementedException();
    }

    public void Update(AIController entity)
    {
       // throw new NotImplementedException();
    }
}
