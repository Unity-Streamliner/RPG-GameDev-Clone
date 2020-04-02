using System;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat 
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject 
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] float weaponDamage = 10f;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;

        public float getDamage() => weaponDamage; 

        public float getRange() => weaponRange;

        public bool HasProjectile() => projectile != null;

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target) 
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target);
        }

        public void Spawn(Transform rightHandTransform, Transform leftHandTransform, Animator animator)
        {
            if (equippedPrefab != null)
            {
                Transform handTransform = GetTransform(rightHandTransform, leftHandTransform);
                Instantiate(equippedPrefab, handTransform);
            }
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }

        private Transform GetTransform(Transform rightHandTransform, Transform leftHandTransform)
        {
            return isRightHanded ? rightHandTransform : leftHandTransform;
        }
    }
}