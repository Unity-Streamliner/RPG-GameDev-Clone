using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat {

    public class Fighter : MonoBehaviour, IAction
    {

        [SerializeField] float weaponRange = 2f;

        Transform target;

        private Mover _moverComponent;

        public void Start()
        {
            _moverComponent = GetComponent<Mover>();
        }

        public void Update()
        {
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
            GetComponent<Animator>().SetTrigger("attack");
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