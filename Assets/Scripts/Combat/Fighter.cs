using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Saving;

namespace RPG.Combat {

    public class Fighter : MonoBehaviour, IAction, ISavable
    {

        
        [SerializeField] float timeBetweenAttacks = 1f;
        
        [SerializeField] Weapon defaultWeapon = null;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;

        GameObject target;
        float timeSinceLastAttack = Mathf.Infinity;
        Weapon currentWeapon = null;

        private Mover _moverComponent;
        private Animator _animator;

        private void Awake() 
        {
            _animator = GetComponent<Animator>();
        }

        public void Start()
        {
            _moverComponent = GetComponent<Mover>();
            
            
            if (currentWeapon == null) {
                EquipWeapon(defaultWeapon);
            }
        }

        private Weapon LoadWeapon(string weaponName)
        {
            Weapon weapon = Resources.Load<Weapon>(weaponName);
            return weapon;
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

        public void EquipWeapon(Weapon weapon) 
        {
            currentWeapon = weapon;
            weapon.Spawn(rightHandTransform, leftHandTransform, _animator);
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
            if (currentWeapon == null) return false;
            return currentWeapon.getRange() >= Vector3.Distance(transform.position, target.transform.position);
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
            if (targetHealth != null && currentWeapon != null)
            {
                if (currentWeapon.HasProjectile())
                {
                    currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, targetHealth);
                } else 
                {
                    targetHealth.TakeDamage(currentWeapon.getDamage());
                }
            }
        }

        void Shoot()
        {
            Hit();
        }

        public object CaptureState()
        {
            return currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            Weapon weapon = Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);
        }
    }

}