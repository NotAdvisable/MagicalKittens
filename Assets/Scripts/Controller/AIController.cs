using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AIController : StatefulMonoBehaviour<AIController> {

    [SerializeField] private GameObject _patrolPath;
    [SerializeField] private float _searchRadius;
    [HideInInspector]public List<Vector3> wayPoints = new List<Vector3>();

    public float SearchRadius { get { return _searchRadius; } }

    private EnemyController _controller;

    public NetworkCharacter Controller { get { return _controller; } }

    void Awake()
    {
        _controller = GetComponent<EnemyController>();

        if (_patrolPath != null)
        {
            foreach (Transform t in _patrolPath.transform)
            {
            if (!t.Equals(_patrolPath.transform))
                wayPoints.Add(t.position);
            }
            fsm = new FSM<AIController>(this, new EnemyPatrol());
        }
    }

}
