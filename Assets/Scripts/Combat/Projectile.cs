using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat 
{
    public class Projectile : MonoBehaviour
    {

        [SerializeField] float speed;
        [SerializeField] bool isHoming = false;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] float maxLifeTime = 10f;
        [SerializeField] GameObject[] destroyOnHit = null;
        [SerializeField] float lifeAfterImpact = 2f;
        Vector3 aim = Vector3.zero;
        Health target;
        float damage = 0;

        public void SetTarget(Health target, float damage) 
        {
            this.target = target;
            this.damage = damage;

            Destroy(gameObject, maxLifeTime);
        }

        void Start()
        {
            if (transform == null) return;
            transform.LookAt(GetAimLocation());
        }

        // Update is called once per frame
        void Update()
        {
            if (transform == null) return;
            if (isHoming && !target.IsDead) transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private Vector3 GetAimLocation() 
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null) return target.transform.position;
            return target.transform.position + Vector3.up * targetCapsule.height/2;
        }

        void OnTriggerEnter(Collider collider) 
        {
            if (collider.gameObject.GetComponent<Health>() != target) return;
            if (target.IsDead) return;
            target.TakeDamage(damage);
            if (hitEffect != null ) Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            speed = 0;
            foreach (GameObject toDestroy in destroyOnHit)
            {
                Destroy(toDestroy);
            }
            GameObject.Destroy(this.gameObject, lifeAfterImpact);
        }
    }

}