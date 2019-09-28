using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform target;

    private NavMeshAgent _navMeshAgent;
    private Ray lastRay;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(lastRay.origin, lastRay.direction * 100);
        }
        _navMeshAgent.SetDestination(target.transform.position);
    }
}
