using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat {

    public class Fighter : MonoBehaviour, IAction
    {

        
        [SerializeField] float timeBetweenAttacks = 1f;
        
        [SerializeField] Weapon weapon = null;
        [SerializeField] Transform handTransform = null;
        

        GameObject target;
        float timeSinceLastAttack = Mathf.Infinity;

        private Mover _moverComponent;
        private Animator _animator;

        public void Start()
        {
            _moverComponent = GetComponent<Mover>();
            _animator = GetComponent<Animator>();
            SpawnWeapon();
        }

        public void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            Health targetHealth = target.GetComponent<Health>();
            if (targetHealth != null && targetHealth.IsDead) return;
            
            if (!GetIsInRange())
            {
                _moverComponent.MoveTo(target.transform.position, 1f);
            } else {
                _moverComponent.Cancel();
                AttackBehaviour();
            }
        }

        private void SpawnWeapon() 
        {
            if (weapon == null) return;
            weapon.Spawn(handTransform, _animator);
        }

        public bool CanAttack(GameObject combatTarget) 
        {
            if (combatTarget == null) return false;
            // cannot attack itself
            if (gameObject.tag == combatTarget.tag)
            {
                Debug.Log("Cannot attack itself");
                return false;
            }
            Health targetHealth = combatTarget.GetComponent<Health>();
            return targetHealth != null && !targetHealth.IsDead;
        }  

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks) 
            {
                StartAttack();
                timeSinceLastAttack = 0;
                // this will trigger the hit event.
            }
        }

        private bool GetIsInRange()
        {
            if (weapon == null) return false;
            return weapon.getRange() >= Vector3.Distance(transform.position, target.transform.position);
        }

        public void Attack(GameObject combatTarget) 
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget;
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
            _moverComponent.Cancel();
        }

        private void StartAttack()
        {
            _animator.ResetTrigger("stopAttack");
            _animator.SetTrigger("attack");
        }

        private void StopAttack()
        {
            _animator.SetTrigger("stopAttack");
            _animator.ResetTrigger("attack");
        }

        // Animation event
        void Hit()
        {
            if (target == null) return;
            Health targetHealth = target.GetComponent<Health>();
            if (targetHealth != null && weapon != null) targetHealth.TakeDamage(weapon.getDamage());
        }
    }

}