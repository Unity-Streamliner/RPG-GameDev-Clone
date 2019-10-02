using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;

namespace RPG.Movement 
{
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
            UpdateAnimator();
        }

        private void UpdateAnimator() 
        {
            Vector3 LocalVelocity = transform.InverseTransformDirection(_navMeshAgent.velocity);
            _animator.SetFloat("forwardSpeed", LocalVelocity.z);
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            GetComponent<Fighter>().Cancel();
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination) {
            _navMeshAgent.SetDestination(destination);
            _navMeshAgent.isStopped = false;
        }

        public void Stop()
        {
            _navMeshAgent.isStopped = true;            
        }
    }
}
