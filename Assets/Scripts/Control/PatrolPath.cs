using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control 
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] float waypointGizmoRadius = 0.3f;

        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.color = Color.yellow; 
                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoRadius);
                int nextIndex = (i + 1) == transform.childCount ? 0 : i + 1;
                Gizmos.DrawLine( GetWaypoint(i), GetWaypoint(nextIndex));
            }
        }

        private Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}
