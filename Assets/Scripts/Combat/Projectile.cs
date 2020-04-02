using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float speed;
    Health target;
    float damage = 0;

    // Start is called before the first frame update
    public void SetTarget(Health target, float damage) 
    {
        this.target = target;
        this.damage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform == null) return;
        transform.LookAt(GetAimLocation());
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
        target.TakeDamage(damage);
        GameObject.Destroy(this.gameObject);
    }
}
