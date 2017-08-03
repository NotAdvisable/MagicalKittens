using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : StatefulMonoBehaviour<AIController> {

    [SerializeField] private GameObject _patrolPath;
    [HideInInspector]public List<Vector3> wayPoints = new List<Vector3>();

    void Awake()
    {
        foreach (Transform t in _patrolPath.transform)
        {
            if (!t.Equals(_patrolPath.transform))
                wayPoints.Add(t.position);
            Debug.Log(t.position);
        }

        fsm = new FSM<AIController>();
        fsm.Configure(this, new EnemyPatrol());
    }
}
