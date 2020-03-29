using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using UnityEngine;

namespace RPG.Combat 
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon;

        void OnTriggerEnter(Collider collider) 
        {
            if (collider.gameObject.tag == "Player")
            {
                Fighter fighter = collider.gameObject.GetComponent<Fighter>();
                if (fighter != null) 
                {
                    fighter.EquipWeapon(weapon);
                    GameObject.Destroy(gameObject);
                }
            }
        }
    }
}
