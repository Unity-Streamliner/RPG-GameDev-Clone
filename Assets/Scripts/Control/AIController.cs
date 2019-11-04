using UnityEngine;
using RPG.Combat;
using UnityEngine.AI;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control
{
    public class AIController : MonoBehaviour 
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 5f;

        Fighter _fighter;
        Health _health;
        
        Vector3 guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        NavMeshAgent _navMeshAgent;
        GameObject _player;
        Mover _mover;

        void Start() 
        {
            _player = GameObject.FindWithTag("Player");
            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();
            _mover = GetComponent<Mover>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            guardPosition = transform.position;
            print(gameObject.name + " start position " + guardPosition);
        }

        void Update()
        {
            if (_health.IsDead) return;
            if (InAttackRangeOfPlayer() && _fighter.CanAttack(_player)) {
                timeSinceLastSawPlayer = 0;
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime) 
            {
                SuspicionBehaviour();
            } 
            else 
            {
                GuardBehaviour();
                Debug.Log(gameObject.name + " returning to start position. " + guardPosition);
            }
            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void AttackBehaviour() 
        {
            _fighter.Attack(_player);
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void GuardBehaviour()
        {
             _mover.StartMoveAction(guardPosition);
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
            return distanceToPlayer < chaseDistance;
        }

        private void OnDrawGizmosSelected() 
        {
            Gizmos.color = Color.blue; 
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}