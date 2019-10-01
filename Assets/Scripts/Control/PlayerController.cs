using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control 
{
    public class PlayerController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if(InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
                foreach(RaycastHit hit in hits)
                {
                    CombatTarget target = hit.transform.gameObject.GetComponent<CombatTarget>();
                    if (target != null) 
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            GetComponent<Fighter>().Attack(target);
                        }
                        return true;
                    }
                    
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
                    Mover mover = GetComponent<Mover>();
                    mover.StartMoveAction(hit.point);
                }
                return true;  
            } 
            return false;
        }

        private static Ray GetMouseRay() => Camera.main.ScreenPointToRay(Input.mousePosition);

    }
}
