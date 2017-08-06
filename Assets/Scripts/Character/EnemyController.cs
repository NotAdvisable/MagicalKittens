using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class EnemyController : NetworkCharacter {

    private NavMeshAgent _agent;

    protected override void Start()
    {
        base.Start();
        _agent = GetComponent<NavMeshAgent>();
    }
    public void SetAnimMoving(float value)
    {
        _anim.SetFloat("Speed", value);
    }
    [ClientRpc]
    public void RpcReplicateAgentDest(Vector3 destination)
    {
        _agent.destination = destination;
    }
    public override void Hit(float dmg)
    {
        _anim.SetTrigger("Hit");
        base.Hit(dmg);
    }
    public override void Die()
    {
        GetComponent<AIController>().TurnOffFSM();
        _anim.SetTrigger("Die");
        _agent.speed = 0;
    }
}
