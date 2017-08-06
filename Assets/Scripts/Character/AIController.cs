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
    [SerializeField] private float _walkSpeed = 6;
    [SerializeField] private float _runSpeed = 12;

    public AIBehaviour Behaviour { get { return _aiBehaviour; } }
    public int SearchRadius { get { return _searchRadius; } }
    public int FieldOfView { get { return _fieldofView; } }
    public float WalkSpeed { get { return _walkSpeed; } }
    public float RunSpeed { get { return _runSpeed; } }

    private EnemyController _controller;
    private NavMeshAgent _agent;

    public EnemyController Controller { get { return _controller; } }
    public NavMeshAgent Agent { get { return _agent; } }

    //guard
    [SerializeField] private Transform _guardPosition;
    [SerializeField] private float _guardChaseTime;
    public Transform GuardPosition { get { return _guardPosition; } }
    public float GuardChaseTime { get { return _guardChaseTime; } }

    //patrol
    [HideInInspector]public List<Vector3> _patrolPoints = new List<Vector3>();
    [SerializeField] private Transform _patrolPathHolder;
    void Start()
    {
        _controller = GetComponent<EnemyController>();
        _agent = GetComponent<NavMeshAgent>();

        if (!_controller.isServer) {
            _agent.enabled = false;
            return;
        }
        StartCoroutine(WaitUntilServerReady());
    }

    protected override void Update()
    {
        if (!_controller.isServer) return;

        base.Update();
    }
    private IEnumerator WaitUntilServerReady()
    {
        yield return new WaitForSeconds(2);
        Initialise();
    }
    private void Initialise()
    {
        switch (_aiBehaviour)
        {
            case AIBehaviour.Guard:
                if(_guardPosition != null)
                {
                    fsm = new FSM<AIController>(this, new EnemyGuard());
                }
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
                fsm = new FSM<AIController>(this, new EnemyWait());
                break;
            case AIBehaviour.Mage:
                fsm = new FSM<AIController>(this, new EnemyWait());
                break;
        }
    }

    public void TurnOffFSM()
    {
        fsm = null;
    }
}
