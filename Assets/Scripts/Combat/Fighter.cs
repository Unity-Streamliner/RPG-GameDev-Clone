using UnityEngine;
using RPG.Movement;

namespace RPG.Combat {

    public class Fighter : MonoBehaviour 
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
                _moverComponent.Stop();
            }
        }

        private bool GetIsInRange()
        {
            return weaponRange >= Vector3.Distance(transform.position, target.position);
        }

        public void Attack(CombatTarget combatTarget) 
        {
            target = combatTarget.transform;
            print("Take that you short, squat peasant!");
        }

        public void Cancel()
        {
            target = null;
        }
    }

}