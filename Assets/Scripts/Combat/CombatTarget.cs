using UnityEngine;
using RPG.Core;

namespace RPG.Combat 
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour 
    {
        public Health Health;

        void Start()
        {
            Health = GetComponent<Health>();
        }
    }
}