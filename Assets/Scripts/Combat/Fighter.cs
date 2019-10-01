using UnityEngine;
using RPG.Movement;

namespace RPG.Combat {

    public class Fighter : MonoBehaviour 
    {

        [SerializeField] float weaponRange = 2f;

        Transform target;

        public void Update()
        {
            
            if (target != null)
            {
                bool isInRange = weaponRange >= Vector3.Distance(transform.position, target.position);
                if (isInRange){
                    target = null;
                    GetComponent<Mover>().Stop();
                } 
                GetComponent<Mover>().MoveTo(target.position);
            }
        }

        public void Attack(CombatTarget combatTarget) 
        {
            target = combatTarget.transform;
            print("Take that you short, squat peasant!");
        }
    }

}