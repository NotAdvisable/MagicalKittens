using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class AIController : StatefulMonoBehaviour<AIController> {
    public enum AIBehaviour
    {
        Guard,
        Patrol,
        Kamikaze,
        Mage
    }
    //general
    [SerializeField] private AIBehaviour _aiBehaviour;
    [SerializeField] private int _searchRadius;
    [SerializeField] private int _fieldofView = 180;
    //guard
    [SerializeField] private Transform _guardPosition;

    //patrol
    [HideInInspector]public List<Vector3> _patrolPoints = new List<Vector3>();
    [SerializeField] private Transform _patrolPathHolder;

    public int SearchRadius { get { return _searchRadius; } }
    public int FieldOfView { get { return _fieldofView; } }
    private EnemyController _controller;
    private NavMeshAgent _agent;
    public EnemyController Controller { get { return _controller; } }
    public NavMeshAgent Agent { get { return _agent; } }

    void Start()
    {
        _controller = GetComponent<EnemyController>();
        _agent = GetComponent<NavMeshAgent>();

        if (!_controller.isServer) return;

        switch (_aiBehaviour)
        {
            case AIBehaviour.Guard:
                break;
            case AIBehaviour.Patrol:
                if (_patrolPathHolder != null)
                {
                    foreach (Transform t in _patrolPathHolder)
                    {
                        if (!t.Equals(_patrolPathHolder))
                            _patrolPoints.Add(t.position);
                    }
                    fsm = new FSM<AIController>(this, new EnemyPatrol());
                }
                break;
            case AIBehaviour.Kamikaze:
                break;
            case AIBehaviour.Mage:
                break;
        }

    }

}
