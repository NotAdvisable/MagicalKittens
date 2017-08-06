using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGuard : IFSMState<AIController>
{
    public void Enter(AIController entity)
    {
        entity.Agent.SetDestination(entity.GuardPosition.position);
    }

    public void Exit(AIController entity)
    {
        throw new NotImplementedException();
    }

    public void Reason(AIController entity)
    {
        throw new NotImplementedException();
    }

    public void Update(AIController entity)
    {
        throw new NotImplementedException();
    }

}
