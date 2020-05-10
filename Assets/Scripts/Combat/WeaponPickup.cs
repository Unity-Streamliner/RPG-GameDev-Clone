using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using UnityEngine;

namespace RPG.Combat 
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon;
        [SerializeField] float respawnTime = 5.0f;

        void OnTriggerEnter(Collider collider) 
        {
            if (collider.gameObject.tag == "Player")
            {
                Fighter fighter = collider.gameObject.GetComponent<Fighter>();
                if (fighter != null) 
                {
                    fighter.EquipWeapon(weapon);
                    StartCoroutine(HideForSeconds(respawnTime));
                }
            }
        }

        private IEnumerator HideForSeconds(float seconds) 
        {
            ShowPickup(false);
            yield return new WaitForSeconds(seconds);
            ShowPickup(true);
        }

        private void HidePickup() 
        {
            gameObject.GetComponent<SphereCollider>().enabled = false;
            foreach(Transform child in gameObject.transform) {
                child.gameObject.SetActive(false);
            }
        }

        private void ShowPickup(bool enable)
        {
            gameObject.GetComponent<SphereCollider>().enabled = enable;
            foreach(Transform child in gameObject.transform) {
                child.gameObject.SetActive(enable);
            }
        }
    }
}
