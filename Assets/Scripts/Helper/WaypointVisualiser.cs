using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointVisualiser : MonoBehaviour
{

    [SerializeField] private Color _waypointGroupColour;

    private void OnDrawGizmos()
    {
        foreach (Transform t in transform)
        {
            Gizmos.color = _waypointGroupColour;
            Gizmos.DrawSphere(t.position, 1f);
        }
    }
}
