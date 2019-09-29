using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform target;

    private NavMeshAgent _navMeshAgent;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) 
        {
            MoveToCursor();
        }
        UpdateAnimator();
    }

    private void UpdateAnimator() 
    {
        Vector3 LocalVelocity = transform.InverseTransformDirection(_navMeshAgent.velocity);
        _animator.SetFloat("forwardSpeed", LocalVelocity.z);
    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);
        if (hasHit) 
        {
            _navMeshAgent.SetDestination(hit.point);
        }
        
    }
}
