using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat {

    public class Fighter : MonoBehaviour, IAction
    {

        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 10f;

        CombatTarget target;
        float timeSinceLastAttack = 0;

        private Mover _moverComponent;
        private Animator _animator;

        public void Start()
        {
            _moverComponent = GetComponent<Mover>();
            _animator = GetComponent<Animator>();
        }

        public void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.Health.IsDead) return;
            
            if (!GetIsInRange())
            {
                _moverComponent.MoveTo(target.transform.position);
            } else {
                _moverComponent.Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks) 
            {
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0;
                // this will trigger the hit event.
            }
        }

        private bool GetIsInRange()
        {
            return weaponRange >= Vector3.Distance(transform.position, target.transform.position);
        }

        public void Attack(CombatTarget combatTarget) 
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget;
            print("Take that you short, squat peasant!");
        }

        public void Cancel()
        {
            _animator.SetTrigger("stopAttack");
            target = null;
        }

        // Animation event
        void Hit()
        {
            target.Health.TakeDamage(weaponDamage);
        }
    }

}