using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat {

    public class Fighter : MonoBehaviour, IAction
    {

        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;

        Transform target;
        float timeSinceLastAttack = 0;

        private Mover _moverComponent;

        public void Start()
        {
            _moverComponent = GetComponent<Mover>();
        }

        public void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            
            if (!GetIsInRange())
            {
                _moverComponent.MoveTo(target.position);
            } else {
                _moverComponent.Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if (timeSinceLastAttack > timeBetweenAttacks) 
            {
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0;
            }
        }

        private bool GetIsInRange()
        {
            return weaponRange >= Vector3.Distance(transform.position, target.position);
        }

        public void Attack(CombatTarget combatTarget) 
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
            print("Take that you short, squat peasant!");
        }

        public void Cancel()
        {
            target = null;
        }

        // Animation event
        void Hit()
        {

        }
    }

}