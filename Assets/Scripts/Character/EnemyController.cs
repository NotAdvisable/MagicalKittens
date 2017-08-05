using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class EnemyController : NetworkCharacter {

    private Animator _anim;
    private NavMeshAgent _agent;

	void Start () {
        _anim = GetComponent<Animator>();
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
    public override void Hit()
    {
        _anim.SetTrigger("Hit");
    }
}
