using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class EnemyPatrol : IFSMState<AIController> {
    private int currentWaypoint;

    private static EnemyPatrol _instance = new EnemyPatrol();

    public static EnemyPatrol Singleton { get { return _instance; } }

    public void Enter(AIController entity)
    {
        currentWaypoint = entity._patrolPoints.IndexOf(entity.transform.ClosestVector3(entity._patrolPoints));
        entity.Agent.SetDestination(entity._patrolPoints[currentWaypoint]);
    }

    public void Exit(AIController entity)
    {
    }

    public void Reason(AIController entity)
    {
        var firstWithinDitance = entity.Controller.FindAnyPlayerWithinDistance(entity.SearchRadius);
        if (firstWithinDitance != null && entity.transform.WithinEulerAngle(firstWithinDitance, entity.FieldOfView))
        {
            Debug.Log("I SEE YOU");
        }

    }

    public void Update(AIController entity)
    {
        if (entity.Agent.remainingDistance <= entity.Agent.stoppingDistance)
        {
            currentWaypoint = (currentWaypoint < entity._patrolPoints.Count-1) ? currentWaypoint+1 : 0;
            //entity.StartCoroutine(TurnThenContinue(entity._patrolPoints[currentWaypoint], entity.transform, entity));
            entity.Agent.destination = entity._patrolPoints[currentWaypoint];
        }
        entity.Controller.SetAnimMoving(entity.Agent.velocity.magnitude/entity.Agent.speed);
    }

    private IEnumerator TurnThenContinue(Vector3 nextWaypoint, Transform entityTransform, AIController entity)
    {
       
        Vector3 dirToTarget = (nextWaypoint - entityTransform.position).normalized;
        float targetAngle = 90-Mathf.Atan2(dirToTarget.z, dirToTarget.x) * Mathf.Rad2Deg;
        while (Mathf.Abs(Mathf.DeltaAngle(entityTransform.eulerAngles.y,  targetAngle)) > 1f)
        {
            float angle = Mathf.MoveTowardsAngle(entityTransform.eulerAngles.y, targetAngle, entity.Agent.angularSpeed * Time.deltaTime);
            entityTransform.eulerAngles = Vector3.up * angle;
           
            yield return null;

        }
        entity.Agent.destination = nextWaypoint;
    }
    
}
