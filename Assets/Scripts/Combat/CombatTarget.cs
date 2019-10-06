using UnityEngine;

namespace RPG.Combat 
{
    public class CombatTarget : MonoBehaviour 
    {
        public Health Health;

        void Start()
        {
            Health = GetComponent<Health>();
        }
    }
}