﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Saving;

namespace RPG.Movement 
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] Transform target;
        [SerializeField] float maxSpeed = 6f;

        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private Health _health;

        // Start is called before the first frame update
        void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _health = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            _navMeshAgent.enabled = !_health.IsDead;
            UpdateAnimator();
        }

        private void UpdateAnimator() 
        {
            Vector3 LocalVelocity = transform.InverseTransformDirection(_navMeshAgent.velocity);
            _animator.SetFloat("forwardSpeed", LocalVelocity.z);
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 destination, float speedFraction) {
            _navMeshAgent.SetDestination(destination);
            _navMeshAgent.isStopped = false;
            _navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction); 
        }

        public void Cancel()
        {
            _navMeshAgent.isStopped = true;            
        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            _navMeshAgent.enabled = false;
            transform.position = ((SerializableVector3)state).ToVector();
            _navMeshAgent.enabled = true;
        }
    }
}
