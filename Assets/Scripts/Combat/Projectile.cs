using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] bool isHoming = false;
    Vector3 aim = Vector3.zero;
    Health target;
    float damage = 0;

    public void SetTarget(Health target, float damage) 
    {
        this.target = target;
        this.damage = damage;
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
        GameObject.Destroy(this.gameObject);
    }
}
