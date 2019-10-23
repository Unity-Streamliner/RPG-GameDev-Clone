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

        Fighter _fighter;
        Health _health;
        
        Vector3 guardPosition;
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
                Debug.Log(gameObject.name + " Attack " + _player.name);
                _fighter.Attack(_player);
            } else if(!InAttackRangeOfPlayer())
            {
                _mover.StartMoveAction(guardPosition);
                _navMeshAgent.SetDestination(guardPosition);
                Debug.Log(gameObject.name + " returning to start position. " + guardPosition);
            }
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