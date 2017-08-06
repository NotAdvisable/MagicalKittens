using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWait : EnemySearch {

    public override void Enter(AIController entity)
    {
        base.Enter(entity);
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
        entity.transform.Rotate(Vector3.up, Time.deltaTime * (entity.Agent.angularSpeed/4));
        base.Update(entity);
    }
}
