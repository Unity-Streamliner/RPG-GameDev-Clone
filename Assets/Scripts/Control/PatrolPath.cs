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
                Gizmos.DrawSphere(transform.GetChild(i).transform.position, waypointGizmoRadius);
            }
        }
    }
}
