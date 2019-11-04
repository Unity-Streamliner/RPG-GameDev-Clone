using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control 
{
    public class PlayerController : MonoBehaviour
    {

        Mover _mover;
        Fighter _fighter;
        Health _health;
        // Start is called before the first frame update
        void Start()
        {
            _mover = GetComponent<Mover>();
            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_health.IsDead) return;
            if(InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
                foreach(RaycastHit hit in hits)
                {
                    CombatTarget target = hit.transform.gameObject.GetComponent<CombatTarget>();
                    if (target == null) continue;
                    if(!_fighter.CanAttack(target.gameObject)) continue;
                    if (Input.GetMouseButton(0))
                    {
                        _fighter.Attack(target.gameObject);
                    }
                    return true;
                    
                }
                return false;
        }

        private bool InteractWithMovement() 
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit) 
            {
                if (Input.GetMouseButton(0)) 
                {
                    _mover.StartMoveAction(hit.point);
                }
                return true;  
            } 
            return false;
        }

        private static Ray GetMouseRay() => Camera.main.ScreenPointToRay(Input.mousePosition);

    }
}
