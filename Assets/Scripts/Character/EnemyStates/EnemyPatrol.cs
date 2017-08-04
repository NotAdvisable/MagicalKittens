using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class EnemyPatrol : IFSMState<AIController> {
    private int currentWaypoint;

    private NavMeshAgent agent;

    private static EnemyPatrol _instance = new EnemyPatrol();

    public static EnemyPatrol Singleton { get { return _instance; } }

    public void Enter(AIController entity)
    {
        agent = entity.GetComponent<NavMeshAgent>();
        currentWaypoint = entity.wayPoints.IndexOf(entity.transform.ClosestVector3(entity.wayPoints));
        agent.SetDestination(entity.wayPoints[currentWaypoint]);
    }

    public void Exit(AIController entity)
    {
        throw new NotImplementedException();
    }

    public void Reason(AIController entity)
    {
      //  var firstWithinDitance = entity.Controller.FindFirstPlayerWithinDistance(entity.SearchRadius);
       // if (firstWithinDitance != null)
        //{

        //}
       // throw new NotImplementedException();
    }

    public void Update(AIController entity)
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            var waypoints = entity.wayPoints;
            Debug.Log(currentWaypoint + "" + waypoints.Count);
            if(currentWaypoint < waypoints.Count)
            {
                agent.destination = waypoints[currentWaypoint++];
            }
            else
            {
                currentWaypoint = 0;
                agent.destination = waypoints[currentWaypoint];
            }
        }
    }

}
